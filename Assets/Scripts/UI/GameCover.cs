using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using General;
using GameAnalyticsSDK;
using System;

public class GameCover : EndScreen
{
    [SerializeField] private float _enableAfter = 1.13f;

    public static event Action<int> Reloaded;

    private void Start()
    {
        StartCoroutine(EnableGameCover());
    }

    protected override void OnButtonClick()
    {
        //EventsSender.Instance.SendLevelRestartEvent(LevelLoader.Instance.Level);
        Reloaded?.Invoke(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator EnableGameCover()
    {
        yield return new WaitForSeconds(_enableAfter);
        Open();
    }

    protected override void Disable()
    {
    }

    protected override void Enable()
    {
    }

    protected override void OnAwake()
    {
    }
}