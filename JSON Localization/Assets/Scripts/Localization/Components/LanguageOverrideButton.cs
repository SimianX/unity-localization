using UnityEngine;
using UnityEngine.UI;

namespace SimianX.Localization.Components
{
    /// <summary>
    /// Component that supplies a TriggerOverride method that, when called, will trigger a language override with its specified language code
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class LanguageOverrideButton : MonoBehaviour, ILanguageOverrider
    {
        [SerializeField]
        private string languageCode; // Manually set language code in editor

        /// <summary>
        /// Accessor for implementing instance's currently selected language code.
        /// Button selection is defined from the component's Unity editor field
        /// </summary>
        public string SelectedLanguageCode => languageCode;

        /// <summary>
        /// A method that overrides the application's language when given a supported language code.
        /// Designed to easily attach to the button's componenent's On Click event
        /// </summary>
        public void TriggerOverride()
        {
            StartCoroutine(LocalizationManager.Instance.OverrideLanguage(SelectedLanguageCode));
        }
    }
}