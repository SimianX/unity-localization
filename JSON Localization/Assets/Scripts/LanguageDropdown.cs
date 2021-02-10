using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Dropdown))]
public class LanguageDropdown : MonoBehaviour
{
    private TMP_Dropdown _dropdown;

    // Called during scene load
    private void Awake()
    {
        _dropdown = GetComponent<TMP_Dropdown>();

        // Build options list from langugae codes
        _dropdown.ClearOptions();
        _dropdown.AddOptions(LocaleHelper.AllSupportedLanguageCodes);
    }

    /// <summary>
    /// A method that should be wired to the dropdown's On Value Changed.
    /// It sends the currently selected language code to the Localization Manager and overrides a new language.
    /// </summary>
    public void OverrideLanguage()
    {
        LocalizationManager.Instance.OverrideLanguage(_dropdown.options[_dropdown.value].text);
    }
}
