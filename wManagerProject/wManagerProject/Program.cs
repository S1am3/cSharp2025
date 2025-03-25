using System;
using System.IO;
using System.Text;

namespace wManagerProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // home pc
            //string Filepath = @"C:\Users\weixu\Downloads\Warframe\3741262_SleepNue_GDPR_Warframe.txt";
            string section = "WARFRAMES";
            // home pc
            string Filepath = @"C:\Users\weixu\Documents\cSharp2025\wManagerProject\wManagerProject\WData\Warframe.txt";
            // school pc
            //string Filepath = @"C:\Users\bobyu\Documents\code\sCode\cSharp2025\wManagerProject\wManagerProject\WData\Warframe.txt";

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
                        //Console.WriteLine("1");
                        int indentLvl = line.TakeWhile(c => c == '\t' || c == ' ').Count();

                        Console.WriteLine();
                        Console.WriteLine(line);
                        string trimLine = line.Trim();
                        Console.WriteLine($"#{setIndentLvl}");

                        if (trimLine == section)
                        {
                            Console.WriteLine($"Found section: {section}");

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
                                if (!string.IsNullOrEmpty(trimLine)) // Ignore empty lines
                                {
                                    //Console.WriteLine("4");
                                    content.AppendLine(trimLine);

                                }
                            }
                            else
                            {
                                Console.WriteLine("5");
                                Console.WriteLine($"Exitiing : {section}");
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
                var files = Directory.GetFiles(Filepath, "*.*", SearchOption.AllDirectories);

                foreach (var file in files) 
                {
                    //Console.WriteLine(file);
                    var info = new FileInfo(file);
                    Console.WriteLine($"{ Path.GetFileName(file) }: {info.Length} bytes");
                }
                Console.WriteLine();
                Console.WriteLine("file not found");
            }

        }
    }
}
