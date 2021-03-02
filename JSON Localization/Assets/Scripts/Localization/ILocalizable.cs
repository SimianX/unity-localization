public interface ILocalizable
{
    /// <summary>
    /// Accessor for component's localization key
    /// </summary>
    string LocalizationKey { get; }

    /// <summary>
    /// Setter for the Localization Key.
    /// Will automatically retrieve and set corresponding text from the Localization Manager
    /// </summary>
    /// <param name="key"></param>
    void SetLocalizationKey(string key);

    /// <summary>
    /// Will attempt to retrieve text from Localization Manager Instance.
    /// If it fails, it will NOT modify the asset in any way
    /// </summary>
    void LocalizeComponent();
}
