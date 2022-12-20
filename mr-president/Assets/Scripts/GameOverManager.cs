using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{

    GameObject gameOverScreen;
    GameObject getDownCoolDown;
    GameObject bgMusic;
    bool gameHasEnded = false;

    public AudioSource audio;
    public AudioClip gameOverAudio;

    void Start()
    {
        gameOverScreen = GameObject.Find("Game Over Screen");
        getDownCoolDown = GameObject.Find("Get down cooldown");
        bgMusic = GameObject.Find("Background Music");
        gameHasEnded = false;
        gameOverScreen.SetActive(false);
    }

    public void SetGameOver()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            audio.clip = gameOverAudio;
            bgMusic.GetComponent<AudioSource>().Stop();
            audio.Play();
            Debug.Log("Set Game Over function is called");
            gameOverScreen.SetActive(true);
            getDownCoolDown.SetActive(false);
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

    public bool ReturnOverStatus()
    {
        return gameHasEnded;
    }
}
