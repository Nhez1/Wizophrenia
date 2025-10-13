using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MenuManager : MonoBehaviour
{
    public Canvas[] allMenus;
    public Canvas wantedActiveMenu;

    void Start()
    {
        Time.timeScale = 1.0f;

        if (allMenus.Length > 0)
        {
            foreach (var menu in allMenus)
            {
                menu.gameObject.SetActive(false);
                if (wantedActiveMenu != null) wantedActiveMenu.gameObject.SetActive(true);
            }
        }
    }

    public void ChangeSceneByNumber(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void ChangeSceneByName(string name)
    {
        SceneManager.LoadScene(name);
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
