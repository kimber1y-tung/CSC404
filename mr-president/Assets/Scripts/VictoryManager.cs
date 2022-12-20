using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryManager : MonoBehaviour
{
    GameObject gameWinScreen;
    GameObject getDownCoolDown;
    GameObject target;
    GameObject bgMusic;
    bool gameHasWon = false;

    
    public AudioSource audio;
    public AudioClip victoryAudio;

    void Start()
    {
        gameWinScreen = GameObject.Find("Victory Screen");
        getDownCoolDown = GameObject.Find("Get down cooldown");
        bgMusic = GameObject.Find("Background Music");
        gameHasWon = false;
        target = GameObject.Find("President");
        gameWinScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!(target) || gameHasWon)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if (gameHasWon){
            var agents = GameObject.Find("Agents").transform;
            foreach(Transform child in agents)
            {
                child.gameObject.GetComponent<agent_movement>().Victory();
            }
            target.GetComponent<Waypoints>().Victory();
            if (Input.GetKeyDown(KeyCode.JoystickButton1)){
                Debug.Log("return menu by controller");
                SceneManager.LoadScene("NewMenu");
            }
            if (Input.GetKeyDown(KeyCode.JoystickButton2))
            {
                Debug.Log("Start next lvl by controller");
                StartNextLevel();
            }
        }
    }

    public void SetGameWin()
    {
        if (gameHasWon == false)
        {
            gameHasWon = true;
            audio.clip = victoryAudio;
            bgMusic.GetComponent<AudioSource>().Stop();
            audio.Play();
            Debug.Log("Set Game Over function is called");
            getDownCoolDown.SetActive(false);
            gameWinScreen.SetActive(true);
        }
    }

    /*public void RestartGame()
    {
        Debug.Log("Start Game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }*/

    public void ReturnMenu()
    {
        Debug.Log("return to menu");
        SceneManager.LoadScene("NewMenu");
    }

    public void StartNextLevel()
    {
        string currscene = SceneManager.GetActiveScene().name;
        string nextscene = currscene.Substring(0,6) + (int.Parse(currscene.Substring(6)) +1);
        SceneManager.LoadScene(nextscene);
    }

    public bool ReturnWinStatus()
    {
        return gameHasWon;
    }
}
