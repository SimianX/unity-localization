using System;
using System.IO;
using UnityEngine;

/// <summary>
/// Container for storing the user's language preference
/// </summary>
[Serializable]
public class PreferredLanguageContainer
{
    private const string RootDirectory = "Preferences";
    private const string Path = "LanguageCode.json";

    public string languageCode;

    public PreferredLanguageContainer()
    {
        languageCode = LocaleHelper.GetSupportedLanguageCode();
    }

    public PreferredLanguageContainer(string languageCode)
    {
        this.languageCode = languageCode;
    }

    private static void ValidateDirectory()
    {
        if (!Directory.Exists(RootDirectory))
        {
            Directory.CreateDirectory(RootDirectory);
        }
    }

    public static string LoadLanguageCode()
    {
        ValidateDirectory();

        try
        {
            if (!File.Exists(Path))
            {
                throw new Exception("Could not find Preferred Language file!");
            }

            string json = File.ReadAllText(Path);
            if (string.IsNullOrEmpty(json))
            {
                throw new Exception("JSON is empty");
            }

            return JsonUtility.FromJson<PreferredLanguageContainer>(json).languageCode;
        }
        catch
        {
            return new PreferredLanguageContainer().languageCode;
        }
    }

    public static void SaveLanguageCode(PreferredLanguageContainer preferredLanguageCode)
    {
        ValidateDirectory();

        File.WriteAllText(Path, JsonUtility.ToJson(preferredLanguageCode));
    }
}
