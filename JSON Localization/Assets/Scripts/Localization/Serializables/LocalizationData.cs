using System;
using System.Collections.Generic;

namespace SimianX.Localization.Serializables
{
    /// <summary>
    /// Container for all rows of a language's localization table.
    /// Each row is comprised of a key value and a text value (standard dictionary)
    /// </summary>
    [Serializable]
    public class LocalizationData
    {
        public List<Item> items;
    }
}