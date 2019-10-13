namespace RegisterLanguages.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the cultures definitions in the 
    /// Json File.
    /// </summary>
    [JsonObject]
    internal class CultureDefinitions
    {
        #region Internal Properties 

        /// <summary>
        /// Represents the list of custom cultures, 
        /// which should be added.
        /// </summary>
        [JsonProperty("Cultures")]
        internal List<CustomCulture> Cultures
        {
            get;
            set;
        }

        #endregion
    }
}
