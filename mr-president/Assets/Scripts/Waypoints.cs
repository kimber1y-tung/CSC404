using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public GameObject[] waypoints;
    int current = 0;
    public float speed;
    float getDownActivationTime = 2f;
    float getDownCooldownTime = 6f;
    float WPradius = 1;
    public bool isGettingDown = false;
    public bool isGetDownReady = true;
    public bool pause = false;

    public AudioSource audio;
    public AudioClip[] dyingSounds;
    public AudioClip[] getDownSounds;

    private Animator animator;

    Rigidbody rb;
    bool won;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
        audio = gameObject.GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // pause is pressed 
        won = FindObjectOfType<VictoryManager>().ReturnWinStatus();
        if (!pause && !won && (Input.GetKeyDown(KeyCode.JoystickButton9) || Input.GetKeyDown(KeyCode.Escape)))
        {
            // Debug.Log("1 pause pressed");
            FindObjectOfType<PauseManager>().SetGamePause();
            // Debug.Log("2 pause pressed");
            FindObjectOfType<PauseMenu>().PauseGame();
            // Debug.Log("3 pause pressed");
            pause = true;

        }
        if ((Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.JoystickButton1)) && isGetDownReady && !pause)
        {
            isGettingDown = true;
            audio.clip = getDownSounds[Random.Range(0, getDownSounds.Length)];
            audio.Play();
            StartCoroutine(FindObjectOfType<GetDownSpeechBubble>().ShowSpeechBubble());
            StartCoroutine(GetDown());
        }
        if (!isGettingDown && !pause)
        {
            MovePresident();
        }
    }

    IEnumerator GetDown()
    {
        isGetDownReady = false;
        float yPosition = transform.position.y;
        //get down motion
        transform.rotation *= Quaternion.AngleAxis(90, Vector3.right);
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);

        // shake cam
        CameraShake.Instance.ShakeCamera(5f, 1f);

        yield return new WaitForSeconds(getDownActivationTime);

        //get up motion
        transform.rotation *= Quaternion.AngleAxis(-90, Vector3.right);
        transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);

        //resume movement, get down cooldown
        isGettingDown = false;
        yield return new WaitForSeconds(getDownCooldownTime);

        //cooldown complete
        isGetDownReady = true;
    }

    void MovePresident()
    {
        if (Vector3.Distance(transform.position, waypoints[waypoints.Length - 1].transform.position) < 1.5f)
        {
            FindObjectOfType<VictoryManager>().SetGameWin();
        }
        if (Vector3.Distance(waypoints[current].transform.position, transform.position) < WPradius)
        {
            if (current < waypoints.Length - 1)
            {
                current++;
                // Vector3 angle = waypoints[current].transform.position - transform.position;
                // transform.forward = angle;
                var qTo = Quaternion.LookRotation(waypoints[current].transform.position - transform.position);
                qTo = Quaternion.Slerp(qTo, transform.rotation, 10 * Time.deltaTime);
                rb.MoveRotation(qTo);
                Debug.Log("CHANGE");
            }
            // if (waypoints[current].name.Substring(3) == "waypoint")
            // {
            //     waypoints[current].transform.GetChild(0).gameObject.SetActive(true);
            // }
            // else if (waypoints[current].name.Substring(3) == "exit waypoint")
            // {
            //     waypoints[current - 1].transform.GetChild(0).gameObject.SetActive(false);
            // }
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Harmful")
        {
            Debug.Log(gameObject.name);
            Debug.Log("president is hit");
            Unalive();
        }
    }

    public void resume()
    {
        pause = false;
    }

    public void Unalive()
    {
        AudioSource.PlayClipAtPoint(dyingSounds[Random.Range(0, dyingSounds.Length)], transform.position);
        Destroy(gameObject);
        FindObjectOfType<GameOverManager>().SetGameOver();
    }

    public void Victory()
    {
        animator.SetFloat("hasWon", 1f);
    }
}
