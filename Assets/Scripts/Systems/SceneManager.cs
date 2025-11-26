using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public string OldSceneName { get; set; }
    public string NewSceneName { get; set; }

    private void Start()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void ChangeSceneByNumber(int index)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }

    public void ChangeSceneByName(string name, LoadSceneMode loadSceneMode)
    {
        NewSceneName = name;
        UnityEngine.SceneManagement.SceneManager.LoadScene(name, loadSceneMode);
    }

    public void ChangeSceneAsyncByName(string name, LoadSceneMode loadSceneMode)
    {
        NewSceneName = name;
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(name, loadSceneMode);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != NewSceneName) return;
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(OldSceneName);
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
