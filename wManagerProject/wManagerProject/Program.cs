using System;
using System.IO;
using System.Text;

namespace wManagerProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\weixu\Downloads\Warframe\3741262_SleepNue_GDPR_Warframe.txt";

            if (File.Exists(path))
            {
                Console.WriteLine($"{path}");
            }
            else 
            {
                Console.WriteLine($"File NOT found at: {path}");
            }
        }
    }
}
