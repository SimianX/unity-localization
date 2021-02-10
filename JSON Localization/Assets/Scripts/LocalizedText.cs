using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalizedText : MonoBehaviour
{
    [SerializeField]
    private string localizationKey = default;
    private TextMeshProUGUI textComponent;

    public string LocalizationKey => localizationKey;

    private void Awake()
    {
        InvalidateText();
    }

    public void SetLocalizationKey(string key)
    {
        localizationKey = key;
        InvalidateText();
    }

    private void InvalidateText()
    {
        if (textComponent == null)
        {
            textComponent = GetComponent<TextMeshProUGUI>();
        }
        try
        {
            textComponent.text = LocalizationManager.Instance.GetTextForKey(localizationKey);
        }
        catch (Exception e)
        {
            // handle the exception your way
            Debug.LogError(e.Message);
            textComponent.text = "ERROR";
        }
    }
}
