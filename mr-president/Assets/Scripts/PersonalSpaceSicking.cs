using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalSpaceSicking : MonoBehaviour
{
    public GameObject president;

    // Start is called before the first frame update
    void Start()
    {
        president = GameObject.Find("President");
        transform.position = president.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (president){
            transform.position = president.transform.position;
        }
    }
}
