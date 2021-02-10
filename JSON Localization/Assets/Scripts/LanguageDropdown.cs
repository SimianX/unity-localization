using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Dropdown))]
public class LanguageDropdown : MonoBehaviour
{
    private TMP_Dropdown _dropdown;

    private void Awake()
    {
        _dropdown = GetComponent<TMP_Dropdown>();

        // Build options list from langugae codes
        _dropdown.ClearOptions();
        _dropdown.AddOptions(LocaleHelper.AllSupportedLanguageNamesEN);
    }

    public void OverrideLanguage()
    {
        //LocalizationManager.Instance.OverrideLangugae();
    }
}
