using System;
using TMPro;
using UnityEngine;

/// <summary>
/// TMP_LocalizedText will change the contents of a TMP_Text element to match the string value corresponding to the Localization Key.
/// If the Localization Key is invalid, the componenet will not replace any text
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class TMP_LocalizedText : MonoBehaviour, ILocalizable
{
    [SerializeField]
    private string localizationKey = default;

    /// <summary>
    /// Getter for component's localization key
    /// </summary>
    public string LocalizationKey => localizationKey;

    private TextMeshProUGUI _textComponent;

    private void Awake()
    {
        LocalizeComponent();
    }

    private void OnEnable()
    {
        LocalizationManager.Instance.OnLanguageOverride += Instance_OnLanguageOverride;
    }

    private void OnDisable()
    {
        LocalizationManager.Instance.OnLanguageOverride -= Instance_OnLanguageOverride;
    }

    /// <summary>
    /// When the Localization Manager Instance signal's that the application language has been overriden, re-validate text
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Instance_OnLanguageOverride(object sender, EventArgs e)
    {
        LocalizeComponent();
    }

    /// <summary>
    /// Setter for the Localization Key.
    /// Will automatically retrieve and set corresponding text from the Localization Manager
    /// </summary>
    /// <param name="key"></param>
    public void SetLocalizationKey(string key)
    {
        localizationKey = key;
        LocalizeComponent();
    }

    /// <summary>
    /// Will attempt to retrieve text from Localization Manager Instance.
    /// If it fails, print exception without modifying text component's content value
    /// </summary>
    public void LocalizeComponent()
    {
        if (_textComponent == null) // Lazy Text Component assignment (Might want to make it strict)
        {
            _textComponent = GetComponent<TextMeshProUGUI>();
        }

        try // Attempt to retrieve text from Localization Manager Instance
        {
            _textComponent.text = LocalizationManager.Instance.GetTextForKey(localizationKey);
        }
        catch (Exception e) // Print Exception in editor
        {
            Debug.LogError(e.Message);
        }
    }
}
