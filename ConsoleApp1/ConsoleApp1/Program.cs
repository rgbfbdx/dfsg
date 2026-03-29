using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main()
        {
            // 1) Sort names example
            Console.WriteLine("Enter 5 names:");
            var names = new string[5];
            for (int i = 0; i < 5; i++)
                names[i] = Console.ReadLine() ?? string.Empty;

            Array.Sort(names, StringComparer.Ordinal);
            Console.WriteLine("Sorted names:");
            foreach (var n in names)
                Console.WriteLine(n);

            // 2) Matrix utilities demonstration
            int[,] matrix = { { 1, 2, 3 }, { 4, 5, 6 } };
            Console.WriteLine("Original matrix:");
            PrintMatrix(matrix);

            matrix = AddColumn(matrix, 1, 0);
            Console.WriteLine("After adding column at position 1:");
            PrintMatrix(matrix);

            matrix = Transpose(matrix);
            Console.WriteLine("Transposed matrix:");
            PrintMatrix(matrix);

            // 3) Contacts example
            var contacts = new List<Contact>();
            AddContact(contacts, "Alice", "123");
            AddContact(contacts, "Bob", "456");
            Console.WriteLine($"Phone for Alice: {FindByName(contacts, "Alice")}");
            Console.WriteLine($"Name for phone 456: {FindByPhone(contacts, "456")}");

            // 4) Vector/set helpers
            var a = new List<int> { 1, 2, -3, 4 };
            var b = new List<int> { 2, 3, -3, 5 };
            var c = new List<int> { 3, 6, -4 };

            Console.WriteLine("Intersection: " + string.Join(", ", Intersection(a, b)));
            Console.WriteLine("Unique all: " + string.Join(", ", UniqueAll(a, b, c)));
            Console.WriteLine("Negatives: " + string.Join(", ", Negatives(a, b, c)));
        }

        // Matrix helpers using 2D arrays
        static int[,] AddColumn(int[,] arr, int pos, int fillValue = 0)
        {
            int rows = arr.GetLength(0);
            int cols = arr.GetLength(1);
            var res = new int[rows, cols + 1];
            for (int i = 0; i < rows; i++)
            {
                int k = 0;
                for (int j = 0; j < cols + 1; j++)
                {
                    if (j == pos)
                        res[i, j] = fillValue;
                    else
                        res[i, j] = arr[i, k++];
                }
            }
            return res;
        }

        static int[,] RemoveColumn(int[,] arr, int pos)
        {
            int rows = arr.GetLength(0);
            int cols = arr.GetLength(1);
            if (cols <= 1) return arr;
            var res = new int[rows, cols - 1];
            for (int i = 0; i < rows; i++)
            {
                int k = 0;
                for (int j = 0; j < cols; j++)
                {
                    if (j == pos) continue;
                    res[i, k++] = arr[i, j];
                }
            }
            return res;
        }

        static int[,] Transpose(int[,] arr)
        {
            int rows = arr.GetLength(0);
            int cols = arr.GetLength(1);
            var res = new int[cols, rows];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    res[j, i] = arr[i, j];
            return res;
        }

        static void ShiftRows(ref int[,] arr, int k)
        {
            int rows = arr.GetLength(0);
            int cols = arr.GetLength(1);
            k = ((k % rows) + rows) % rows;
            if (k == 0) return;
            var res = new int[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                int src = (i - k + rows) % rows;
                for (int j = 0; j < cols; j++)
                    res[i, j] = arr[src, j];
            }
            arr = res;
        }

        static void ShiftCols(ref int[,] arr, int k)
        {
            int rows = arr.GetLength(0);
            int cols = arr.GetLength(1);
            k = ((k % cols) + cols) % cols;
            if (k == 0) return;
            var res = new int[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    res[i, j] = arr[i, (j - k + cols) % cols];
            arr = res;
        }

        static void PrintMatrix(int[,] m)
        {
            int rows = m.GetLength(0);
            int cols = m.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                    Console.Write(m[i, j] + " ");
                Console.WriteLine();
            }
        }

        // Contacts
        class Contact
        {
            public string Name { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
        }

        static void AddContact(List<Contact> contacts, string name, string phone)
        {
            contacts.Add(new Contact { Name = name, Phone = phone });
        }

        static string? FindByName(List<Contact> contacts, string name)
        {
            return contacts.FirstOrDefault(c => c.Name == name)?.Phone;
        }

        static string? FindByPhone(List<Contact> contacts, string phone)
        {
            return contacts.FirstOrDefault(c => c.Phone == phone)?.Name;
        }

        // Vector / set helpers
        static List<int> ToList(int[,] arr)
        {
            var res = new List<int>();
            int rows = arr.GetLength(0);
            int cols = arr.GetLength(1);
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    res.Add(arr[i, j]);
            return res;
        }

        static List<int> Intersection(List<int> a, List<int> b)
        {
            var s = new HashSet<int>(a);
            return b.Where(x => s.Contains(x)).Distinct().ToList();
        }

        static List<int> UniqueAll(List<int> a, List<int> b, List<int> c)
        {
            var s = new HashSet<int>(a);
            s.UnionWith(b);
            s.UnionWith(c);
            return s.ToList();
        }

        static List<int> Negatives(List<int> a, List<int> b, List<int> c)
        {
            var s = new HashSet<int>();
            foreach (var x in a) if (x < 0) s.Add(x);
            foreach (var x in b) if (x < 0) s.Add(x);
            foreach (var x in c) if (x < 0) s.Add(x);
            return s.ToList();
        }
    }
}
