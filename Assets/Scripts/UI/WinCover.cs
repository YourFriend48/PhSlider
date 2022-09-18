using UnityEngine.SceneManagement;

public class WinCover : Screen
{
    protected override void OnButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}