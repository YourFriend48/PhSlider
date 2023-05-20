using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;

namespace YandexSDK
{
    public static class YandexPersonalData
    {
        public static PlayerAccountProfileDataResponse Data { get; private set; } = new();
        public static bool IsDataSetted = false;
        public static bool HasLeaderboardRecord = false;
        public static int HighestResult;

        public static event UnityAction DataPermissioned;
        public static event UnityAction DataLoaded;
        public static event UnityAction DataProhibited;

        public static void Request()
        {
            PlayerAccount.RequestPersonalProfileDataPermission(OnDataPermissioned, OnErrorCallback);
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
            IsDataSetted = true;
            Debug.Log("Data loaded");
            DataLoaded?.Invoke();
        }
    }
}