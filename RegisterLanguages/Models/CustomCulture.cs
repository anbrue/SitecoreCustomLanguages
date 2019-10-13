namespace RegisterLanguages.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents the model of the culture to add.
    /// </summary>
    [JsonObject]
    internal class CustomCulture
    {
        #region Internal Properties 

        /// <summary>
        /// Represents the culture which this culture is 
        /// based from, e.g. "en-US".
        /// </summary>
        [JsonProperty("BaseFrom")]
        internal string BaseFrom
        {
            get;
            set;
        }

        /// <summary>
        /// e.g. "US"
        /// </summary>
        [JsonProperty("BaseFromRegion")]
        internal string BaseFromRegion
        {
            get;
            set;
        }

        /// <summary>
        /// Represents the culturname, such like "en-DE".
        /// </summary>
        [JsonProperty("CultureName")]
        internal string CultureName
        {
            get;
            set;
        }

        /// <summary>
        /// Almost the same like the culturname. 
        /// E.g. "en-DE".
        /// </summary>
        [JsonProperty("CultureLangTag")]
        internal string CultureLangTag
        {
            get;
            set;
        }

        /// <summary>
        /// The english name of the culture, 
        /// e.g. "English (Germany)".
        /// </summary>
        [JsonProperty("CultureEnglishName")]
        internal string CultureEnglishName
        {
            get;
            set;
        }

        /// <summary>
        /// The native name of the culture, 
        /// e.g. "Englisch (Deutschland)".
        /// </summary>
        [JsonProperty("NativeName")]
        internal string NativeName
        {
            get;
            set;
        }

        /// <summary>
        /// The english name of the region, 
        /// e.g. "Germany".
        /// </summary>
        [JsonProperty("RegionEnglishName")]
        internal string RegionEnglishName
        {
            get;
            set;
        }

        /// <summary>
        /// The native name of the region, 
        /// e.g. "Deutschland".
        /// </summary>
        [JsonProperty("RegionNativeName")]
        internal string RegionNativeName
        {
            get;
            set;
        }

        /// <summary>
        /// The two letters ISO Code for the 
        /// language, e.g. "en".
        /// </summary>
        [JsonProperty("LanguageCode")]
        internal string LanguageCode
        {
            get;
            set;
        }

        /// <summary>
        /// The two letters ISO Code for the 
        /// region, e.g. "DE".
        /// </summary>
        [JsonProperty("RegionCode")]
        internal string RegionCode
        {
            get;
            set;
        }

        /// <summary>
        /// The path in sitecore for the image for the
        /// flag of the language, 
        /// e.g. "flags/16x16/flag_Germany.PNG".
        /// </summary>
        [JsonProperty("SitecoreIconPath")]
        internal string SitecoreIconPath
        {
            get;
            set;
        }

        #endregion
    }
}