using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Update(){
        // if (Input.GetKeyDown(KeyCode.JoystickButton1)){
        //     SceneManager.LoadScene("Level 1");
        // }
    }

    public void PlayGame(){
        SceneManager.LoadScene("Level 1");
    }

    public void SelectLevel()
    {
        SceneManager.LoadScene("Level Select");
    }

    public void QuitGame(){
        Debug.Log("Quit");
        Application.Quit();
    }
}
