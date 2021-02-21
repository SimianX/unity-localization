/// <summary>
/// Interface for objects that want to override the Language Manager's supported language
/// </summary>
public interface ILanguageOverrider
{
    /// <summary>
    /// Accessor for implementing instance's currently selected language code
    /// </summary>
    string SelectedLanguageCode { get; }

    /// <summary>
    /// A method that overrides the application's language when given a supported language code
    /// </summary>
    void TriggerOverride();
}
