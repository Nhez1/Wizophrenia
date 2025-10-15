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

    public PauseSystem pause;
    bool sweetch;

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

    void ActivateInventory()
    {
        sweetch = !sweetch;

        inventoryMenu.SetActive(sweetch);
        if (sweetch) pause.Pause();
        else pause.Unpause();
    }

    void ActivatePauseMenu() => pauseMenu.SetActive(true);

    private void OnEnable()
    {
        InputController.OnPause += ActivatePauseMenu;
        InputController.OnBagOpen += ActivateInventory;
    }
    private void OnDisable()
    {
        InputController.OnPause -= ActivatePauseMenu;
        InputController.OnBagOpen -= ActivateInventory;
    }
}