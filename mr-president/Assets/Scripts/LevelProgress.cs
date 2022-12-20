using UnityEngine;
using UnityEngine.UI;
public class LevelProgress : MonoBehaviour
{
    [SerializeField]
    private Image stripe1;
    [SerializeField]
    private Image stripe2;
    [SerializeField]
    private Image stripe3;
    [SerializeField]
    private GameObject president;
    private float totalDistance;
    private float distanceTravelled;
    private Transform currWaypoint;
    private Transform prevWaypoint;
    private Vector3 prevPosition;

    void Start()
    {
        var waypoints = GameObject.Find("waypoints").transform;
        currWaypoint = prevWaypoint = president.transform;
        //ignore y position so get-down doesn't count as level progress
        prevPosition = new Vector3(president.transform.position.x, 0, president.transform.position.z);
        totalDistance = distanceTravelled = 0;
        foreach (Transform child in waypoints)
        {
            currWaypoint = child;
            totalDistance += Vector3.Distance(currWaypoint.position, prevWaypoint.position);
            prevWaypoint = currWaypoint;
        }
        stripe1.fillAmount = stripe2.fillAmount = stripe3.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //ignore y position so get-down doesn't count as level progress
        Vector3 currPosition = new Vector3(president.transform.position.x, 0, president.transform.position.z);
        distanceTravelled += Vector3.Distance(currPosition, prevPosition);
        prevPosition = currPosition;
        stripe1.fillAmount = stripe2.fillAmount = stripe3.fillAmount = distanceTravelled / totalDistance;
    }
}
