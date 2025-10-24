using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public static class WaitForSecondsCache {
    private const int MAX_ITEMS = 100;
    private static readonly Dictionary<float, WaitForSeconds> Cache = new();

    public static WaitForSeconds Get(float waitTime) {
        if (Cache.TryGetValue(waitTime, out var value)) {
            return value;
        }

        if (Cache.Count > MAX_ITEMS) {
            Cache.Remove(Cache.First().Key);
        }

        var newWait = new WaitForSeconds(waitTime);
        Cache[waitTime] = newWait;
        return newWait;
    }
}