using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    List<Transform> chars = new List<Transform>();
    GameObject prez;
    Transform smallr;
    Transform bigr;
    public float lethalRad;
    public float knockRad;
    public float sizzleRange;
    public AudioSource audio;
    public AudioClip explosionSound;
    private bool shouldRagdoll;


    // Start is called before the first frame update
    void Start()
    {
        lethalRad = 3f;
        knockRad = 10f;
        sizzleRange = 20f;
        var agents = GameObject.Find("Agents").transform;
        prez = GameObject.Find("President");
        shouldRagdoll = false;

        foreach(Transform child in agents)
        {
            chars.Add(child);
            // Debug.Log("added agent");
        }
        chars.Add(prez.transform);
        // Debug.Log("added Prez");

        smallr = transform.Find("smallrad");
        bigr = transform.Find("bigrad");

        smallr.localScale = new Vector3(lethalRad, 0.001f, lethalRad);
        bigr.localScale = new Vector3(knockRad, 0.001f, knockRad);
    }

    // Update is called once per frame
    void Update()
    {
        if (CharInRange())
        {
            Explode();
            Destroy(this.gameObject);
        }
        
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
            else
            {
                chars.Remove(c);
            }
        }
        return false;
    }
    void Explode()
    {
        Debug.Log("BOOM!!!");

        // shake cam
        CameraShake.Instance.ShakeCamera(7f, 1f);

        AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        foreach (Transform c in chars)
        {
            if (c != null)
            {
                float dist = Vector3.Distance(transform.position, c.position);
                if (dist < lethalRad)
                {
                    if (c.gameObject.name == "President")
                    {
                        c.gameObject.GetComponent<Waypoints>().Unalive();
                    }
                    else
                    {
                        c.gameObject.GetComponent<agent_movement>().Unalive(shouldRagdoll, false);
                    }
                }
                else if (dist < knockRad-2)
                {
                    Debug.Log("weeee");
                    if (c.gameObject.name == "President")
                    {
                        Debug.Log("president:" + dist);
                        c.gameObject.GetComponent<Waypoints>().Unalive();
                    }
                    else
                    {
                        c.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(c.position - transform.position) * 40f, ForceMode.Impulse);
                    }
                }
            }
        }
    }
}
