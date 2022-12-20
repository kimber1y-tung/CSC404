using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{

    GameObject pauseScreen;
    GameObject target;
    bool over = false;

    void Start()
    {
        // gameOverScreen = GameObject.Find("Game Over Screen");
        // getDownCoolDown = GameObject.Find("Get down cooldown");
        // gameHasEnded = false;
        // gameOverScreen.SetActive(false);
        pauseScreen = GameObject.Find("Pause Screen");
        pauseScreen.SetActive(false);
        target = GameObject.Find("President");
    }

    public void SetGamePause()
    {
        over = FindObjectOfType<VictoryManager>().ReturnWinStatus() || FindObjectOfType<GameOverManager>().ReturnOverStatus();
        // Debug.Log(won);

        // if game over disable pause function
        if (target && !over)
        {
            pauseScreen.SetActive(true);
            // make the cursor visible
            Cursor.lockState = CursorLockMode.None;
            Debug.Log(over);
        }
        // pauseScreen.SetActive(true);
        // // make the cursor visible
        // Cursor.lockState = CursorLockMode.None;
    }

}
