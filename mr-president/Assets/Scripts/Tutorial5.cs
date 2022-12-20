using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial5 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            Debug.Log("Start next lvl by controller");
            CloseScreen();
        }
    }

    public void CloseScreen()
    {

        var agents = GameObject.Find("Agents").transform;
        foreach (Transform child in agents)
        {
            child.gameObject.GetComponent<agent_movement>().isGettingDown = false;

        }

        GameObject.Find("Tutorial 5").SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
