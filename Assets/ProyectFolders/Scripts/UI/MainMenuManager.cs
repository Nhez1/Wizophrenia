using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header(" Main Menus ")]
    public Canvas[] allMenus;
    public Canvas wantedActiveMenu;

    [Header("Scene References")]
    [SerializeField] public SceneField _startinglevel;
    //con SceneField hice que se puedan agregar escenas desde los menus de unity, drag & drop, mucho mas facil y no hay que referenciar en codigo o por nombre o numero

    void Start()
    {
        if (allMenus.Length > 0)
        {
            foreach (var menu in allMenus)
            {
                menu.gameObject.SetActive(false);
                if (wantedActiveMenu != null) wantedActiveMenu.gameObject.SetActive(true);
            }

            return;
        }
    }

    public void HideMenu()
    {
        for (int i = 0; i < allMenus.Length; i++)
        {
            allMenus[i].gameObject.SetActive(false);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(_startinglevel);
    }
}
