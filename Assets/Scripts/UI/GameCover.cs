using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCover : EndScreen
{
    [SerializeField] private float _enableAfter = 1.13f;

    private void Start()
    {
        StartCoroutine(EnableGameCover());
    }

    protected override void OnButtonClick()
    {
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