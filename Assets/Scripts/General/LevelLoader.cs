using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace General
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private Levels _levels;

        private int _sceneIndex;

        private const int StartLevel = 0;
        private const string LevelKey = nameof(LevelKey);

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

            Level = PlayerPrefs.GetInt(LevelKey, StartLevel);

        }

        private void Start()
        {
            _sceneIndex = GetLevelIndex();

            if (SceneManager.GetActiveScene().buildIndex > 0)
            {
                return;
            }
            DontDestroyOnLoad(gameObject);
            Load();

            
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
        }

        private void Load()
        {
            EventsSender.Instance.SendLevelStartEvent(Level);
            SceneManager.LoadScene(_levels.Names[_sceneIndex]);
        }
    }
}