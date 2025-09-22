using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsSystem : MonoBehaviour
{
    public void Done()
    {
        SceneManager.LoadScene ("SampleScene");
        Time.timeScale = 1;
    }
}
