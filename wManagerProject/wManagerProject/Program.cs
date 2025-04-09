using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Text.Json;


namespace wManagerProject
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string filepath = @"C:\Users\bobyu\Documents\code\sCode\cSharp2025\wManagerProject\wManagerProject\WData\Warframe.json";
            //string json = File.ReadAllText(filepath);
            // covert json to object
            //Warframe warframe = JsonSerializer.Deserialize<Warframe>(json);
            //Root warframeData = JsonSerializer.Deserialize<Root>(json);
            //Warframe warframe = JsonConvert.DeserializeObject<Warframe>(jsonInput);
            //now print all the objects info
            // make a function the loads everything from the json file
            static void LoadCharacters(string filepath)
            {
                string json = File.ReadAllText(filepath);
                Root warframeData = JsonSerializer.Deserialize<Root>(json);
                foreach (var Warframe in warframeData.Warframes)
                {
                    Console.WriteLine();
                    Console.WriteLine($" {Warframe.name}: {Warframe.date}" +
                                      $"\n     Polarized: {Warframe.polarized}" +
                                      $"\n      Catalyst: {Warframe.OrokinCatalysts}" +
                                      $"\n        Exilus: {Warframe.Exilus}");
                    Console.WriteLine();
                }
            }
            static void AddNewChar(string filepath)
            {
                Console.WriteLine("Enter the name of the new Warframe:");
                string name = Console.ReadLine();

                Console.WriteLine("Enter the date of the new character (yyyy-mm-dd):");
                string date = Console.ReadLine();

                Console.WriteLine("Enter the polarized value of the new character:");
                int polarized = int.Parse(Console.ReadLine());

                Console.WriteLine("Is it a catalyst? (true/false):");
                bool OrokinCatalysts = bool.Parse(Console.ReadLine());

                Console.WriteLine("Is it an Exilus? (true/false):");
                bool Exilus = bool.Parse(Console.ReadLine());

                // Create a new Warframe object
                Warframe nWarframe = new Warframe
                {
                    name = name,
                    date = date,
                    polarized = polarized,
                    OrokinCatalysts = OrokinCatalysts,
                    Exilus = Exilus
                };
                // Load characters
                string json = File.ReadAllText(filepath);
                Root warframeData = JsonSerializer.Deserialize<Root>(json);

                // Add new character to the list
                warframeData.Warframes.Add(nWarframe);
                // Save back to the json file
                string upJson = JsonSerializer.Serialize(warframeData, new JsonSerializerOptions {WriteIndented=true});
                File.WriteAllText(filepath, upJson);
                Console.WriteLine("New character added successfully!");
            }


            // make a method that will remove a character from the json file
            static void RemoveChar(string filepath)
            {
                Console.WriteLine("Enter the name of the character to remove:");
                string name = Console.ReadLine();
                // Load characters
                string json = File.ReadAllText(filepath);
                Root warframeData = JsonSerializer.Deserialize<Root>(json);
                // Find and remove the character
                // "Warframe" characterToRemove is explicit telling the compiler that this is a Warframe object
                // "var" charaterToRemove is implicit the compiler will figure out the type
                // var characterToRemove = warframeData.Warframes.Find(...);
                // .Find() return a Warframe so now the compiler knows that var is a warframe type
                Warframe characterToRemove = null;
                foreach (Warframe warframe in warframeData.Warframes)
                {
                    //https://www.c-sharpcorner.com/blogs/compare-strings-using-stringcomparisonordinalignorecase1
                    // StringComparison.OrdinalIgnoreCase compares 2 strings ignoring case
                    if (warframe.name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    {
                        characterToRemove = warframe;
                        break; // Stop once we find the first match
                    }
                }

                if (characterToRemove != null)
                {
                    warframeData.Warframes.Remove(characterToRemove);
                    // Save json file
                    string reJson = JsonSerializer.Serialize(warframeData, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(filepath, reJson);
                    Console.WriteLine($"Removed {name} warframe");
                }
                else
                {
                    Console.WriteLine("Character not found.");
                }
            }



            while (true)
            {
                Console.WriteLine("\n=== Game Character Manager ===");
                Console.WriteLine("1. Load characters");
                Console.WriteLine("2. Add new character");
                Console.WriteLine("3. Remove");
                Console.WriteLine("4. Quit");
                Console.Write("Choose an option (1-4): ");

                string choice = Console.ReadLine().Trim();

                switch (choice)
                {
                    case "1":
                        LoadCharacters(filepath);
                        break;
                    case "2":
                        AddNewChar(filepath);
                        break;
                    case "3":
                        RemoveChar(filepath);
                        break;
                    case "4":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid input. Try again.");
                        break;
                }
            }
        }
    }
}

////string path = @"C:\Users\weixu\Downloads\Warframe\3741262_SleepNue_GDPR_Warframe.txt";
//string section = "subFirst";
////string Filepath = @"C:\Users\bobyu\Documents\code\sCode\cSharp2025\wManagerProject\wManagerProject\WData";
//string Filepath = @"C:\Users\bobyu\Documents\code\sCode\cSharp2025\wManagerProject\wManagerProject\WData\Warframe.txt";

////string[] dirs = Directory.GetDirectories(Filepath,"*", SearchOption.AllDirectories);

////foreach (string dir in dirs) 
////{
////    Console.WriteLine(dir);
////}

////var files = Directory.GetFiles(Filepath,"*.*",SearchOption.AllDirectories);

////foreach (var file in files) 
////{
////    //Console.WriteLine(file);
////    var info = new FileInfo(file);
////    Console.WriteLine($"{ Path.GetFileName(file) }: {info.Length} bytes");
////}
////Console.WriteLine();
//bool inSection = false;
//StringBuilder content = new StringBuilder();
//int setIndentLvl = -1;

//if (File.Exists(Filepath))
//{
//    using (StreamReader reader = new StreamReader(Filepath))
//    {
//        string line;
//        while ((line = reader.ReadLine()) != null)
//        {
//            Console.WriteLine("1");
//            int indentLvl = line.TakeWhile(c => c == '\t').Count();

//            Console.WriteLine();
//            Console.WriteLine(line);
//            string trimLine = line.Trim();
//            Console.WriteLine($"##{setIndentLvl}");

//            if (trimLine == section)
//            {
//                Console.WriteLine("2");
//                inSection = true;
//                // add 1 per indent level
//                setIndentLvl = indentLvl + 1;
//                continue;
//            }

//            if (inSection)
//            {
//                Console.WriteLine("3");
//                if (indentLvl >= setIndentLvl)
//                {
//                    Console.WriteLine("4");
//                    // trimline seems to be empty
//                    content.AppendLine(trimLine);
//                }
//                else
//                {
//                    Console.WriteLine("5");
//                    break;
//                }
//            }
//        }
//    }

//    if (content.Length > 0)
//    {
//        Console.WriteLine("6");
//        Console.WriteLine($"Content: \n{content}");
//    }
//    else
//        Console.WriteLine("Not found");
//}
//else
//{
//    var files = Directory.GetFiles(Filepath, "*.*", SearchOption.AllDirectories);

//    foreach (var file in files) 
//    {
//        //Console.WriteLine(file);
//        var info = new FileInfo(file);
//        Console.WriteLine($"{ Path.GetFileName(file) }: {info.Length} bytes");
//    }
//    Console.WriteLine();
//    Console.WriteLine("file not found");
//}
