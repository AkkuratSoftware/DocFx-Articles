using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DocFx_Articles
{
    [Serializable]
    class StructureDefinition
    {
        public List<StructureItem> Items { get; set; }
        public bool BuildYaml { get; set; }

        public void SetLevel(int level)
        {
            Items.ForEach(item => item.SetLevel(level + 1));
        }

        public void BuildFolders(string path)
        {
            // This is the definition so make a root directory.
            string thisPath = Path.Combine(path, "articles");
            Directory.CreateDirectory(thisPath);
            File.WriteAllText(Path.Combine(thisPath, "articles.md"), "## Articles");

            Items.ForEach(item => item.BuildFolders(thisPath));
        }

        /// <summary>
        /// Return this structure definition as a Yaml formated representation
        /// </summary>
        /// <returns></returns>
        public string ToYaml(string path)
        {
            // Get the level string
            StringBuilder yamlBuilder = new StringBuilder();
            if (Items.Count > 0)
            {
                Items.ForEach(item => yamlBuilder.Append(item.ToYaml("")));
            }

            return yamlBuilder.ToString();

        }

        public static StructureDefinition OpenFromFile(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            var definition = JsonConvert.DeserializeObject<StructureDefinition>(jsonString);
            definition.SetLevel(1);
            return definition;
        }
    }
}
