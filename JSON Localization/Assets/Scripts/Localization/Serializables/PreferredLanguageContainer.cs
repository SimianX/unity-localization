using System;
using System.IO;
using UnityEngine;

/// <summary>
/// Container for storing the user's language preference
/// </summary>
[Serializable]
public class PreferredLanguageContainer
{
    public const string RootDirectory = "Preferences";
    public const string Path = RootDirectory + "/" + "LanguageCode.json";

    public string languageCode;

    public PreferredLanguageContainer()
    {
        languageCode = LocaleHelper.GetSupportedLanguageCode(); // Default to the system locale if supported
    }

    public PreferredLanguageContainer(string languageCode)
    {
        this.languageCode = languageCode;
    }

    /// <summary>
    /// If the Root Directory doesn't exist, make it
    /// </summary>
    private static void ValidateDirectory()
    {
        if (!Directory.Exists(RootDirectory))
        {
            Directory.CreateDirectory(RootDirectory);
        }
    }

    /// <summary>
    /// Will attempt to load the user's preffered language from the container's file path
    /// </summary>
    /// <returns>
    /// Will either return the user's language preference or determine a suitable default with LocaleHelper
    /// </returns>
    public static string LoadLanguageCode()
    {
        ValidateDirectory();

        try
        {
            if (!File.Exists(Path))
            {
                throw new Exception("Could not find Preferred Language file");
            }

            string json = File.ReadAllText(Path);
            if (string.IsNullOrEmpty(json))
            {
                throw new Exception("JSON is empty");
            }

            string languageCode = JsonUtility.FromJson<PreferredLanguageContainer>(json).languageCode;

            if (!LocaleHelper.AllSupportedLanguageCodes.Contains(languageCode))
            {
                throw new Exception(string.Format("Language code \"{0}\" is unsupported", languageCode));
            }

            return languageCode;
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex + "\nDeriving default language from OS locale");
            return new PreferredLanguageContainer().languageCode;
        }
    }

    /// <summary>
    /// Will save the container to its file path
    /// </summary>
    /// <param name="preferredLanguageCode">
    /// Container that stores the language code
    /// </param>
    public static void SaveLanguageCode(PreferredLanguageContainer preferredLanguageCode)
    {
        ValidateDirectory();

        File.WriteAllText(Path, JsonUtility.ToJson(preferredLanguageCode));
    }
}
