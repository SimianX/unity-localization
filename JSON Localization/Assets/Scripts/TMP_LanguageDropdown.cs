using TMPro;
using UnityEngine;

/// <summary>
/// TMP_LanguageDropdown will rebuild attached dropdown's options to reflect the application's supported language options.
/// When attaching script to a dropdown component, ensure the OverrideLanguage method is called on its OnValueChanged UnityEvent
/// </summary>
[RequireComponent(typeof(TMP_Dropdown))]
public class TMP_LanguageDropdown : MonoBehaviour
{
    private TMP_Dropdown _dropdown; // Used to attached dropdown's rebuild options during scene load

    // Called during scene load
    private void Awake()
    {
        _dropdown = GetComponent<TMP_Dropdown>();

        // Build options list from languagae codes
        _dropdown.ClearOptions();
        _dropdown.AddOptions(LocaleHelper.AllSupportedLanguageCodes);
    }

    /// <summary>
    /// A method that should be wired to the dropdown's On Value Changed.
    /// It sends the currently selected language code to the Localization Manager and overrides a new language.
    /// </summary>
    public void OverrideLanguage()
    {
        StartCoroutine(LocalizationManager.Instance.OverrideLanguage(_dropdown.options[_dropdown.value].text));
    }
}
