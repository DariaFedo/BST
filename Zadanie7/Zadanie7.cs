using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie7
{
    class Zadanie7
    {
        static int noValue = -1337;
        

        static void Insert(ref List<int> key, ref List<int> parent, ref List<int> left, ref List<int> right, ref int root, ref int insertedKey)
        {

            int temp, parentCandidate, insertedIndex;
            parentCandidate = 0;
            temp = root;

            key.Add(insertedKey);
            parent.Add(noValue);
            left.Add(noValue);
            right.Add(noValue);

            insertedIndex = key.Count() - 1;

            while (temp != noValue)
            {
                parentCandidate = temp;
                if (insertedKey < key[parentCandidate])
                {
                    temp = left[parentCandidate];

                }
                else
                {
                    temp = right[parentCandidate];
                }
            }
            if (key.Count() == 1)
            {
                parentCandidate = noValue;
            }
            parent[insertedIndex] = parentCandidate;
            if (parentCandidate == noValue)
            {
                root = insertedIndex;
                left[insertedIndex] = noValue;
                right[insertedIndex] = noValue;
                parent[insertedIndex] = noValue;
            }
            else
            {
                if (insertedKey < key[parentCandidate])
                {
                    left[parentCandidate] = insertedIndex;
                }
                else
                {
                    right[parentCandidate] = insertedIndex;
                }
            }

        }


        static int Min(ref List<int> key, ref List<int> parent, ref List<int> left, ref List<int> right, ref int root)
        {
            int x = root;
            while (left[x] != noValue)
            {
                x = left[x];
            }
            return x;
        }
        static int Max(ref List<int> key, ref List<int> parent, ref List<int> left, ref List<int> right, ref int root)
        {
            int x = root;
            while (right[x] != noValue)
            {
                x = right[x];
            }
            return x;
        }
        static void Inorder(ref List<int> key, ref List<int> parent, ref List<int> left, ref List<int> right, ref int root)
        {
            int x = root;
            Stack<int> stos = new Stack<int>();

            Console.WriteLine("Inorder: ");

            while (x != noValue || stos.Count != 0)
            {
                while (x != noValue)
                {
                    stos.Push(x);
                    x = left[x];
                }
                x = stos.Peek();
                stos.Pop();

                Console.WriteLine(key[x] + " ");
                x = right[x];
            }
            

        }
        static int Member(ref List<int> key, ref List<int> parent, ref List<int> left, ref List<int> right, ref int root)
        {
            int x = root;
            int k;
            Console.WriteLine("Podaj key do wyszukania po drzewie ");
            k = Int32.Parse(Console.ReadLine());

            while (x != noValue && k != key[x])
            {
                if (k < key[x])
                {
                    x = left[x];
                }
                else
                {
                    x = right[x];
                }
            }
            Console.WriteLine(x);
            return x;

        }
        static int FindHeight(ref List<int> key, ref List<int> parent, ref List<int> left, ref List<int> right, int root)
        {
            if (root == noValue)
            {
                return -1;
            }

            int leftheight = FindHeight(ref key, ref parent, ref left, ref right, left[root]);
            int rightheight = FindHeight(ref key, ref parent, ref left, ref right, right[root]);

            if (leftheight > rightheight)
            {
                return leftheight + 1;
            }
            else
            {
                return rightheight + 1;
            }
        }
        static void InserteConf(ref List<int> key, ref List<int> parent, ref List<int> left, ref List<int> right, ref int root)
        {
            int insertedKey, minnum, maxnum, nodes;
            bool executing = false;
            Console.WriteLine("Wartosci losowe/wpisywane: l/w");
            string odp = Console.ReadLine();
            if (odp == "l")
            {
                Console.WriteLine("Ilosc nodów(węzłów): ");
                nodes = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Minimalna wartość liścia: ");
                minnum = Int32.Parse(Console.ReadLine());

                Console.WriteLine("Maksymalna wartość liścia: ");
                maxnum = Int32.Parse(Console.ReadLine());
                executing = true;
                if (executing)
                    
                    for (int i = 0; i < nodes; i++)
                    {
                        Random random = new Random();
                        insertedKey = random.Next(minnum, maxnum);
                        Insert(ref key, ref parent, ref left, ref right, ref root, ref insertedKey);
                    }
               
            }
            else
            {
                do
                {
                    Console.WriteLine("Podaj klucz do dodania do drzewa, jeśli chcesz zakończyć dodawanie kluczy podaj wartość (-1): ");
                    insertedKey = Int32.Parse(Console.ReadLine());
                    executing = false;
                    Insert(ref key, ref parent, ref left, ref right, ref root, ref insertedKey);

                } while (insertedKey != -1);
               
            }
            

        }

        static void Main(string[] args)
        {
            List<int> key = new List<int>();
            List<int> parent = new List<int>();
            List<int> left = new List<int>();
            List<int> right = new List<int>();


            int root = 0;
            int input = noValue;
            int temp = noValue;

            while (input != 0)
            {
                if (key.Count > 0)
                    Console.WriteLine("Wysokość drzewa: {0}", FindHeight(ref key, ref parent, ref left, ref right, root));



                Console.WriteLine("1.Insert \n 2. Max \n 3.Min \n 4.Inorder \n 5.Member \n 6. Wyjscie");
                input = Int32.Parse(Console.ReadLine());


                switch (input)
                {
                    case 1:
                        InserteConf(ref key, ref parent, ref left, ref right, ref root);
                        break;
                    case 2:
                        Console.WriteLine(key[Max(ref key, ref parent, ref left, ref right, ref root)]);
                        break;
                    case 3:
                        Console.WriteLine(key[Min(ref key, ref parent, ref left, ref right, ref root)]);
                        break;
                    case 4:
                        Inorder(ref key, ref parent, ref left, ref right, ref root);
                        break;
                    case 5:
                        Member(ref key, ref parent, ref left, ref right, ref root);
                        break;
                    case 6:
                        break;

                }

                bool showTablice = true;

                if (showTablice)
                {
                    Console.WriteLine("key: ");
                    for (int i = 0; i < key.Count(); i++)
                    {
                        Console.WriteLine(key[i] + " ");
                    }
                    Console.WriteLine("parent: ");
                    for (int i = 0; i < parent.Count(); i++)
                    {
                        Console.WriteLine(parent[i] + " ");
                    }
                    Console.WriteLine("left: ");
                    for (int i = 0; i < left.Count(); i++)
                    {
                        Console.WriteLine(left[i] + " ");
                    }
                    Console.WriteLine("right: ");
                    for (int i = 0; i < right.Count(); i++)
                    {
                        Console.WriteLine(right[i] + " ");
                    }

                }

            }

        }
    }

}

