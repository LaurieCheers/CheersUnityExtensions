using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CheersDictionaryExtensions
{
    public static V GetOrFallback<K, V>(this IReadOnlyDictionary<K, V> dict, K key)
    {
        dict.TryGetValue(key, out V result);
        return result;
    }

    public static V GetOrFallback<K, V>(this IReadOnlyDictionary<K, V> self, K key, V fallback)
    {
        if (self.TryGetValue(key, out V result))
            return result;
        else
            return fallback;
    }

    public static V GetOrFallback<K, V>(this IReadOnlyDictionary<K, V> self, K key, System.Func<V> fallback)
    {
        if (self.TryGetValue(key, out V result))
            return result;
        else
            return fallback();
    }

    public static V GetOrFallback<K, V>(this IReadOnlyDictionary<K, V> self, K key, System.Func<K, V> fallback)
    {
        if (self.TryGetValue(key, out V result))
            return result;
        else
            return fallback(key);
    }

    public static V GetOrFallback<K, V>(this IReadOnlyDictionary<K, V> self, K key, IReadOnlyDictionary<K, V> fallback)
    {
        if (self.TryGetValue(key, out V result))
            return result;

        return fallback.GetOrFallback(key);
    }


    public static V GetOrAdd<K, V>(this Dictionary<K, V> self, K key, V defaultValue)
    {
        if (self.TryGetValue(key, out V result))
            return result;

        result = defaultValue;
        self.Add(key, result);
        return result;
    }

    public static V GetOrAdd<K, V>(this Dictionary<K, V> self, K key, System.Func<K, V> creator)
    {
        if (self.TryGetValue(key, out V result))
            return result;

        result = creator(key);
        self.Add(key, result);
        return result;
    }

    public static V GetOrAdd<K, V>(this Dictionary<K, V> self, K key, System.Func<V> creator)
    {
        if (self.TryGetValue(key, out V result))
            return result;

        result = creator();
        self.Add(key, result);
        return result;
    }

    public static V GetOrAddNew<K, V>(this Dictionary<K, V> self, K key) where V : new()
    {
        if (self.TryGetValue(key, out V result))
            return result;

        result = new V();
        self.Add(key, result);
        return result;
    }



    public static V Mutate<K, V>(this Dictionary<K, V> dict, K key, V baseValue, System.Func<V, V> mutator)
    {
        V currentValue = dict.GetOrFallback(key, baseValue);
        currentValue = mutator(currentValue);
        dict[key] = currentValue;
        return currentValue;
    }

    public static V Mutate<K, V>(this Dictionary<K, V> dict, K key, System.Func<V> baseValue, System.Func<V, V> mutator)
    {
        V currentValue = dict.GetOrFallback(key, baseValue);
        currentValue = mutator(currentValue);
        dict[key] = currentValue;
        return currentValue;
    }

    public static V Mutate<K, V>(this Dictionary<K, V> dict, K key, System.Func<K, V> baseValue, System.Func<V, V> mutator)
    {
        V currentValue = dict.GetOrFallback(key, baseValue);
        currentValue = mutator(currentValue);
        dict[key] = currentValue;
        return currentValue;
    }

    public static V Mutate<K, V>(this Dictionary<K, V> dict, K key, V baseValue, System.Action<V> mutator)
    {
        V currentValue = dict.GetOrAdd(key, baseValue);
        mutator(currentValue);
        return currentValue;
    }

    public static V Mutate<K, V>(this Dictionary<K, V> dict, K key, System.Func<V> baseValue, System.Action<V> mutator)
    {
        V currentValue = dict.GetOrAdd(key, baseValue);
        mutator(currentValue);
        return currentValue;
    }

    public static V Mutate<K, V>(this Dictionary<K, V> dict, K key, System.Func<K, V> baseValue, System.Action<V> mutator)
    {
        V currentValue = dict.GetOrAdd(key, baseValue);
        mutator(currentValue);
        return currentValue;
    }

    public static V MutateWithNew<K, V>(this Dictionary<K, V> dict, K key, System.Func<V, V> mutator) where V : new()
    {
        V currentValue = dict.GetOrFallback(key, ()=>new V());
        currentValue = mutator(currentValue);
        dict[key] = currentValue;
        return currentValue;
    }

    public static V MutateWithNew<K, V>(this Dictionary<K, V> dict, K key, System.Action<V> mutator) where V : new()
    {
        V currentValue = dict.GetOrAddNew(key);
        mutator(currentValue);
        return currentValue;
    }

    public static V GetMemoized<K, V>(this Dictionary<K, V> self, K key, System.Func<V> getValue) => self.GetOrAdd(key, getValue);
    public static V GetMemoized<K, V>(this Dictionary<K, V> self, K key, System.Func<K, V> getValue) => self.GetOrAdd(key, getValue);
    public static V InvokeMemoized<K, V>(this System.Func<V> getValue, K arg, Dictionary<K, V> store) => store.GetOrAdd(arg, getValue);
    public static V InvokeMemoized<K, V>(this System.Func<K, V> getValue, K arg, Dictionary<K, V> store) => store.GetOrAdd(arg, getValue);

    public static string Concat<K>(this Dictionary<K, string> dict, K key, string text) => dict.Mutate(key, "", value => value + text);
    public static int AddAmount<K>(this Dictionary<K, int> dict, K key, int amount) => dict.Mutate(key, 0, value => value + amount);
    public static long AddAmount<K>(this Dictionary<K, long> dict, K key, long amount) => dict.Mutate(key, 0, value => value + amount);
    public static float AddAmount<K>(this Dictionary<K, float> dict, K key, float amount) => dict.Mutate(key, 0, value => value + amount);
    public static double AddAmount<K>(this Dictionary<K, double> dict, K key, double amount) => dict.Mutate(key, 0, value => value + amount);
    public static List<T> AddItem<K, T>(this Dictionary<K, List<T>> dict, K key, T item) => dict.MutateWithNew(key, list => list.Add(item));
    public static HashSet<T> AddItem<K, T>(this Dictionary<K, HashSet<T>> dict, K key, T item) => dict.MutateWithNew(key, list => list.Add(item));

}
