namespace RegisterLanguages
{
    using System;
    using System.Configuration;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using Newtonsoft.Json;
    using RegisterLanguages.Models;

    /// <summary>
    /// Represents the tool, which adds custom languages to the system 
    /// and languagedefinitions for sitecore.
    /// </summary>
    internal class Program
    {
        #region Private Static Methods 

        /// <summary>
        /// Runs the programm and tries to add the languages which 
        /// are defined in the configured Json File.
        /// </summary>
        /// <param name="args">
        ///  Not needed.
        /// </param>
        private static void Main(string[] args)
        {
            string configPath =
                ConfigurationManager.AppSettings["ConfigPath"];

            if (string.IsNullOrWhiteSpace(configPath))
            {
                Console.WriteLine(
                    "You have to define the path to the config in the app.config!");

                return;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(configPath);

            XmlNode languagesElement =
                doc.SelectSingleNode("configuration/languages");

            bool xmlHasChanges = false;

            string jsonPath = ConfigurationManager.AppSettings["JsonPath"];

            if (string.IsNullOrWhiteSpace(configPath))
            {
                Console.WriteLine(
                    "You have to define the path to the json for defining the cultures to add!");

                return;
            }

            string json = File.ReadAllText(jsonPath);

            CultureDefinitions definitions =
                JsonConvert.DeserializeObject<CultureDefinitions>(json);

            foreach (CustomCulture culture in definitions.Cultures)
            {
                bool isAlreadyRegistered = false;

                Console.WriteLine(
                    "Try to register language '{0}' on the system.",
                    culture.CultureName);

                CultureAndRegionInfoBuilder cib = null;

                try
                {
                    cib = new CultureAndRegionInfoBuilder(
                        culture.CultureName, CultureAndRegionModifiers.None);

                    cib.LoadDataFromCultureInfo(new CultureInfo(culture.BaseFrom));
                    cib.LoadDataFromRegionInfo(new RegionInfo(culture.BaseFromRegion));
                    cib.CultureEnglishName = culture.CultureEnglishName;
                    cib.CultureNativeName = culture.NativeName;
                    cib.IetfLanguageTag = culture.CultureLangTag;
                    cib.RegionEnglishName = culture.RegionEnglishName;
                    cib.RegionNativeName = culture.RegionNativeName;
                    cib.Register();

                    Console.WriteLine(cib.CultureName + " => created");
                }
                catch (InvalidOperationException invalidOpExp)
                {
                    Console.WriteLine(
                        "The language is already registered: {0}",
                        culture.CultureName);

                    isAlreadyRegistered = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                if (!isAlreadyRegistered)
                {
                    bool isAdded = languagesElement.ChildNodes.Cast<XmlNode>()
                        .Any(x => x.Attributes["id"].Value == culture.LanguageCode &&
                                x.Attributes["region"].Value == culture.RegionCode);

                    if (!isAdded)
                    {
                        XmlNode newLang = languagesElement.FirstChild.CloneNode(false);
                        newLang.Attributes["id"].Value = culture.LanguageCode;
                        newLang.Attributes["region"].Value = culture.RegionCode;
                        newLang.Attributes["icon"].Value = culture.SitecoreIconPath;
                        languagesElement.AppendChild(newLang);
                        xmlHasChanges = true;

                        Console.WriteLine(
                            "language '{0}' is added in the LanguageDefinitions.config",
                            culture.CultureName);
                    }
                    else
                    {
                        Console.WriteLine(
                            "language '{0}' is already defined in the LanguageDefinitions.config",
                            culture.CultureName);
                    }

                }
            }

            if (xmlHasChanges)
            {
                doc.Save(configPath);
            }

            Console.WriteLine("The program has ended, see the output above.");
        }

        #endregion
    }

}
