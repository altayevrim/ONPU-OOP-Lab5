using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Lab4
{
    public interface IServant //Change this name after.
    {
        delegate void MyHandler(string message); //(for .NET Core 3.x)
        event MyHandler Notify;
        string[] ReadFileArray();

        string ReadFileAll();
        void WriteLine(string swearingWord);
        void WriteAll(string newText);
    }

    class RimFile : IServant
    {
        public event IServant.MyHandler Notify; //public event MyHandler Notify; (for .NET Core 2.x)
        protected string filePath;
        public string FilePath
        {
            get => this.filePath;
            set => this.filePath = value;
        }
        public string[] ReadFileArray()
        {
            if (File.Exists(this.filePath))
            {

                Notify?.Invoke("File opened and data converted to Array: " + this.FilePath);
                string[] lines;
                lines = File.ReadAllLines(this.filePath);
                return lines;
            }
            else
            {
                throw new Exception("Error: File not found!");
            }
        }

        public string ReadFileAll()
        {
            if (File.Exists(this.filePath))
            {
                Notify?.Invoke("File opened and data converted to string: " + this.FilePath);
                string lines;
                lines = File.ReadAllText(this.filePath);
                return lines;
            }
            else
            {
                throw new Exception("Error: File not found!");
            }
        }

        public void WriteLine(string swearingWord)
        {
            if (File.Exists(this.filePath))
            {
                using (StreamWriter sr = File.AppendText(this.filePath))
                {
                    Notify?.Invoke("File opened and a new line added to the file: " + this.FilePath);
                    sr.WriteLine(swearingWord);
                    sr.Close();

                    Console.WriteLine(File.ReadAllText(this.filePath));
                }
            }
            else
            {
                throw new Exception("Error: File not found!");
            }
        }

        public void WriteAll(string text)
        {
            if (File.Exists(this.filePath))
            {
                Notify?.Invoke("File opened and all data changed: " + this.FilePath);
                File.WriteAllText(this.filePath, text);
            }
            else
            {
                throw new Exception("Error: File not found!");
            }
        }
    }

    class FirstCheck
    {
        public FirstCheck()
        {
            RimFile evrim = new RimFile();
            RimFile altay = new RimFile();
            evrim.Notify += DisplayMessage;
            altay.Notify += DisplayMessage;
            evrim.FilePath = @"/home/rimtay/Documents/_CS Projects/Lab4/deneme.txt";
            altay.FilePath = @"/home/rimtay/Documents/_CS Projects/Lab4/checkwords.txt";
            string text = evrim.ReadFileAll();
            string[] checkLines = altay.ReadFileArray();

            string newText = FilterSwear(text, checkLines);
            Console.WriteLine("\n\nThe Text:");
            Console.WriteLine(newText);
            evrim.WriteAll(newText);
        }
        public string FilterSwear(string text, string[] swearingWords)
        {
            string filtered = text;
            bool foundOnce = false;
            for (int i = 0; i < swearingWords.Length; i++)
            {
                if (text.Contains(swearingWords[i]))
                {
                    foundOnce = true;
                    Console.WriteLine("Word Found '{0}'", swearingWords[i]);
                    int chars = swearingWords[i].Length;
                    string stars = "";
                    for (int a = 0; a < chars; a++)
                    {
                        stars += "*";
                    }
                    filtered = filtered.Replace(swearingWords[i], stars);
                }
            }
            if (foundOnce == false)
            {
                Console.WriteLine("No changes have been made!");
            }
            return filtered;
        }
        private void DisplayMessage(string message)
        {
            Console.WriteLine(message + " (method handler)");
        }
    }

    class SecondCheck
    {
        public SecondCheck()
        {
            RimFile altay = new RimFile();
            string text = GetText();
            altay.FilePath = @"/home/rimtay/Documents/_CS Projects/Lab4/checkwords.txt";
            string[] checkLines = altay.ReadFileArray();
            string newText = FilterSwear(text, checkLines);
            Console.WriteLine("\n\nThe Text:");
            Console.WriteLine(newText);
        }

        public string GetText()
        {
            string text = "";

            do
            {
                Console.WriteLine("Please enter a text to control:");
                text = Console.ReadLine();
            } while (text == "");

            return text;
        }
        public string FilterSwear(string text, string[] swearingWords)
        {
            string filtered = text;
            bool foundOnce = false;
            for (int i = 0; i < swearingWords.Length; i++)
            {
                if (text.Contains(swearingWords[i]))
                {
                    foundOnce = true;
                    Console.WriteLine("Word Found '{0}'", swearingWords[i]);
                    int chars = swearingWords[i].Length;
                    string stars = "";
                    for (int a = 0; a < chars; a++)
                    {
                        stars += "*";
                    }
                    filtered = filtered.Replace(swearingWords[i], stars);
                }
            }
            if (foundOnce == false)
            {
                Console.WriteLine("No changes have been made!");
            }
            return filtered;
        }
        private void DisplayMessage(string message)
        {
            Console.WriteLine(message + " (method handler)");
        }
    }

    class ThirdCheck
    {
        public ThirdCheck()
        {
            Console.WriteLine("Silence is gold.");
        }
    }

    class Service {
        public void Menu(){
            bool run = true;
            ConsoleKeyInfo call;
            while (run)
            {
                Console.WriteLine("==================================================");
                Console.WriteLine("Please choose an action.");
                call = Console.ReadKey();
                Console.WriteLine("\r ");
                if (call.Key == ConsoleKey.Escape)
                {
                    run = false;
                    Console.WriteLine("Bye.");
                    break;
                }
                switch (call.Key.ToString())
                {
                    case "F":
                        CallFirst();
                        break;
                    case "S":
                        CallSecond();
                        break;
                    case "T":
                        CallThird();
                        break;

                    default:
                        Console.WriteLine("Only f-s-t are allowed. Your Input: {0}", call.Key.ToString());
                        break;
                }
            }
        }

        public static void CallFirst()
        {
            try
            {
                FirstCheck fC = new FirstCheck();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Hello World!");
            }
        }

        public static void CallSecond()
        {
            try
            {
                SecondCheck sC = new SecondCheck();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Hello World!");
            }
        }

        public static void CallThird()
        {
            ThirdCheck tC = new ThirdCheck();

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Service s = new Service();
            s.Menu(); 
        }
    }
}
