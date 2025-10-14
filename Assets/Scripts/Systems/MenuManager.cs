using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header(" Main Menu ")]
    public Canvas[] allMenus;
    public Canvas wantedActiveMenu;

    [Header(" Game ")]
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject inventoryMenu;

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

    void ActivatePauseMenu() => pauseMenu.SetActive(true);

    private void OnEnable()
    {
        InputController.OnPause += ActivatePauseMenu;
    }
    private void OnDisable()
    {
        InputController.OnPause -= ActivatePauseMenu;
    }
}