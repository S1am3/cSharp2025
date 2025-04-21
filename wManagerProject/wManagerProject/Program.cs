using System;
using System.IO;
using System.Text.Json;


namespace wManagerProject
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // might remove date created from any of the option and data
            // need to change file path when testing
            string filepath = @"C:\Users\bobyu\Documents\code\sCode\cSharp2025\wManagerProject\wManagerProject\WData\Warframe.json";

            while (true)
            {
                PrintMenu();

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
                        UpdateChar(filepath);
                        break;
                    case "4":
                        RemoveChar(filepath);
                        break;
                    case "5":
                        FilterWarframe(filepath);
                        break;
                    case "6":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid input. Try again.");
                        break;
                }
            }
        }

        //Create
        static void AddNewChar(string filepath)
        {
            Console.WriteLine("Enter the name of the new Warframe:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter the date of the new character (yyyy-mm-dd):");
            string date = Console.ReadLine();

            Console.WriteLine("Enter the polarized value of the new character:");
            int polarized = IsValidInt(Console.ReadLine());

            Console.WriteLine("Does it have an Orokin Catalyst equipped? (true/false):");
            bool OrokinCatalysts = IsValid(Console.ReadLine());

            Console.WriteLine("Does it have an Orokin Reactors equipped? (true/false): ");
            bool OrokinReactors = IsValid(Console.ReadLine());

            Console.WriteLine("Is it an Exilus? (true/false):");
            bool Exilus = IsValid(Console.ReadLine());

            // Create a new Warframe object
            Warframe nWarframe = new Warframe
            {
                name = name,
                date = date,
                polarized = polarized,
                OrokinCatalysts = OrokinCatalysts,
                OrokinReactors = OrokinReactors,
                Exilus = Exilus
            };
            // Load
            string json = File.ReadAllText(filepath);
            Root warframeData = JsonSerializer.Deserialize<Root>(json);

            // Add new character to the list
            warframeData.Warframes.Add(nWarframe);
            // Save
            string upJson = JsonSerializer.Serialize(warframeData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filepath, upJson);
            Console.WriteLine("\nNew character added successfully!");
        }

        //Read
        static void LoadCharacters(string filepath)
        {
            // Load characters
            string json = File.ReadAllText(filepath);
            Root warframeData = JsonSerializer.Deserialize<Root>(json);
            // loop foreach character
            foreach (var Warframe in warframeData.Warframes)
            {
                Console.WriteLine();
                Console.WriteLine($"{Warframe.name}: {Warframe.date}" +
                                  $"\n      Polarized: {Warframe.polarized}" +
                                  $"\nOrokin Catalyst: {Warframe.OrokinCatalysts}" +
                                  $"\n Orokin Reactor: {Warframe.OrokinReactors}" +
                                  $"\n         Exilus: {Warframe.Exilus}");
                Console.WriteLine();
            }
        }

        //Update
        static void UpdateChar(string filepath)
        {
            Console.WriteLine("Enter the name of the character to update:");
            string name = Console.ReadLine();
            // Load characters
            string json = File.ReadAllText(filepath);
            Root warframeData = JsonSerializer.Deserialize<Root>(json);
            // Find charater based on name
            Warframe cUpdate = null;
            foreach (Warframe warframe in warframeData.Warframes)
            {
                if (warframe.name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    cUpdate = warframe;
                    break;
                }
            }
            if (cUpdate != null)
            {
                Console.WriteLine("Enter the new date:");
                string date = Console.ReadLine();
                Console.WriteLine("Enter the number of Polarized: ");
                int polarized = IsValidInt(Console.ReadLine());
                Console.WriteLine("Has Orokin Catalysts?: ");
                bool OrokinCatalysts = IsValid(Console.ReadLine());
                Console.WriteLine("Has Orokin Reactors?: ");
                bool OrokinReactors = IsValid(Console.ReadLine());
                Console.WriteLine("Has Exilus?: ");
                bool Exilus = IsValid(Console.ReadLine());
                // update
                cUpdate.date = date;
                cUpdate.polarized = polarized;
                cUpdate.OrokinCatalysts = OrokinCatalysts;
                cUpdate.OrokinReactors = OrokinReactors;
                cUpdate.Exilus = Exilus;
                // Save
                string reJson = JsonSerializer.Serialize(warframeData, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filepath, reJson);
                Console.WriteLine($"Updated {name} warframe");
            }
            else
            {
                Console.WriteLine("\nCharacter not found.");
            }
        }

        //Delete
        static void RemoveChar(string filepath)
        {
            Console.WriteLine("Enter the name of the character to remove:");
            string name = Console.ReadLine();
            // Load
            string json = File.ReadAllText(filepath);
            Root warframeData = JsonSerializer.Deserialize<Root>(json);
            // Find and remove the character
            // "Warframe" characterToRemove is explicit telling the compiler that this is a Warframe object
            // "var" charaterToRemove is implicit the compiler will figure out the type
            // var characterToRemove = warframeData.Warframes.Find(...);
            // .Find() return a Warframe so now the compiler knows that var is a warframe type
            Warframe cRemove = null;
            foreach (Warframe warframe in warframeData.Warframes)
            {
                //https://www.c-sharpcorner.com/blogs/compare-strings-using-stringcomparisonordinalignorecase1
                // StringComparison.OrdinalIgnoreCase compares 2 strings ignoring case
                if (warframe.name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {

                    cRemove = warframe;
                    break;
                }
            }

            if (cRemove != null)
            {
                warframeData.Warframes.Remove(cRemove);
                // Save json file
                string reJson = JsonSerializer.Serialize(warframeData, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filepath, reJson);
                Console.WriteLine($"\nRemoved {name} warframe");
            }
            else
            {
                Console.WriteLine("\nCharacter not found.");
            }
        }

        // Filter Warframe based on user input
        static void FilterWarframe(string filepath)
        {
            PrintOption();
            string filterType = Console.ReadLine();
            Console.WriteLine("Enter the value to filter by:");
            string filterValue = Console.ReadLine();

            // load
            string json = File.ReadAllText(filepath);
            Root warframeData = JsonSerializer.Deserialize<Root>(json);

            // make the new list
            List<Warframe> filteredWarframe = new List<Warframe>();

            foreach (Warframe warframe in warframeData.Warframes)
            {
                //check what to filter
                switch (filterType.ToLower())
                {
                    case "1":
                        // StringComparison.OrdinalIgnoreCase compares 2 strings ignoring case
                        if (warframe.name.Equals(filterValue, StringComparison.OrdinalIgnoreCase))
                            // add append to the filteredWarframe list
                            filteredWarframe.Add(warframe);
                        break;
                    case "2":
                        if (warframe.date.Equals(filterValue, StringComparison.OrdinalIgnoreCase))
                            filteredWarframe.Add(warframe);
                        break;
                    case "3":
                        if (warframe.polarized.ToString().Equals(filterValue, StringComparison.OrdinalIgnoreCase))
                            filteredWarframe.Add(warframe);
                        break;
                    case "4":
                        if (warframe.OrokinCatalysts.ToString().Equals(filterValue, StringComparison.OrdinalIgnoreCase))
                            filteredWarframe.Add(warframe);
                        break;
                    case "5":
                        if (warframe.OrokinReactors.ToString().Equals(filterValue, StringComparison.OrdinalIgnoreCase))
                            filteredWarframe.Add(warframe);
                        break;
                    case "6":
                        if (warframe.Exilus.ToString().Equals(filterValue, StringComparison.OrdinalIgnoreCase))
                            filteredWarframe.Add(warframe);
                        break;
                    default:
                        Console.WriteLine("Invalid filter option.");
                        return;
                }
            }
            // shows the filtered resultes
            Console.WriteLine($"\nFiltered Results ({filteredWarframe.Count}):");
            // run per item in list
            foreach (Warframe warframe in filteredWarframe)
            {
                Console.WriteLine($"{warframe.name} - Polarized: {warframe.polarized}, Catalysts: {warframe.OrokinCatalysts}, Reactors: {warframe.OrokinReactors}, Exilus: {warframe.Exilus}");
            }

        }

        //checks inputs
        static bool IsValid(string p)
        {
            bool result = false;
            bool valid = false;

            while (!valid)
            {
                //Console.WriteLine(p);


                if (bool.TryParse(p, out result))
                    valid = true;
                else
                    Console.WriteLine("Invalid input. Pick from (true/false)");
                p = Console.ReadLine().ToLower();
            }
            return result;
        }
        static int IsValidInt(string p)
        {
            int result = 0;
            bool valid = false;

            while (!valid)
            {
                if (int.TryParse(p, out result))
                {
                    if (result >= 0)
                        valid = true;
                    else
                    {
                        Console.WriteLine("Negative is a no go number:");
                        p = Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number:");
                    p = Console.ReadLine();
                }
            }

            return result;
        }

        // string menu option
        static void PrintMenu()
        {
            Console.WriteLine("\n=== Game Character Manager ===");
            Console.WriteLine("1. Load characters");
            Console.WriteLine("2. Add new character");
            Console.WriteLine("3. Update character");
            Console.WriteLine("4. Remove");
            Console.WriteLine("5. Filter Characters");
            Console.WriteLine("6. Quit");
            Console.Write("Choose an option (1-6): ");
        }
        static void PrintOption()
        {
            Console.WriteLine("\nFilter Option");
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Date");
            Console.WriteLine("3. Polarized");
            Console.WriteLine("4. Orokin Catalysts");
            Console.WriteLine("5. Orokin Reactors");
            Console.WriteLine("6. Exilus");
        }
    }
}


// for me only will remove when submitting

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
