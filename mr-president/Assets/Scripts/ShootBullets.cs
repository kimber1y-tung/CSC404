using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullets : MonoBehaviour
{

    public GameObject[] bullets;
    public GameObject shooter;
    public GameObject target;

    public GameObject cooldownText;
    public GameObject tripwire;

    public AudioClip[] shootingSounds;

    float lastShot = 1000;
    public float cooldown = 3;

    public string direction = "right";

    void Start()
    {
        target = GameObject.Find("President");
        cooldownText.GetComponent<TMPro.TextMeshPro>().text = "";
    }

    void Update(){
        lastShot += Time.deltaTime;
        if (lastShot >= cooldown){
            cooldownText.GetComponent<TMPro.TextMeshPro>().text = "";
            tripwire.GetComponent<Renderer>().enabled = true;
        }
        else {
            cooldownText.GetComponent<TMPro.TextMeshPro>().text = Mathf.Round(cooldown - lastShot).ToString();
            tripwire.GetComponent<Renderer>().enabled = false;
        }
    }

    void OnTriggerEnter(Collider collision) 
    {        
        if (collision.gameObject.tag == "Agent" || collision.gameObject.name == "President"){
            if (lastShot >= cooldown){
                AudioSource.PlayClipAtPoint(shootingSounds[Random.Range(0, shootingSounds.Length)], transform.position);
                
                Invoke("Shoot", 0.1f);
                lastShot = 0;
            }
        }
    }

    void Shoot(){
        //Create a new bullet
        Debug.Log("CREATING BULLET");
        GameObject newBullet = Instantiate(bullets[Random.Range(0, bullets.Length)], shooter.transform.position, shooter.transform.rotation) as GameObject;
        newBullet.GetComponent<BulletCollision>().direction = direction;

        //Parent it to get a less messy workspace
        newBullet.transform.parent = shooter.transform;
    }
}