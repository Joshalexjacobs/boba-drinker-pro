using System;
using System.Collections.Generic;
using CandyCoded;

public static class ListUtil
{
    public static List<T> RandomRange<T>(this List<T> items, int count) {
        if (count > items.Count) {
            throw new Exception($"RandomRange's List<{typeof(T)}> (items) must have more values than the range requested (count).");
        }

        var indices = new List<int>();

        for (int i = 0; i < items.Count; i++) {
            indices.Add(i);
        }

        var results = new List<T>();

        for (int i = 0; i < count; i++) {
            var randomIndex = indices.Random();

            indices.Remove(randomIndex);

            results.Add(items[randomIndex]);
        }

        return results;
    }
}
