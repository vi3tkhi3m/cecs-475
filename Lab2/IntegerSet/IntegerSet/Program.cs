// Khiem Pham #026909504
// CECS 475-01
// Lab 2
// Due date : 08/31/2020
using System;

namespace Lab2
{
    class Program
    {
        /// <summary>
        /// Takes the user's input and put it in the set.
        /// </summary>
        private static IntegerSet InputSet()
        {
            IntegerSet temp = new IntegerSet();
            int number;

            string input = Console.ReadLine();
            bool done = false;

            while (done == false)
            {
                if (Int32.TryParse(input, out number))
                {
                    temp.InsertElement(number);
                    input = Console.ReadLine();
                }
                else
                {
                    if (input == "done")
                    {
                        done = true;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid number!");
                        input = Console.ReadLine();
                    }
                }
            }

            return temp;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("INSTRUCTIONS:\n");
            Console.WriteLine("Insert valid numbers. Once you're done, type in \"done\" (without quotes) to go to the next step.\n");

            // initialize two sets
            Console.WriteLine("Input Set A");
            IntegerSet set1 = InputSet();
            Console.WriteLine("\nInput Set B");
            IntegerSet set2 = InputSet();

            IntegerSet union = set1.Union(set2);
            IntegerSet intersection = set1.Intersection(set2);

            // prepare output
            Console.WriteLine("\nSet A contains elements:");
            Console.WriteLine(set1.ToString());
            Console.WriteLine("\nSet B contains elements:");
            Console.WriteLine(set2.ToString());
            Console.WriteLine(
            "\nUnion of Set A and Set B contains elements:");
            Console.WriteLine(union.ToString());
            Console.WriteLine(
            "\nIntersection of Set A and Set B contains elements:");
            Console.WriteLine(intersection.ToString());

            // test whether two sets are equal
            if (set1.IsEqualTo(set2))
                Console.WriteLine("\nSet A is equal to set B");
            else
                Console.WriteLine("\nSet A is not equal to set B");

            // test insert and delete
            Console.WriteLine("\nInserting 77 into set A...");
            set1.InsertElement(77);
            Console.WriteLine("\nSet A now contains elements:");
            Console.WriteLine(set1.ToString());

            Console.WriteLine("\nDeleting 77 from set A...");
            set1.DeleteElement(77);
            Console.WriteLine("\nSet A now contains elements:");
            Console.WriteLine(set1.ToString());

            // test constructor
            int[] intArray = { 25, 67, 2, 9, 99, 105, 45, -5, 100, 1 };
            IntegerSet set3 = new IntegerSet(intArray);

            Console.WriteLine("\nNew Set contains elements:");
            Console.WriteLine(set3.ToString());
        }
    }
}
