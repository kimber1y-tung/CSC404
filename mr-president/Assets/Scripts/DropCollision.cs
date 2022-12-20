using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCollision : MonoBehaviour
{
    List<Transform> chars = new List<Transform>();
    GameObject prez;
    float lethalRad;
    float knockRad;

    public AudioSource audio;
    public AudioClip[] clangSounds;
    private bool shouldRagdoll;

    Transform dieCircle;
    Transform triggerCircle;
    Transform sign;

    // Start is called before the first frame update
    void Start()
    {
        var agents = GameObject.Find("Agents").transform;
        prez = GameObject.Find("President");
        dieCircle = gameObject.transform.parent.Find("Danger");
        triggerCircle = gameObject.transform.parent.Find("Trigger");
        sign = gameObject.transform.parent.Find("Sign");
        shouldRagdoll = false;

        lethalRad = gameObject.GetComponent<Renderer>().bounds.size.x;
        knockRad = gameObject.GetComponent<Renderer>().bounds.size.x * 1.5f;

        Debug.Log("LETHAL RADIUS: " + lethalRad);
        Debug.Log("KNOCK RAD: " + knockRad);

        foreach(Transform child in agents)
        {
            chars.Add(child);
            // Debug.Log("added agent");
        }
        chars.Add(prez.transform);
        // Debug.Log("added Prez");
    }

    // Update is called once per frame
    void Update()
    {
        // if (CharInRange())
        // {
        //     Flatten();
        // }
    }

    public bool CharInRange()
    {
        foreach(Transform c in chars)
        {
            if (c != null)
            {
                //Debug.Log(c);
                //Debug.Log(Vector3.Distance(transform.position, c.position));
                if (Vector3.Distance(transform.position, c.position) < lethalRad)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void Flatten(){
        foreach (Transform c in chars)
        {
            if (c != null)
            {
                // GET 2D DISTANCE
                float x_dist = Mathf.Abs(transform.transform.position.x - c.transform.position.x);
                float z_dist = Mathf.Abs(transform.transform.position.z - c.transform.position.z);

                float horizontal_dist = Mathf.Sqrt((x_dist * x_dist) + (z_dist * z_dist));

                // float dist = Vector3.Distance(transform.position, c.position);
                if (horizontal_dist < knockRad)
                {
                    if (c.gameObject.name == "President")
                    {
                        c.gameObject.GetComponent<Waypoints>().Unalive();

                    }
                    else
                    {
                        c.gameObject.GetComponent<Rigidbody>().AddForce((c.position - transform.position) * 5, ForceMode.Impulse);
                    }
                }
            }
        }
    }

    void OnCollisionEnter(Collision c){
        if (c.gameObject.name == "President")
        {
            c.gameObject.GetComponent<Waypoints>().Unalive();
        }
        foreach (Transform ch in chars)
            {
                if (ch != null)
                {
                    float x_dist = Mathf.Abs(transform.transform.position.x - ch.transform.position.x);
                    float z_dist = Mathf.Abs(transform.transform.position.z - ch.transform.position.z);

                    float horizontal_dist = Mathf.Sqrt((x_dist * x_dist) + (z_dist * z_dist));

                    // float dist = Vector3.Distance(transform.position, c.position);
                    if (horizontal_dist < lethalRad)
                    {
                        ch.gameObject.GetComponent<agent_movement>().Unalive(false, false);
                    }
                }
        }
        AudioSource.PlayClipAtPoint(clangSounds[Random.Range(0, clangSounds.Length)], transform.position);
        Destroy(triggerCircle.gameObject);
        Destroy(dieCircle.gameObject);
        Destroy(sign.gameObject);
        Destroy(gameObject);
    }
}
