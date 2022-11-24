using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;

namespace YandexSDK
{
    public static class YandexAuthorizing
    {
        public static event UnityAction Authorized;
        public static bool IsAuthorised = false;

        public static void Authorise()
        {
            PlayerAccount.Authorize(OnSucsessAuthorize);
        }

        private static void OnSucsessAuthorize()
        {
            IsAuthorised = true;
            Authorized?.Invoke();
        }
    }
}