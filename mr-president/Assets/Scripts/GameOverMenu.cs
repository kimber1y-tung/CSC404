using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    void Update(){
        if (Input.GetKeyDown(KeyCode.JoystickButton1)){
            Debug.Log("Start Game by controller");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            Debug.Log("return menu by controller");
            SceneManager.LoadScene("NewMenu");
        }
    }
    
    public void RestartGame()
    {
        Debug.Log("Start Game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnMenu()
    {
        Debug.Log("return to menu");
        SceneManager.LoadScene("NewMenu");
    }
}
