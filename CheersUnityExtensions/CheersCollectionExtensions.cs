using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CheersCollectionExtensions
{
    public static int FindMin(this IEnumerable<int> self)
    {
        int min = int.MaxValue;
        foreach (int current in self)
        {
            if (current < min)
            {
                min = current;
            }
        }
        return min;
    }

    public static float FindMin(this IEnumerable<float> self)
    {
        float min = float.MaxValue;
        foreach (float current in self)
        {
            if (current < min)
            {
                min = current;
            }
        }
        return min;
    }

    public static int FindMin<T>(this IEnumerable<T> self, System.Func<T, int> property)
    {
        int min = int.MaxValue;
        foreach (T element in self)
        {
            int current = property(element);
            if (current < min)
            {
                min = current;
            }
        }
        return min;
    }

    public static float FindMin<T>(this IEnumerable<T> self, System.Func<T, float> property)
    {
        float min = float.MaxValue;
        foreach (T element in self)
        {
            float current = property(element);
            if (current < min)
            {
                min = current;
            }
        }
        return min;
    }

    public static int FindMax(this IEnumerable<int> self)
    {
        int min = int.MinValue;
        foreach (int current in self)
        {
            if (current > min)
            {
                min = current;
            }
        }
        return min;
    }

    public static float FindMax(this IEnumerable<float> self)
    {
        float min = float.MinValue;
        foreach (float current in self)
        {
            if (current > min)
            {
                min = current;
            }
        }
        return min;
    }

    public static int FindMax<T>(this IEnumerable<T> self, System.Func<T, int> property)
    {
        int min = int.MinValue;
        foreach (T element in self)
        {
            int current = property(element);
            if (current > min)
            {
                min = current;
            }
        }
        return min;
    }

    public static float FindMax<T>(this IEnumerable<T> self, System.Func<T, float> property)
    {
        float min = float.MinValue;
        foreach (T element in self)
        {
            float current = property(element);
            if (current > min)
            {
                min = current;
            }
        }
        return min;
    }

    public static int IndexOfMin(this IReadOnlyList<int> self)
    {
        int bestIndex = -1;
        int min = int.MaxValue;
        for (int Idx = 0; Idx < self.Count; ++Idx)
        {
            int current = self[Idx];
            if (current < min)
            {
                min = current;
                bestIndex = Idx;
            }
        }
        return bestIndex;
    }

    public static int IndexOfMin(this IReadOnlyList<float> self)
    {
        int bestIndex = -1;
        float min = float.MaxValue;
        for (int Idx = 0; Idx < self.Count; ++Idx)
        {
            float current = self[Idx];
            if (current < min)
            {
                min = current;
                bestIndex = Idx;
            }
        }
        return bestIndex;
    }

    public static int IndexOfMin<T>(this IReadOnlyList<T> self, System.Func<T, int> property)
    {
        int bestIndex = -1;
        int min = int.MaxValue;
        for (int Idx = 0; Idx < self.Count; ++Idx)
        {
            int current = property(self[Idx]);
            if (current < min)
            {
                min = current;
                bestIndex = Idx;
            }
        }
        return bestIndex;
    }

    public static int IndexOfMin<T>(this IReadOnlyList<T> self, System.Func<T, float> property)
    {
        int bestIndex = -1;
        float min = float.MaxValue;
        for (int Idx = 0; Idx < self.Count; ++Idx)
        {
            float current = property(self[Idx]);
            if (current < min)
            {
                min = current;
                bestIndex = Idx;
            }
        }
        return bestIndex;
    }

    public static int IndexOfMax(this IReadOnlyList<int> self)
    {
        int bestIndex = -1;
        int min = int.MinValue;
        for (int Idx = 0; Idx < self.Count; ++Idx)
        {
            int current = self[Idx];
            if (current > min)
            {
                min = current;
                bestIndex = Idx;
            }
        }
        return bestIndex;
    }

    public static int IndexOfMax(this IReadOnlyList<float> self)
    {
        int bestIndex = -1;
        float min = float.MinValue;
        for (int Idx = 0; Idx < self.Count; ++Idx)
        {
            float current = self[Idx];
            if (current > min)
            {
                min = current;
                bestIndex = Idx;
            }
        }
        return bestIndex;
    }

    public static int IndexOfMax<T>(this IReadOnlyList<T> self, System.Func<T, int> property)
    {
        int bestIndex = -1;
        int min = int.MinValue;
        for (int Idx = 0; Idx < self.Count; ++Idx)
        {
            int current = property(self[Idx]);
            if (current > min)
            {
                min = current;
                bestIndex = Idx;
            }
        }
        return bestIndex;
    }

    public static int IndexOfMax<T>(this IReadOnlyList<T> self, System.Func<T, float> property)
    {
        int bestIndex = -1;
        float min = float.MinValue;
        for (int Idx = 0; Idx < self.Count; ++Idx)
        {
            float current = property(self[Idx]);
            if (current > min)
            {
                min = current;
                bestIndex = Idx;
            }
        }
        return bestIndex;
    }

    public static T ItemWithMin<T>(this IEnumerable<T> self, System.Func<T, int> property)
    {
        T bestValue = default(T);
        int min = int.MaxValue;
        foreach (T element in self)
        {
            int current = property(element);
            if (current < min)
            {
                min = current;
                bestValue = element;
            }
        }
        return bestValue;
    }

    public static T ItemWithMin<T>(this IEnumerable<T> self, System.Func<T, float> property)
    {
        T bestValue = default(T);
        float min = float.MaxValue;
        foreach (T element in self)
        {
            float current = property(element);
            if (current < min)
            {
                min = current;
                bestValue = element;
            }
        }
        return bestValue;
    }

    public static T ItemWithMax<T>(this IEnumerable<T> self, System.Func<T, int> property)
    {
        T bestValue = default(T);
        int min = int.MinValue;
        foreach (T element in self)
        {
            int current = property(element);
            if (current > min)
            {
                min = current;
                bestValue = element;
            }
        }
        return bestValue;
    }

    public static T ItemWithMax<T>(this IEnumerable<T> self, System.Func<T, float> property)
    {
        T bestValue = default(T);
        float min = float.MinValue;
        foreach (T element in self)
        {
            float current = property(element);
            if (current > min)
            {
                min = current;
                bestValue = element;
            }
        }
        return bestValue;
    }

    public static List<T> ListItemsWithMin<T>(this IEnumerable<T> self, System.Func<T, int> property) => ListItemsWithMin(self, property, new List<T>());

    public static List<T> ListItemsWithMin<T>(this IEnumerable<T> self, System.Func<T, int> property, List<T> result)
    {
        result.Clear();
        int min = int.MaxValue;
        foreach (T element in self)
        {
            int current = property(element);
            if (current < min)
            {
                result.Clear();
                min = current;
                result.Add(element);
            }
            else if (current == min)
            {
                result.Add(element);
            }
        }
        return result;
    }

    public static List<T> ListItemsWithMin<T>(this IEnumerable<T> self, System.Func<T, float> property) => ListItemsWithMin(self, property, new List<T>());

    public static List<T> ListItemsWithMin<T>(this IEnumerable<T> self, System.Func<T, float> property, List<T> result)
    {
        result.Clear();
        float min = float.MaxValue;
        foreach (T element in self)
        {
            float current = property(element);
            if (current < min)
            {
                result.Clear();
                min = current;
                result.Add(element);
            }
            else if (current == min)
            {
                result.Add(element);
            }
        }
        return result;
    }

    public static List<T> ListItemsWithMax<T>(this IEnumerable<T> self, System.Func<T, int> property) => ListItemsWithMax(self, property, new List<T>());

    public static List<T> ListItemsWithMax<T>(this IEnumerable<T> self, System.Func<T, int> property, List<T> result)
    {
        result.Clear();
        int min = int.MinValue;
        foreach (T element in self)
        {
            int current = property(element);
            if (current > min)
            {
                result.Clear();
                min = current;
                result.Add(element);
            }
            else if (current == min)
            {
                result.Add(element);
            }
        }
        return result;
    }

    public static List<T> ListItemsWithMax<T>(this IEnumerable<T> self, System.Func<T, float> property) => ListItemsWithMax(self, property, new List<T>());

    public static List<T> ListItemsWithMax<T>(this IEnumerable<T> self, System.Func<T, float> property, List<T> result)
    {
        result.Clear();
        float min = float.MinValue;
        foreach (T element in self)
        {
            float current = property(element);
            if (current > min)
            {
                result.Clear();
                min = current;
                result.Add(element);
            }
            else if (current == min)
            {
                result.Add(element);
            }
        }
        return result;
    }

    public static T Last<T>(this IReadOnlyList<T> self) => self[self.Count - 1];
    public static void RemoveLast<T>(this List<T> self) => self.RemoveAt(self.Count - 1);

    public static int GetRandomIndex<T>(this IReadOnlyList<T> self)
    {
        return Random.Range(0, self.Count);
    }

    public static T GetRandomItem<T>(this IReadOnlyList<T> self)
    {
        return self[self.GetRandomIndex()];
    }

    public static void InsertRandom<T>(this List<T> self, T newValue)
    {
        // NB not the same range as GetRandomIndex! self.Count is a valid insertion index.
        self.Insert(Random.Range(0, self.Count + 1), newValue);
    }

    public static T PopItemAt<T>(this List<T> self, int index)
    {
        T result = self[index];
        self.RemoveAt(index);
        return result;
    }

    public static T PopLast<T>(this List<T> self) => self.PopItemAt(self.Count - 1);

    public static T PopAndSwapItemAt<T>(this List<T> self, int index)
    {
        if (index == self.Count - 1)
            return self.PopLast();

        T result = self[index];
        self[index] = self.PopLast();
        return result;
    }

    public static T PopRandom<T>(this List<T> self) => self.PopItemAt(self.GetRandomIndex());
    public static T PopAndSwapRandom<T>(this List<T> self) => self.PopAndSwapItemAt(self.GetRandomIndex());

    public static void SwapItems<T>(this T[] self, int indexA, int indexB)
    {
        T temp = self[indexA];
        self[indexA] = self[indexB];
        self[indexB] = temp;
    }

    public static void SwapItems<T>(this List<T> self, int indexA, int indexB)
    {
        T temp = self[indexA];
        self[indexA] = self[indexB];
        self[indexB] = temp;
    }

    public static void Shuffle<T>(this T[] self)
    {
        int IdxLimit = self.Length - 1;
        for (int Idx = 0; Idx < IdxLimit; ++Idx)
        {
            self.SwapItems(Idx, Random.Range(Idx, self.Length));
        }
    }

    public static void Shuffle<T>(this List<T> self)
    {
        int IdxLimit = self.Count - 1;
        for (int Idx = 0; Idx < IdxLimit; ++Idx)
        {
            self.SwapItems(Idx, Random.Range(Idx, self.Count));
        }
    }

    public static int Count<T>(this IEnumerable<T> self, int stopCountingAt)
    {
        int result = 0;
        foreach (T testVal in self)
        {
            result++;
            if (result >= stopCountingAt)
                return stopCountingAt;
        }
        return result;
    }

    public static int Count<T>(this List<T> self, T equalsValue)
    {
        int result = 0;
        foreach (T testVal in self)
        {
            if (EqualityComparer<T>.Default.Equals(testVal, equalsValue))
                result++;
        }
        return result;
    }

    public static void SortAscending<T>(this List<T> self, System.Func<T, System.IComparable> getProperty)
    {
        self.Sort((a, b) => getProperty(a).CompareTo(getProperty(b)));
    }

    public static void SortDescending<T>(this List<T> self, System.Func<T, System.IComparable> getProperty)
    {
        self.Sort((a, b) => getProperty(b).CompareTo(getProperty(a)));
    }

    public static bool ContentsEqual<A, B>(this IEnumerable<A> a, IEnumerable<B> b) where A : System.IEquatable<B>
    {
        if (a == null || b == null)
            return a == b;

        IEnumerator<A> aE = a.GetEnumerator();
        bool aNext = aE.MoveNext();
        IEnumerator<B> bE = b.GetEnumerator();
        bool bNext = bE.MoveNext();

        while (aNext == bNext)
        {
            if (!aE.Current.Equals(bE.Current))
                return false;

            aNext = aE.MoveNext();
            bNext = bE.MoveNext();
        }

        return true;
    }

    public static bool ContentsEqual<A, B>(this IEnumerable<A> a, IEnumerable<B> b, System.Func<A, B, bool> comparer)
    {
        if (a == null || b == null)
            return a == b;

        IEnumerator<A> aE = a.GetEnumerator();
        bool aNext = aE.MoveNext();
        IEnumerator<B> bE = b.GetEnumerator();
        bool bNext = bE.MoveNext();

        while (aNext && bNext)
        {
            if (!comparer(aE.Current, bE.Current))
                return false;

            aNext = aE.MoveNext();
            bNext = bE.MoveNext();
        }

        if (aNext != bNext)
            return false;

        return true;
    }

    public static HashSet<T> ToHashSet<T>(this IEnumerable<T> self)
    {
        HashSet<T> result = new HashSet<T>();
        foreach (T value in self)
        {
            result.Add(value);
        }
        return result;
    }
}
