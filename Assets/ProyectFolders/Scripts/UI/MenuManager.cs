using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header(" Game Menus ")]
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _alchemyMenu;
    [SerializeField] private GameObject _inventoryMenu;
    [SerializeField] private GameObject _winMenu;
    [SerializeField] private GameObject _healthGameOverMenu;
    [SerializeField] private GameObject _sanityGameOverMenu;
    private CanvasGroup _inventory;

    //private PauseSystem _pause;
    //private MouseLook _mouse;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private MouseLook _mouse;
    [SerializeField] private PauseSystem _pauseSystem;

    private bool _anyMenuOpen = false;
    private bool _anyEndGameScreensOpen = false;

    private Life _playerLife;

    private void Start()
    {
        Time.timeScale = 1.0f;

        //_pause = GetComponent<PauseSystem>();
        _mouse = Camera.main.GetComponent<MouseLook>();

        _inventory = GetComponentInChildren<CanvasGroup>();

        var p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _playerLife = p.Life;
        _playerLife.OnHealthGameOver += HealthGameOver;
    }

    #region Inventory
    private bool _invSwitch = false;

    void ActivateInventory()
    {
        if (_inventory == null) return;
        _inventory.alpha = 1f;
        _anyMenuOpen = true;
        _mouse.LockCamera();
    }
    void DeactivateInventory()
    {
        if (_inventory == null) return;
        _mouse.UnlockCamera();
        _anyMenuOpen = false;
        _inventory.alpha = 0f;
    }

    public void SwitchInventory()
    {
        if (!_invSwitch && _anyMenuOpen && _anyEndGameScreensOpen) return;
        _invSwitch = !_invSwitch;

        if (_invSwitch) ActivateInventory();
        else DeactivateInventory();
    }
    #endregion

    #region Alchemy
    private bool _alchSwitch = false;

    public void ActivateAlchemyMenu()
    {
        _alchemyMenu.SetActive(true);

        ActivateInventory();
        _anyMenuOpen = true;
    }
    public void DeactivateAlchemyMenu()
    {
        _alchemyMenu.SetActive(false);

        DeactivateInventory();
        _anyMenuOpen = false;
    }

    private void SwitchAlchemy()
    {
        if (!_alchSwitch && _anyMenuOpen && _anyEndGameScreensOpen) return;
        _alchSwitch = !_alchSwitch;

        if (_alchSwitch)
        {
            ActivateAlchemyMenu();
            
        }
        else
        {
            DeactivateAlchemyMenu();
            
        }

     }
    #endregion

    //Esto es re villero pero la otra era tener que pasarle la referencia de la cámara al botón de resumir del menú de pausa y eso era peor
    public void UnlockCamera() => _mouse.UnlockCamera();

    //PauseMenu
    public void ShowPauseMenu()
    {
        if (_anyEndGameScreensOpen == true) return;
        if (_anyEndGameScreensOpen == false) 
        { 
        _inventory.alpha = 0f;
        _alchemyMenu.SetActive(false);

        _invSwitch = false;
        _alchSwitch = false;

        _pauseMenu.SetActive(true);
        _mouse.LockCamera();
        _anyMenuOpen = false;
        }
    }
    public void HidePauseMenu()
    {
        if (_anyEndGameScreensOpen == false)
        {
            _pauseMenu.SetActive(false);
            _mouse.UnlockCamera();
            _anyMenuOpen = false; 
        }
    }

    public void TogglePauseMenu()
    {
        if (_pauseMenu.activeSelf)
            HidePauseMenu();
        else
            ShowPauseMenu();
    }

    public void ResumeGame()
    {
        _pauseSystem.Unpause();
        HidePauseMenu();
    }



    #region Win&GameOver
    void HealthGameOver()
    {
        _mouse.LockCamera();
        _pauseSystem.Pause();
        _healthGameOverMenu.SetActive(true);
        _anyMenuOpen = true;
        _anyEndGameScreensOpen = true;
    }
    void SanityGameOver()
    {
        _mouse.LockCamera();
        _pauseSystem.Pause();
        _sanityGameOverMenu.SetActive(true);
        _anyMenuOpen = true;
        _anyEndGameScreensOpen = true;
    }
    void Win()
    {
        _mouse.LockCamera();
        _pauseSystem.Pause();
        _winMenu.SetActive(true);
        _anyMenuOpen = true;
        _anyEndGameScreensOpen = true;
    }
    #endregion

    private void OnEnable()
    {
        CauldronObject.OnAlchemyToggle += SwitchAlchemy;
        Sanity.OnSanityGameOver += SanityGameOver;
        Sanity.OnGameWin += Win;

    }
    private void OnDisable()
    {
        CauldronObject.OnAlchemyToggle -= SwitchAlchemy;
        _playerLife.OnHealthGameOver -= HealthGameOver;
        Sanity.OnSanityGameOver -= SanityGameOver;
        Sanity.OnGameWin -= Win;
    }
}