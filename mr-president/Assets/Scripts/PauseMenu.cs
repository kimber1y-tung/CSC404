using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{   

    GameObject pauseScreen;
    bool paused = false;

    void Start()
    {
        pauseScreen = GameObject.Find("Pause Screen");
        // Debug.Log(paused);
    }

    void Update()
    {
        if (paused && (Input.GetKeyDown(KeyCode.JoystickButton9) || Input.GetKeyDown(KeyCode.Escape))){
            ResumeGame();
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

    public void QuitGame()
    {   
        Debug.Log("Quit");
        Application.Quit();
    }

    public void PauseGame()
    {   
        Debug.Log("the game is paused");
        paused = true;

        // Time.timeScale = 0f;
    }

    public void ResumeGame()
    {   
        FindObjectOfType<Waypoints>().resume();
        FindObjectOfType<TargetMovement>().resume();
        FindObjectOfType<rotator>().resume();
        
        Debug.Log("unpaused");
        paused = false;
        pauseScreen.SetActive(false);
        // make the cursor locked
        Cursor.lockState = CursorLockMode.Locked;
    }
}
