using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace General
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private Levels _levels;
        //[SerializeField] private EventsSender _eventsSender;

        private int _sceneIndex;

        private const int StartLevel = 0;
        private const int FirstLevelIndex = 0;
        private const string LevelKey = nameof(LevelKey);
        private const string RegistrationDateKey = nameof(RegistrationDateKey);
        private const string SessionsKey = nameof(SessionsKey);

        public int Level { get; private set; }
        public static LevelLoader Instance { get; private set; }

        public int Count => _levels.Names.Count;

        private void Awake()
        {
            if (Instance && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            SendStartEvents();
            Level = PlayerPrefs.GetInt(LevelKey, StartLevel);

        }

        private void Start()
        {
            //_eventsSender.Init();

            _sceneIndex = GetLevelIndex();

            if (SceneManager.GetActiveScene().buildIndex > 0)
            {
                return;
            }

            DontDestroyOnLoad(gameObject);
            Load();
        }

        private void SendStartEvents()
        {
            int sessions = PlayerPrefs.GetInt(SessionsKey, 1);
            DateTime registrationDate;

            if (PlayerPrefs.HasKey(RegistrationDateKey))
            {
                registrationDate = DateTime.Parse(PlayerPrefs.GetString(RegistrationDateKey));
                PlayerPrefs.SetString(RegistrationDateKey, registrationDate.ToString());
            }
            else
            {
                registrationDate = DateTime.Now;
            }

            //_eventsSender.SendGameStartEvent(sessions);
            //_eventsSender.SendCustomUserEvent(sessions, 0, registrationDate.ToString("dd/MM/yyyy"), (DateTime.Today - registrationDate).Days);
        }

        public void Reload()
        {
            Load();
        }

        public void LoadNext()
        {
            PlayerPrefs.SetInt(LevelKey, ++Level);

            _sceneIndex = GetLevelIndex();

            Load();
        }

        private int GetLevelIndex()
        {
            return Level - Level / _levels.Names.Count * _levels.Names.Count;

            //if (Level < _levels.Names.Count)
            //{
            //    return Level;
            //}
            //else
            //{
            //}



            //int attempts = 0;
            //int newIndex;

            //do
            //{
            //    newIndex = Level < _levels.Names.Count ? Level : Random.Range(FirstLevelIndex, _levels.Names.Count);
            //}
            //while (_sceneIndex == newIndex && ++attempts < _levels.Names.Count);

            //return newIndex;
        }

        private void Load()
        {
            SceneManager.LoadScene(_levels.Names[_sceneIndex]);
        }
    }
}