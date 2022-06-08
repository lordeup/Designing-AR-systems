using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public static class ArrayUtils
    {
        public static T GetRandomListItem<T>(IReadOnlyList<T> list)
        {
            var range = GetRandomRange(list.Count);
            var item = list[range];
            return item;
        }

        private static int GetRandomRange(int count)
        {
            return Random.Range(0, count);
        }
        
        public static Dictionary<TKey, TValue> SetDictionaryValue<TKey, TValue>(Dictionary<TKey, TValue> objects, TKey key,
            TValue value)
        {
            var dictionary = new Dictionary<TKey, TValue>(objects);

            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }

            return dictionary;
        }
    }
}
