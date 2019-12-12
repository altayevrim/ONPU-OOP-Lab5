using System;
using System.Collections.Generic;
namespace Part2
{
    class Computer : System.IEquatable<Computer>
    {
        public string brand;
        public string processor;
        public Computer(){
            this.brand = "Unknown";
            this.processor = "Unknown";
        }

        public Computer(string brand, string processor){
            this.brand = brand;
            this.processor = processor;
        }

        public bool Equals(Computer other)
        {
            // return true;
            if (other == null) return false;
            if (this.brand == other.brand && this.processor == other.processor)
            {
                return true;
            }
            return false;
            // return this.Equals(other);
        }
    }

    class Search {
        Computer searchTerms;
        public Search(Computer search, SortedList<int, Computer> searchFrom){
            this.searchTerms = search;
            foreach (var item in searchFrom.Values)
            {
                    if (item.Equals(searchTerms))
                    {
                        Console.WriteLine("\n\tFound in the database");
                    }   
            }
        }

       
    }
    class Program
    {
        public static SortedList<int, Computer> mySL = new SortedList<int, Computer>();

        public static int generalCounter = 0;
        static void Main(string[] args)
        {
            AddComputer("Lenovo","AMD");
            AddComputer("Lenovo","INTEL");
            AddComputer("Asus","AMD");
            AddComputer("Asus","INTEL");
            var brand = "";
            var processor = "";
            do{
                Console.WriteLine("==================================================");
                Console.WriteLine("Please enter the computer brand name: ");
                brand = Console.ReadLine();
                Console.WriteLine("Please enter a processor name: ");
                processor = Console.ReadLine();
                Computer searchTerms = new Computer(brand,processor);
                Search search = new Search(searchTerms, mySL);
                Console.WriteLine("Hello World!");
            }while(brand != "" || processor != "");

            Console.WriteLine("Bye.");
        }

        public static void AddComputer(string brand, string processor){
            if(brand != "" && processor != ""){
                mySL.Add(generalCounter, new Computer(brand, processor));
                generalCounter++;
            }
        }
    }
}
