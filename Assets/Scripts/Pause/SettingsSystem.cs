using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Por Jere
public class SettingsSystem : MonoBehaviour
{
    public void Done()
    {
        SceneManager.LoadScene ("Sandbox");
        Time.timeScale = 1;
    }
}
