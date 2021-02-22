using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Singleton that stores a dictionary of localized string values for the loaded language
/// </summary>
public class LocalizationManager : MonoBehaviour
{
    /* Path Values */
    public const string FILE_EXTENSION = ".json";
    public const string FOLDER_IN_STREAMMING_ASSETS_NAME = "Locale";
    public const string FILENAME_PREFIX = "locale_";

    public static LocalizationManager Instance { get; private set; }    // Singleton Instance set during Awake

    public bool Ready { get; private set; }                             // Used by the Loading Screen Manager to tell when this this manager's Start coroutine has finished

    public string LoadedLanguage { get; private set; }                  // Stores the 2-character ISO language code. Used for building a path to the locale dict}

    private Dictionary<string, string> _localizedDictionary;            // A dictionary built during the manager's start coroutine.
                                                                        // Used to retrieve text for Localized Text components

    private StringBuilder _filenameStringBuilder;                       // Used to build the path to the locale-dependent JSON dictionary stored in the StreamingAssets directory

    private LocalizationData _loadedData;                               // Stores the localization data from the conversion of the loaded JSON text

    private string _loadedJsonText;                                     // Stores the JSON text before converting from JSON to the Localization Data csharp container

    public event EventHandler OnLanguageOverride;                       // Triggered whenever a user overrides the current language.
                                                                        // Useful for letting text elements know they have to change to a new language

    private void Awake()
    {
        if (Instance == null) // Must set Instance early to avoid race condition with Loading Screen Manager
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        Ready = false; // Set Ready to false before manager is setup

        // Clear out fields
        _filenameStringBuilder = new StringBuilder();
        _loadedData = null;
        _loadedJsonText = string.Empty;
    }

    private IEnumerator Start() 
    {
        yield return StartCoroutine(LoadJsonLanguageData(LocaleHelper.GetSupportedLanguageCode())); // Load Language

        Ready = true; // Set Ready to true after setup has been completed

        // Clear out fields
        _filenameStringBuilder = new StringBuilder();
        _loadedData = null;
        _loadedJsonText = string.Empty;
    }

    /// <summary>
    /// Loads the locale dict into a private field
    /// </summary>
    /// <param name="languageCode">
    /// Two character ISO locale code supported by the LocaleHelper
    /// </param>
    /// <returns></returns>
    private IEnumerator LoadJsonLanguageData(string languageCode)
    {
        string filePath = GetPathForLocale(languageCode);
        yield return LoadFileContents(filePath);

        if (string.IsNullOrEmpty(_loadedJsonText))
        {
            if (languageCode.ToLower().Equals(LocaleHelper.DefaultSupportedLanguageCode.ToLower()))
            {
                // Log an error due to missing file for default language.
                Debug.LogError("Localization file for default language: " + LocaleHelper.DefaultSupportedLanguageCode + " is missing");
            }

            yield return LoadJsonLanguageData(LocaleHelper.DefaultSupportedLanguageCode);
        }
        else
        {
            LoadedLanguage = languageCode;

            // Convert JSON text to Localization Data class
            _loadedData = JsonUtility.FromJson<LocalizationData>(_loadedJsonText);
            _localizedDictionary = new Dictionary<string, string>(_loadedData.items.Count);
            _loadedData.items.ForEach(item =>
            {
                try
                {
                    _localizedDictionary.Add(item.key, item.value);
                }
                catch (Exception e)
                {
                    // Log the exception
                    Debug.LogError("Failed to parse loaded Data for " + LoadedLanguage + " language." + " E message: " + e.Message);
                }
            });
            _filenameStringBuilder.Length = 0;
        }
    }

    /// <summary>
    /// This method builds a path by taking the ISO languague code (e.x. 'en') and
    /// appending it to the filename prefix "locale_" with the ".json" file extension.
    /// </summary>
    /// <param name="languageCode"></param>
    /// <returns>
    /// Returns a path string in the format:
    /// StreamingAssets/Locale/locale_xx.json
    /// </returns>
    private string GetPathForLocale(string languageCode)
    {
        _filenameStringBuilder.Length = 0;
        _filenameStringBuilder.Append(FILENAME_PREFIX).Append(languageCode.ToLower()).Append(FILE_EXTENSION);
        return Path.Combine(
            Path.Combine(Application.streamingAssetsPath, FOLDER_IN_STREAMMING_ASSETS_NAME),
            _filenameStringBuilder.ToString()
        );
    }

    /// <summary>
    /// Loads a JSON file from the specified path
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    private IEnumerator LoadFileContents(string filePath)
    {
        if (filePath.Contains("://")) // Http paths are currently unused in the project, don't worry
        {
            UnityWebRequest www = UnityWebRequest.Get(filePath);
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError($"Missing web file: " + filePath); // Missing file
            }

            _loadedJsonText = www.downloadHandler.text;
        }
        else
        {
            if (File.Exists(filePath))
            {
                _loadedJsonText = File.ReadAllText(filePath); // Store raw JSON into field
            }
            else
            {
                Debug.LogError("Missing local file: " + filePath); // Missing file
            }
        }
    }

    /// <summary>
    /// Retrives a string value corresponding to the Localization Key
    /// </summary>
    /// <param name="localizationKey">
    /// Key used to retrieve text from the active locale dict
    /// </param>
    /// <returns>
    /// Key-specific text
    /// </returns>
    public string GetTextForKey(string localizationKey)
    {
        if (_localizedDictionary == null)
        {
            throw new MissingLocalizationException("You are missing LocalizationManager in the scene. Either add it and remove it before commit or run the app from loading screen.");
        }

        if (_localizedDictionary.ContainsKey(localizationKey))
        {
            return _localizedDictionary[localizationKey];
        }

        throw new MissingLocalizationException(string.Format("Missing localization for key: {0} and language: {1}.", localizationKey, LoadedLanguage));
    }

    /// <summary>
    /// Sets a new system language for the current runtime.
    /// </summary>
    /// <param name="languageCode">
    /// Two character ISO language code. All supported languages can be found in ApplicationLocale
    /// </param>
    /// <returns></returns>
    public IEnumerator OverrideLanguage(string languageCode)
    {
        if (Ready) // Only let one override process execute at a time
        {
            Ready = false;
            _filenameStringBuilder = new StringBuilder();
            yield return StartCoroutine(LoadJsonLanguageData(languageCode));
            OnLanguageOverride?.Invoke(this, EventArgs.Empty);
            Ready = true;
        }
    }
}
