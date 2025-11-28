using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string OldSceneName { get; set; }
    public string NewSceneName { get; set; }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void ChangeSceneByNumber(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void ChangeSceneByName(string name, LoadSceneMode loadSceneMode)
    {
        NewSceneName = name;
        SceneManager.LoadScene(name, loadSceneMode);
    }

    public void ChangeSceneAsyncByName(string name, LoadSceneMode loadSceneMode)
    {
        NewSceneName = name;
        SceneManager.LoadSceneAsync(name, loadSceneMode);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != NewSceneName) return;
        if (OldSceneName == null) return;
        SceneManager.UnloadSceneAsync(OldSceneName);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
