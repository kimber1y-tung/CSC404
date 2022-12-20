using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFurniture : MonoBehaviour
{
    public GameObject furniture;
    public AudioSource audio;
    bool fallen = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collision) 
    {        
        if ((collision.name == "President" || collision.tag == "Agent") && !fallen){
            Debug.Log("FALLING");
            fallen = true;
            audio.Play();
            
            Invoke ("Drop", 0.2f);

            // //Add velocity to the bullet with a rigidbody
            // newBullet.GetComponent<Rigidbody>().velocity = speed * transform.forward;
            // Vector3 movement = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
        }
    }

    void Drop(){

        furniture.GetComponent<Rigidbody>().useGravity = true;
        //Create a new bullet
        // GameObject fallingFurniture = Instantiate(furniture, spawnLocation.transform.position, spawnLocation.transform.rotation) as GameObject;

        // //Parent it to get a less messy workspace
        // fallingFurniture.transform.parent = gameObject.transform.parent;
    }
}
