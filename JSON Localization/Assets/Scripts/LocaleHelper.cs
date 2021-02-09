using UnityEngine;

public static class LocaleHelper
{
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
        SystemLanguage lang = Application.systemLanguage;

        switch (lang)
        {
            case SystemLanguage.English:
                return ApplicationLocale.EN;
            case SystemLanguage.French:
                return ApplicationLocale.FR;
            default:
                return GetDefaultSupportedLanguageCode();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>
    /// 
    /// </returns>
    public static string GetDefaultSupportedLanguageCode()
    {
        return ApplicationLocale.EN;
    }
}
