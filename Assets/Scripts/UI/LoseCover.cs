using UnityEngine.SceneManagement;

public class LoseCover : Screen
{
    protected override void OnButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}