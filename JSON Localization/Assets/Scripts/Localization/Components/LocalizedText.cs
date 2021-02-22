using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// LocalizedText will change the contents of a Text element to match the string value corresponding to the Localization Key.
/// If the Localization Key is invalid, the componenet will not replace any text
/// </summary>
[RequireComponent(typeof(Text))]
public class LocalizedText : MonoBehaviour, ILocalizable
{
    [SerializeField]
    private string localizationKey = default;

    public string LocalizationKey => localizationKey;

    private Text _textComponent;

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
    /// When the Localization Manager Instance signal's that the application language has been overriden, re-localize text
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
    /// If it fails, it will NOT modify the text component's content value
    /// </summary>
    public void LocalizeComponent()
    {
        if (_textComponent == null) // Lazy component assignment (might want to make it strict)
        {
            _textComponent = GetComponent<Text>();
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
