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
        foreach (MementoEntity entity in _mementoEntities) entity.RestoreFromGlobalCache();
    }

    public void ChangeSceneAsyncByName(object sender, object data)
    {
        foreach(MementoEntity entity in _mementoEntities) entity.SaveToGlobalCache();

        OldSceneName = SceneManager.GetActiveScene().name;
        var name = (string)data;
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
        if (scene.name != NewSceneName)
            return;
        if (OldSceneName == null)
            return;
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
