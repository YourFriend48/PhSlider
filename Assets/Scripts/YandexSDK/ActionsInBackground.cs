using UnityEngine;
using Agava.WebUtility;

namespace YandexSDK
{
    public class ActionsInBackground : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour[] _gameSpeedChangers;

        private float _volume = 1;

        private IGameSpeedGhangable[] CastedSpeedChangers => GetArray();

        private void OnValidate()
        {
            IGameSpeedGhangable[] GameSpeedChangers = new IGameSpeedGhangable[_gameSpeedChangers.Length];

            for (int i = 0; i< _gameSpeedChangers.Length; i++)
            {
                if(_gameSpeedChangers[i] is not IGameSpeedGhangable)
                {
                    Debug.LogError($"{_gameSpeedChangers[i].name} need to implement {nameof(IGameSpeedGhangable)}");
                    _gameSpeedChangers[i] = null;
                        }
            }
        }

        private IGameSpeedGhangable[] GetArray()
        {
            IGameSpeedGhangable[] result = new IGameSpeedGhangable[_gameSpeedChangers.Length];
            for (int i = 0; i < _gameSpeedChangers.Length; i++)
            {
                result[i] = (IGameSpeedGhangable)_gameSpeedChangers[i];
            }

            return result;
        }

        private void OnEnable()
        {
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;

            foreach(IGameSpeedGhangable gameSpeedChanger in CastedSpeedChangers)
            {
                gameSpeedChanger.GameSpeedChanged += OnGameSpeedChanged;
            }
        }

        private void OnDisable()
        {
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;

            foreach (IGameSpeedGhangable gameSpeedChanger in CastedSpeedChangers)
            {
                gameSpeedChanger.GameSpeedChanged -= OnGameSpeedChanged;
            }
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
            if(_volume == 0)
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