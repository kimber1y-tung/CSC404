using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float smoothSpeed = 0.5f;
    public Vector3 offset;
    Transform target;
    public float sensitivity;
    public float pLerp = .02f;
    public float rLerp = .01f;
    GameObject president;

    void Start()
    {
        //target = GameObject.Find("rotator").transform;
        president = GameObject.Find("President");
        //Cursor.lockState = CursorLockMode.Locked;
    }
    
    void LateUpdate ()
    {
        /*transform.position = target.position + offset;*/
    }
    

    void FixedUpdate()
    {
        //float rotateHorizontal = Input.GetAxis("Mouse X");
        //float rotateVertical = Input.GetAxis("Mouse Y");
        /*transform.RotateAround(target.position, Vector3.up, -rotateHorizontal * sensitivity);
        transform.RotateAround(target.position, target.right, -rotateVertical * sensitivity);*/
        if (president && target){
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
