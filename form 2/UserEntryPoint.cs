using System;
using System.Runtime.InteropServices;

namespace CoolGraphicsApp
{
    class UserEntryPoint : IStarter
    {
        static int[] ShellSort(int[] array)
        {
            //расстояние между элементами, которые сравниваются
            var d = array.Length / 2;
            while (d >= 1)
            {
                for (var i = d; i < array.Length; i++)
                {
                    var j = i;
                    PointsManager.IncreaseCompare();
                    while ((j >= d) && (array[j - d] > array[j]))
                    {
                        PointsManager.IncreaseSwap();
                        var t = array[j];
                        array[j] = array[j - d];
                        array[j - d] = t;

                        j = j - d;
                    }
                }

                d = d / 2;
            }

            return array;
        }
        public void Start()
        {
            for (int len = 0; len <= 50000; len += 5000)
            {
                Random random = new Random();
                int[] arr = new int[len];
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = random.Next(0, 1000);

                }
                Array.Sort(arr);
                Array.Reverse(arr);
                BubbleSortOptimized(arr);

                for (long i = 0; i <= 50000; i++)
                {
                    PointsManager.RegisterPoint(i, i * i);
                }
                
                
            }
        }

        static void BubbleSort(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = i; j < array.Length-1; j++)
                {
                    PointsManager.IncreaseCompare();
                    if (array[i] > array[j+1])
                    {
                        Swap(ref array[i], ref array[j+1]);
                    }
                }
            }
        }

        static void BubbleSortOptimized(int[] array)
        {
            for (int step = 0; step < (array.Length - 1); ++step)
            {
                bool swapped = false;

                for (int i = 0; i < (array.Length - step - 1); ++i)
                {
                    if (array[i] > array[i + 1])
                    {
                        Swap(ref array[i], ref array[i + 1]);
                        swapped = true;
                    }
                }
                if (!swapped)
                    break;
            }
        }

        static void Swap(ref int a, ref int b)
        {
            PointsManager.IncreaseSwap();
            int temp = a; 
            a = b;
            b = temp;
        }

        static void StartShellSort(int[] array)
        {
            int value = 1;
            array = GonnetPidorNextStep(value, array.Length);
            ShellSort(array);
        }
        static int[] GonnetPidorNextStep(int startValue, int len)
        {
            int current = startValue;
            int next = 0;
            int[] arr = new int[len];

            for (int i = 1; i <= len; i++)
            {
                if (i == 1)
                    next = 1;
                else if (IsPrime(current) || current == 1)
                    next = 2 * current + 1;
                else if(!IsPrime(current))
                    next = current / SmallestDivisor(current);

                current = next;
                arr[i-1] = current;
                
            }

            return arr;
        }

        static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }

        static int SmallestDivisor(int n)
        {
            // if divisible by 2
            if (n % 2 == 0)
                return 2;

            // iterate from 3 to sqrt(n)
            for (int i = 3; i * i <= n; i += 2)
            {
                if (n % i == 0)
                    return i;
            }

            return n;
        }
    }
}