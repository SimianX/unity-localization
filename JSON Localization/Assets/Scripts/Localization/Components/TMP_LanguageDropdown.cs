using TMPro;
using UnityEngine;

namespace SimianX.Localization.Components
{
    /// <summary>
    /// TMP_LanguageDropdown will rebuild attached dropdown's options to reflect the application's supported language options.
    /// When attaching script to a dropdown component, ensure the OverrideLanguage method is called on its OnValueChanged UnityEvent
    /// </summary>
    [RequireComponent(typeof(TMP_Dropdown))]
    public class TMP_LanguageDropdown : MonoBehaviour, ILanguageOverrider
    {
        private TMP_Dropdown _dropdown; // Used to attached dropdown's rebuild options during scene load
                                        // and define the Selected Language Code expression with the options

        /// <summary>
        /// Accessor for implementing instance's currently selected language code
        /// </summary>
        public string SelectedLanguageCode => _dropdown.options[_dropdown.value].text;

        private void Awake()
        {
            _dropdown = GetComponent<TMP_Dropdown>();

            // Build options list from language codes
            _dropdown.ClearOptions();
            _dropdown.AddOptions(LocaleHelper.AllSupportedLanguageCodes);

            _dropdown.value = 0; // Default value

            // Search for loaded language code to derive default dropdown value
            for (int i = 0; i < _dropdown.options.Count; i++)
            {
                if (_dropdown.options[i].text.ToLower() == LocalizationManager.Instance.LoadedLanguageCode.ToLower())
                {
                    _dropdown.value = i;
                    i = _dropdown.options.Count;
                }
            }
        }

        /// <summary>
        /// A method that overrides the application's language when given a supported language code
        /// This method is designed to attach easily to the dropdown component's On Value Change event
        /// </summary>
        public void TriggerOverride()
        {
            StartCoroutine(LocalizationManager.Instance.OverrideLanguage(SelectedLanguageCode));
        }
    }
}