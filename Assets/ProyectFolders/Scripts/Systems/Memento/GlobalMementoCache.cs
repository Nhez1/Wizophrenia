public static class GlobalMementoCache
{
    public static object[] CachedState;

    public static bool HasState => CachedState != null && CachedState.Length > 0;

    public static void Clear() => CachedState = null;
}
