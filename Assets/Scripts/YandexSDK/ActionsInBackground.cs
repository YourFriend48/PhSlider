using UnityEngine;
using Agava.WebUtility;

namespace YandexSDK
{
    public class ActionsInBackground : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour[] _gameSpeedChangers;
        [SerializeField] private WinCover _winCover;
        [SerializeField] private Upgrading _powerUpgrade;

        private float _volume = 1;

        //private IGameSpeedChangable[] CastedSpeedChangers => GetArray();

        //private void OnValidate()
        //{
        //    //IGameSpeedChangable[] GameSpeedChangers = new IGameSpeedChangable[_gameSpeedChangers.Length];

        //    for (int i = 0; i < _gameSpeedChangers.Length; i++)
        //    {
        //        if (_gameSpeedChangers[i] is not IGameSpeedChangable)
        //        {
        //            Debug.LogError($"{_gameSpeedChangers[i]} need to implement {nameof(IGameSpeedChangable)}");
        //            _gameSpeedChangers[i] = null;
        //        }
        //    }
        //}

        //private IGameSpeedChangable[] GetArray()
        //{
        //    IGameSpeedChangable[] result = new IGameSpeedChangable[_gameSpeedChangers.Length];
        //    for (int i = 0; i < _gameSpeedChangers.Length; i++)
        //    {
        //        result[i] = (IGameSpeedChangable)_gameSpeedChangers[i];
        //    }

        //    return result;
        //}

        private void OnEnable()
        {
            _winCover.GameSpeedChanged += OnGameSpeedChanged;
            _powerUpgrade.GameSpeedChanged += OnGameSpeedChanged;
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;

            //foreach (IGameSpeedChangable gameSpeedChanger in CastedSpeedChangers)
            //{
            //    gameSpeedChanger.GameSpeedChanged += OnGameSpeedChanged;
            //}
        }

        private void OnDisable()
        {
            _winCover.GameSpeedChanged -= OnGameSpeedChanged;
            _powerUpgrade.GameSpeedChanged -= OnGameSpeedChanged;
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;

            //foreach (IGameSpeedChangable gameSpeedChanger in CastedSpeedChangers)
            //{
            //    gameSpeedChanger.GameSpeedChanged -= OnGameSpeedChanged;
            //}
        }

        private void OnInBackgroundChange(bool inBackground)
        {
            // Use both pause and volume muting methods at the same time.
            // They're both broken in Web, but work perfect together. Trust me on this.

            AudioListener.volume = inBackground ? 0f : _volume;
            Time.timeScale = inBackground ? 0f : _volume;

            if (AudioListener.volume == 0)
            {
                AudioListener.pause = false;
            }
            else
            {
                AudioListener.pause = true;
            }
        }

        private void OnGameSpeedChanged(float value)
        {
            _volume = value;
            //OnInBackgroundChange(false);
            if (_volume == 0)
            {
                AudioListener.pause = false;
            }
            else
            {
                AudioListener.pause = true;
            }

            AudioListener.volume = _volume;
            Time.timeScale = _volume;
        }
    }
}