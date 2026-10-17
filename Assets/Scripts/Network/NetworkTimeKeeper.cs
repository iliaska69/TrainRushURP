using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public static class NetworkTimeKeeper
{
    private static DateTime _startupTime;
    private static bool _initialized = false;

    public static bool IsInitialized => _initialized;

    public static async UniTask InitializeAsync()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.LogWarning("No internet, skipping time sync");
            return;
        }
        
        var time = await NetworkProvider.GetGoogleTimeAsync();

        if (time.HasValue)
        {
            _startupTime = time.Value;
            _initialized = true;
        }
    }

    public static DateTime? GetCurrentTime()
    {
        if (!_initialized)
        {
            return null;
        }

        return _startupTime;
    }
}