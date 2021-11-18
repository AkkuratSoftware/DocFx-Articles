using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace DocFx_Articles
{
    [Serializable]
    class StructureItem
    {
        [JsonIgnore]
        public int Level { get; set; }
        public string Name { get; set; }
        public List<StructureItem> Items { get; set; }

        public void SetLevel(int level)
        {
            Level = level;
            Items.ForEach(item => item.SetLevel(level + 1));
        }

        /// <summary>
        /// Returns this object as Yaml
        /// </summary>
        /// <returns></returns>
        public string ToYaml(string path)
        {
            StringBuilder yamlBuilder = new StringBuilder();
            string thisPath = path + "/" + Name.ToLower().Replace(' ', '-');

            var indent = StaticFunctions.GetYamlIndent(Level);
            var yamlName = Name;
            var yamlHref = Name.ToLower().Replace(' ', '-') + ".md";

            yamlBuilder.AppendLine(indent + '- ' + "name: " + Name);
            yamlBuilder.AppendLine(indent + "href: " + yamlHref);

            if(Items.Count > 0)
            {
                yamlBuilder.AppendLine(indent + "items: "); 
                Items.ForEach(item => yamlBuilder.Append(item.ToYaml(thisPath)));
            }

            return yamlBuilder.ToString();
        }

        public void BuildFolders(string path)
        {
            var yamlHref = Name.ToLower().Replace(' ', '-') + ".md";
            // This is the definition so make a root directory.
            string thisPath = Path.Combine(path, Name.ToLower().Replace(' ', '-'));
            Directory.CreateDirectory(thisPath);
            File.WriteAllText(Path.Combine(thisPath, yamlHref), "## " + Name);

            Items.ForEach(item => item.BuildFolders(thisPath));
        }
    }
}
