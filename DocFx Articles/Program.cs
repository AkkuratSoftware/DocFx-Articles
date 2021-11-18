using System;
using System.IO;

namespace DocFx_Articles
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.WriteLine("DocFx Article Structure Builder");
            Console.WriteLine("");

            Console.WriteLine("Enter the root directory:");

            string root = Console.ReadLine();
            Console.WriteLine("");

            Console.WriteLine("Using {0} as the root directory.", root);

            Directory.CreateDirectory(Path.Combine(root, "articles"));
            string inputPath = Path.Combine(root, "StructureDefinition.json");
            string outputPath = Path.Combine(root, "articles", "toc.yml");

            var sd = StructureDefinition.OpenFromFile(inputPath);
            File.WriteAllText(outputPath, sd.ToYaml(""));

            Console.WriteLine("Yaml written to {0}", outputPath);

            Console.WriteLine("Press any key to create folders");

            Console.ReadKey();

            sd.BuildFolders(root);

        }
    }
}
