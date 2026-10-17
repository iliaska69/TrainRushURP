using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public static class NetworkProvider
{
    public static async UniTask<DateTime?> GetGoogleTimeAsync()
    {
        UnityWebRequest request = UnityWebRequest.Head("https://www.google.com");
        await request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogWarning("Failed to get Google time: " + request.error);
            return null;
        }

        if (request.GetResponseHeaders().TryGetValue("date", out string dateHeader))
        {
            if (DateTime.TryParse(dateHeader, out DateTime serverTime))
            {
                return serverTime.ToLocalTime();
            }
        }

        return null;
    }
}