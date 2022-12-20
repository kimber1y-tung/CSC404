using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial1 : MonoBehaviour
{
    Transform agents;
    bool appeared = false;


    void Start()
    {
        if (!appeared)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            agents = GameObject.Find("Agents").transform;

            foreach (Transform child in agents)
            {
                child.gameObject.GetComponent<agent_movement>().isGettingDown = true;

            }
            appeared = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            Debug.Log("Start next lvl by controller");
            CloseScreen();
        }
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    public void CloseScreen()
    {
        GameObject.Find("Tutorial 1").SetActive(false);
        foreach (Transform child in agents)
        {
            child.gameObject.GetComponent<agent_movement>().isGettingDown = false;

        }
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
