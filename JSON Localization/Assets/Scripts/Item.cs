/// <summary>
/// Represents a single row in a language's localization table.
/// Each row is comprised of a key value and a text value (standard dictionary)
/// </summary>
[System.Serializable]
public class Item
{
    /// <summary>
    /// Unique value distinguising a row
    /// </summary>
    public string key;

    /// <summary>
    /// Text value associsated with key
    /// </summary>
    public string value;
}
