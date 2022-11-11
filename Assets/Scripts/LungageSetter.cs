using Agava.YandexGames;
using UnityEngine;
using YandexSDK;

public class LungageSetter : MonoBehaviour
{
    private void Awake()
    {
        string currentLanguge;

        switch (Languge.Name)
        {
            case "en":
                currentLanguge = "English";
                break;
            case "ru":
                currentLanguge = "Russian";
                break;
            case "tr":
                currentLanguge = "Turkish";
                break;

            default:
                currentLanguge = "English";
                break;
        }

        Lean.Localization.LeanLocalization.SetCurrentLanguageAll(currentLanguge);
    }
}
