using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Por Jere

public class MenuSystem : MonoBehaviour
{
    public string newGameScene = "Sandbox";


   public void NewGame ()
   {
    SceneManager.LoadScene(newGameScene);
    Time.timeScale = 1;
   }

//Cuando sepa hacer guardados voy a desarrollar el continue, por ahora queda deshabilitado 

   /* public void Continue ()
   {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }
*/

    public void Exit ()
   {
    Debug.Log ("Saliendo del juego");
    Application.Quit();
   }
   

    public void Credits ()
   {
    SceneManager.LoadScene("Credits");
   }

    public void Settings ()
   {
    SceneManager.LoadScene("Settings");
   }



}
