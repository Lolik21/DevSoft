using System;
using System.Collections;

namespace ConsoleApplication1
{
    class Letter
    {
        char ch = 'А';
        int end;

        public Letter(int end)
        {
            this.end = end;
        }

        // Итератор, возвращающий end-букв
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < this.end; i++)
            {
                if (i == 33) yield break; // Выход из итератора, если закончится алфавит
                yield return (char)(ch + i);
            }
        }

        // Создание именованного итератора
        public IEnumerable MyItr(int begin, int end)
        {
            for (int i = begin; i <= end; i++)
            {
                yield return (char)(ch + i);
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Console.Write("Сколько букв вывести? ");
            int i = int.Parse(Console.ReadLine());

            Letter lt = new Letter(i);
            Console.WriteLine("\nРезультат: \n");

            foreach (char c in lt)
                Console.Write(c + " ");

            Console.Write("\nВведите пределы\n\nMin: ");
            int j = int.Parse(Console.ReadLine());
            Console.Write("Max: ");
            int y = int.Parse(Console.ReadLine());
            Console.WriteLine("\nРезультат: \n");

            foreach (char c in lt.MyItr(j, y))
                Console.Write(c + " ");

            Console.ReadLine();
        }
    }
}