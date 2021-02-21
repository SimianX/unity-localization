public interface ILocalizable
{
    string LocalizationKey { get; }
    void SetLocalizationKey(string key);
    void LocalizeComponent();
}
