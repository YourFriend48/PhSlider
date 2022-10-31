using Agava.YandexGames;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace YandexSDK
{
    public static class YandexInitializing
    {
        public static event UnityAction Initialized;
        public static event UnityAction Failed;

        public static bool GetIsInitialized()
        {
#if UNITY_EDITOR == false && UNITY_WEBGL
        return YandexGamesSdk.IsInitialized;
#endif
            return false;
        }

        private static void StartCoroutine()
        {
            throw new NotImplementedException();
        }

        public static IEnumerator Initialize()
        {
            Debug.Log("YandexSDK initialize attempt");

#if UNITY_EDITOR == false && UNITY_WEBGL
            yield return YandexGamesSdk.Initialize(OnInitialized);
#else
            Debug.Log("YandexSDK initialize is possible only in the Yandex Games build");

            Failed?.Invoke();
            yield break;
#endif
        }

        private static void OnInitialized()
        {
            Debug.Log("YandexSDK initialized");
            Initialized?.Invoke();
        }
    }
}