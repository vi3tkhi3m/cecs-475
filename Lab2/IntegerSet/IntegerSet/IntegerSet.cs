using System;
using System.Linq;

namespace Lab2
{
    class IntegerSet
    {
        private const int SETSIZE = 101;
        private bool[] set;

        /// <summary>
        /// Default constructor
        /// </summary>
        public IntegerSet()
        {
            set = new bool[SETSIZE];
        }

        /// <summary>
        /// Overloaded constructor
        /// </summary>
        /// <param name="array">integer array.</param>
        public IntegerSet(int[] array) : this()
        {
            for (int i = 0; i < array.Length; i++)
            {
                // Make sure that the array values is/greater than 0 and less than the setsize. I.e -5 is not allowed.
                if (array[i] >= 0 && array[i] < SETSIZE)
                {
                    InsertElement(array[i]);
                }
            }

        }

        /// <summary>
        /// Creates the union of two sets
        /// </summary>
        /// <returns>Returns a new IntegerSet, the union of this set and otherSet set.</returns>
        /// <param name="otherSet">IntegerSet otherSet.</param>
        public IntegerSet Union(IntegerSet otherSet)
        {
            IntegerSet temp = new IntegerSet();

            for (int i = 0; i < SETSIZE; i++)
            {
                if (this.set[i] || otherSet.set[i])
                {
                    temp.set[i] = true;
                }
            }

            return temp;
        }

        /// <summary>
        /// Creates the intersection of two sets
        /// </summary>
        /// <returns>Returns a new IntegerSet, the intersection of this set and otherSet set.</returns>
        /// <param name="otherSet">IntegerSet otherSet.</param>
        public IntegerSet Intersection(IntegerSet otherSet)
        {
            IntegerSet temp = new IntegerSet();

            for (int i = 0; i < SETSIZE; i++)
            {
                temp.set[i] = this.set[i] && otherSet.set[i];
            }

            return temp;
        }

        /// <summary>
        /// Inserts a new value in the set
        /// </summary>
        /// <param name="k">integer k.</param>
        public void InsertElement(int k)
        {
            if (IsValueInRange(k))
            {
                this.set[k] = true;
            }
            else
            {
                Console.WriteLine($"Please enter a valid number 0 - {SETSIZE - 1}.");
            }
        }

        /// <summary>
        /// Deletes a value from the set
        /// </summary>
        /// <param name="m">integer m.</param>
        public void DeleteElement(int m)
        {
            if (IsValueInRange(m))
            {
                this.set[m] = false;
            }
            else
            {
                Console.WriteLine($"Please enter a valid number 0 - {SETSIZE - 1}.");
            }

        }

        /// <summary>
        /// Creates a string with integers in the set
        /// </summary>
        /// <returns>A string with integers in it <see cref="T:System.String"/></returns>
        public override string ToString()
        {
            string temp = "";

            for (int i = 0; i < SETSIZE; i++)
            {
                if (set[i])
                {
                    temp += i + " ";
                }
            }

            return temp;
        }

        /// <summary>
        /// Checks if this set is equal to the ohter set
        /// </summary>
        /// <param name="otherSet">IntegerSet otherSet.</param>
        /// <returns>Returns true if both sets matches, else returns false</returns>
        public bool IsEqualTo(IntegerSet otherSet)
        {
            return Enumerable.SequenceEqual(this.set, otherSet.set);
        }

        /// <summary>
        /// Checks if the integer is greater or equal to 0 and less than the setsize
        /// </summary>
        /// <param name="v">integer v</param>
        /// <returns>Return true if requirements are met, else returns false</returns>
        public bool IsValueInRange(int v)
        {
            return (v >= 0 && v < SETSIZE);
        }
    }
}

