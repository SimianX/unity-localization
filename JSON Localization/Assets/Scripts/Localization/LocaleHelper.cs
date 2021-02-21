using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/* TODO:
 * -> Create a list of all supported languages as they are spelt in their own language, i.e. French => Français
 */

/// <summary>
/// Contains static methods and expressions 
/// </summary>
public static class LocaleHelper
{
    // IMPORTANT: Whenever a new System Language is added to the SupportedLanguages list,
    //            make sure to add a corresponding ApplicationLocale string constant. Also
    //            ensure that the GetSupportedLanguageCode method has a switch case for the
    //            new language.

    /// <summary>
    /// List of all supported System Langugages (UnityEngine)
    /// </summary>
    public static List<SystemLanguage> AllSupportedLanguages => new List<SystemLanguage>
    {
        SystemLanguage.English,
        SystemLanguage.French
    };

    /// <summary>
    /// List of all supported language names (in English)
    /// </summary>
    public static List<string> AllSupportedLanguageNamesEN => AllSupportedLanguages.Select(lang => lang.ToString()).ToList();

    /// <summary>
    /// List of all supported language codes
    /// </summary>
    public static List<string> AllSupportedLanguageCodes => AllSupportedLanguages.Select(lang => GetSupportedLanguageCode(lang)).ToList();

    /// <summary>
    /// Stores the default 2-letter ISO code for the application's default language.
    /// </summary>
    public static string DefaultSupportedLanguageCode => ApplicationLocale.EN;

    /// <summary>
    /// Helps to convert Unity's Application.systemLanguage to a
    /// 2 letter ISO country code. English will be returned by default.
    /// Otherwise supported language code will be returned.
    /// </summary>
    /// <returns>
    /// The 2-letter ISO code from system language if the language is supported by the application.
    /// If the language is not supported English will be returned.
    /// </returns>
    public static string GetSupportedLanguageCode()
    {
        return GetSupportedLanguageCode(Application.systemLanguage);
    }

    /// <summary>
    /// Takes a System Language enum and finds a corresponding langugae code.
    /// </summary>
    /// <param name="language">
    /// System Language enum defined in the Unity Engine
    /// </param>
    /// <returns>
    /// The 2-letter ISO code from system language if the language is supported by the application.
    /// If the language is not supported English will be returned.
    /// </returns>
    public static string GetSupportedLanguageCode(SystemLanguage language)
    {
        switch (language)
        {
            case SystemLanguage.English:
                return ApplicationLocale.EN;
            case SystemLanguage.French:
                return ApplicationLocale.FR;
            default:
                return DefaultSupportedLanguageCode;
        }
    }
}
