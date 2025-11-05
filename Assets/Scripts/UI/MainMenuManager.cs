using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header(" Main Menus ")]
    public Canvas[] allMenus;
    public Canvas wantedActiveMenu;


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
}
