using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;

namespace YandexSDK
{
    public static class YandexAuthorizing
    {
        public static event UnityAction Authorized;

        public static bool GetIsAuthorized()
        {
#if UNITY_EDITOR == false && UNITY_WEBGL
        if (YandexInitializing.GetIsInitialized())
        {
            return PlayerAccount.IsAuthorized;
        }
#endif
            return false;
        }

        public static void Authorize()
        {
            if (GetIsAuthorized())
            {
                OnAuthorized();
            }
            else
            {
                Debug.Log("Authorization attempt");

#if UNITY_EDITOR == false && UNITY_WEBGL
            PlayerAccount.Authorize(OnAuthorized);
#else
                Debug.Log("Authorization is possible only in the Yandex Games build");
#endif
            }
        }

        private static void OnAuthorized()
        {
            Debug.Log("Authorized");
            Authorized?.Invoke();
        }
    }
}