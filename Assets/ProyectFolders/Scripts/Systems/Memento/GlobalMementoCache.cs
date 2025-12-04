using System;
using System.Collections.Generic;

public static class GlobalMementoCache
{
    private static Dictionary<Type, object[]> _cache = new();

    public static void Save(Type type, object[] state)
    {
        _cache[type] = state;
    }

    public static bool TryLoad(Type type, out object[] state)
    {
        return _cache.TryGetValue(type, out state);
    }

    public static void Clear(Type type)
    {
        _cache.Remove(type);
    }

    public static void ClearAll()
    {
        _cache.Clear();
    }
}
