using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;
using System;

namespace YandexSDK
{
    public class YandexInitializer : MonoBehaviour
    {
        public event Action Failed;
        public event Action Initialized;

        private void Awake()
        {
            YandexGamesSdk.CallbackLogging = true;
        }

        public void Init()
        {
            StartCoroutine(Initializing());
        }

        private IEnumerator Initializing()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            Failed?.Invoke();
            yield break;
#endif

            // Always wait for it if invoking something immediately in the first scene.
            yield return YandexGamesSdk.Initialize();
            Initialized?.Invoke();
        }
    }
}