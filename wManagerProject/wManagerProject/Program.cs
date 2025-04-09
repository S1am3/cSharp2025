using System;
using System.IO;
using System.Text;

namespace wManagerProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //string path = @"C:\Users\weixu\Downloads\Warframe\3741262_SleepNue_GDPR_Warframe.txt";
            string section = "subFirst";
            //string Filepath = @"C:\Users\bobyu\Documents\code\sCode\cSharp2025\wManagerProject\wManagerProject\WData";
            string Filepath = @"C:\Users\bobyu\Documents\code\sCode\cSharp2025\wManagerProject\wManagerProject\WData\Warframe.txt";

            //string[] dirs = Directory.GetDirectories(Filepath,"*", SearchOption.AllDirectories);

            //foreach (string dir in dirs) 
            //{
            //    Console.WriteLine(dir);
            //}

            //var files = Directory.GetFiles(Filepath,"*.*",SearchOption.AllDirectories);

            //foreach (var file in files) 
            //{
            //    //Console.WriteLine(file);
            //    var info = new FileInfo(file);
            //    Console.WriteLine($"{ Path.GetFileName(file) }: {info.Length} bytes");
            //}
            //Console.WriteLine();
            bool inSection = false;
            StringBuilder content = new StringBuilder();
            int setIndentLvl = -1;

            if (File.Exists(Filepath))
            {
                using (StreamReader reader = new StreamReader(Filepath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Console.WriteLine("1");
                        int indentLvl = line.TakeWhile(c => c == '\t').Count();

                        Console.WriteLine();
                        Console.WriteLine(line);
                        string trimLine = line.Trim();
                        Console.WriteLine($"##{setIndentLvl}");

                        if (trimLine == section)
                        {
                            Console.WriteLine("2");
                            inSection = true;
                            // add 1 per indent level
                            setIndentLvl = indentLvl + 1;
                            continue;
                        }

                        if (inSection)
                        {
                            Console.WriteLine("3");
                            if (indentLvl >= setIndentLvl)
                            {
                                Console.WriteLine("4");
                                // trimline seems to be empty
                                content.AppendLine(trimLine);
                            }
                            else
                            {
                                Console.WriteLine("5");
                                break;
                            }
                        }
                    }
                }

                if (content.Length > 0)
                {
                    Console.WriteLine("6");
                    Console.WriteLine($"Content: \n{content}");
                }
                else
                    Console.WriteLine("Not found");
            }
            else
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
                Console.WriteLine();
                Console.WriteLine("file not found");
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
