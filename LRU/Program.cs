using System;
using System.Collections.Generic;

namespace LRU
{
    class Program
    {
        static void Main(string[] args)
        {
            LRU<int> lru = new LRU<int>(2);
            lru.Add(1, 11);
            foreach (var item in lru.dictionary)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("--------------------");
            lru.Add(2, 22);
            foreach (var item in lru.dictionary)
            {
                Console.WriteLine(item);
            }
            lru.Add(3, 33);
            Console.WriteLine("--------------------");
            foreach (var item in lru.dictionary)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("--------------------");
            lru.RemoveLeastRecentlyUsed();
            foreach (var item in lru.dictionary)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("--------------------");
            foreach (var item in lru.cacheList)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Hello World!");
            Console.WriteLine("!--------------------");
            Console.WriteLine(SumGeometricProgression(2, 2, 4));

        }
        // геометрическая прогрессия
        // a - первый член
        // r - знаменатель
        // n - колличество
        static int SumGeometricProgression(int a, int r, int n)
        {
            if (n == 1)
                return a;
            if (n == 0)
                return 0;
            n--;
            return a * (int)Math.Pow(r, n) + SumGeometricProgression(a, r, n);
        }
    }
    class LRU<T>
    {
        public int capacity;
        public Dictionary<int, T> dictionary;
        public LinkedList<int> cacheList;
        public LRU(int capacity)
        {
            this.capacity = capacity;
            dictionary = new Dictionary<int, T>();
            cacheList = new LinkedList<int>();
        }
        public void Add(int key, T value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
                cacheList.Remove(key);
                cacheList.AddFirst(key);
            }
            else
            {
                if (dictionary.Count>=capacity)
                {
                    RemoveLeastRecentlyUsed();
                }
                dictionary.Add(key, value);
                cacheList.AddFirst(key);
            }

        }
        public T Get(int key)
        {
            if (!dictionary.ContainsKey(key))
            {
                throw new KeyNotFoundException();
            }
            cacheList.Remove(key);
            cacheList.AddFirst(key);
            return dictionary[key];
        }
        public void RemoveLeastRecentlyUsed()
        {
            var key = cacheList.Last.Value;
            dictionary.Remove(key);
            cacheList.Remove(key);
        } //удаляет дольше всего не использовавшийся ключ 
    }
}
