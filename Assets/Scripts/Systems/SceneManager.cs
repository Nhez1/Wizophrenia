using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public void ChangeSceneByNumber(int index)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }

    public void ChangeSceneByName(string name)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }

    public void RestartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
