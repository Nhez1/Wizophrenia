using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private MementoEntity[] _mementoEntities;

    public string OldSceneName { get; set; }
    public string NewSceneName { get; set; }

    private void Awake()
    {
        _mementoEntities = FindObjectsOfType<MementoEntity>();
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void ChangeSceneAsyncByName(object sender, object data)
    {
        var name = (string)data;
        Debug.Log("Scene name " + name);

        foreach (MementoEntity entity in _mementoEntities) entity.ForceSave();
        NewSceneName = name;

        SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
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

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        foreach (MementoEntity entity in _mementoEntities) entity.TryLoadStates();
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
