using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;

namespace YandexSDK
{
    public static class YandexPersonalData
    {
        public static PlayerAccountProfileDataResponse Data { get; private set; } = new();

        public static event UnityAction DataPermissioned;
        public static event UnityAction DataLoaded;
        public static event UnityAction DataProhibited;

        public static bool GetDataIsAvailabled()
        {
#if UNITY_EDITOR == false && UNITY_WEBGL
        if (YandexAuthorizing.GetIsAuthorized())
        {
            return PlayerAccount.HasPersonalProfileDataPermission;
        }
#endif
            return false;
        }

        public static void RequestData()
        {
            if (YandexAuthorizing.GetIsAuthorized())
            {
                if (GetDataIsAvailabled())
                {
                    OnDataPermissioned();
                }
                else
                {
                    Debug.Log("Attempt to obtain permission for personal data");

#if UNITY_EDITOR == false && UNITY_WEBGL
            PlayerAccount.RequestPersonalProfileDataPermission(OnDataPermissioned, OnErrorCallback);
#else
                    Debug.Log("Access to personal data is possible only in the Yandex Games assembly");
#endif
                }
            }
            else
            {
                YandexAuthorizing.Authorize();
            }
        }

        private static void OnDataPermissioned()
        {
            PlayerAccount.GetProfileData(SetData);

            Debug.Log("Data permissioned");
            DataPermissioned?.Invoke();
        }

        private static void OnErrorCallback(string errorText)
        {
            Debug.Log($"Data prohibited - {errorText}");
            DataProhibited?.Invoke();
        }

        private static void SetData(PlayerAccountProfileDataResponse data)
        {
            Data = data;
            Debug.Log("Data loaded");
            DataLoaded?.Invoke();
        }
    }
}