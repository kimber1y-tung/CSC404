using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger3 : MonoBehaviour
{
    bool isActive = false;
    GameObject tutorial3;

    // Start is called before the first frame update
    void Start()
    {
        tutorial3 = GameObject.Find("Tutorial 3");
        tutorial3.SetActive(false);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.name == "President" && !isActive)
        {

            var agents = GameObject.Find("Agents").transform;

            foreach (Transform child in agents)
            {
                child.gameObject.GetComponent<agent_movement>().isGettingDown = true;

            }
            isActive = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            tutorial3.SetActive(true);
        }
    }
}
