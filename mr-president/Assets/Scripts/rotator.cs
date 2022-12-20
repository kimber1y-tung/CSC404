using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotator : MonoBehaviour
{

    public Vector2 turn;
    public GameObject target;

    public bool pause = false;

    private Transform charactor;
    private Transform pos;
    private Vector3 offset;

    private float R_H;

    private float lookAngle;

    private float moveSpeed = 10f;
    private float turnSpeed = 1.5f;

    private Transform mainCamera;
    bool won;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("President");
        charactor = GameObject.Find("MoveTarget").GetComponent<Transform>();
        pos = GameObject.Find("rotator").GetComponent<Transform>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Transform>();
        offset = mainCamera.position - pos.position;
    }

    // Update is called once per frame
    void Update()
    {   
        won = FindObjectOfType<VictoryManager>().ReturnWinStatus();
        if (!pause && !won && (Input.GetKeyDown(KeyCode.JoystickButton9) || Input.GetKeyDown(KeyCode.Escape))){
            // Debug.Log("1 pause pressed");
            FindObjectOfType<PauseManager>().SetGamePause();
            // Debug.Log("2 pause pressed");
            FindObjectOfType<PauseMenu>().PauseGame();
            // Debug.Log("3 pause pressed");
            pause = true;

        }

        if (target && !pause){

            R_H = Input.GetAxis("Mouse X");

            detect(Vector3.forward, Vector3.back);
            detect(Vector3.back, Vector3.forward);
            detect(Vector3.left, Vector3.right);
            detect(Vector3.right, Vector3.left);

            HandleCameraRotate();
            FollowCharactor(Time.deltaTime);
        }

        if (pause){
            // Debug.Log("camera is locked");
            turn.x += 0f;

        }

        if (pause){
            // Debug.Log("camera is locked");
            turn.x += 0f;
        }
    }
    
    private void FollowCharactor(float deltaTime)
    {
        mainCamera.position = Vector3.Lerp(mainCamera.position, pos.position + pos.TransformDirection(offset), deltaTime * moveSpeed);
    }

    private void HandleCameraRotate()
    {
        transform.position = charactor.position;//设定水平旋转角度
        lookAngle += R_H * turnSpeed;
        transform.localEulerAngles = new Vector3(0, lookAngle, 0);//将得到的角度赋值给empty的localEulerAngles
    }

    // check 4 direc
    void checkWallCollision(Vector3 dir, Vector3 inverse_dir)
    {
        RaycastHit hitInfo;
        Vector3 _dir = Camera.main.transform.TransformDirection(dir);
        if (Physics.Raycast(Camera.main.transform.position, _dir, out hitInfo, 0.5f))
        {
            
            float dis = hitInfo.distance;
            Vector3 correction = Vector3.Normalize(Camera.main.transform.TransformDirection(inverse_dir)) * dis;
            Camera.main.transform.position += correction;
        }
    }

    // check as sphere
    void detect(Vector3 dir, Vector3 inverse_dir)
    {
        RaycastHit hitInfo;
        Vector3 _dir = Camera.main.transform.TransformDirection(dir);
        if (Physics.SphereCast(Camera.main.transform.position, 0.5f, _dir, out hitInfo, 0.5f))
        {
            // Debug.Log(hitInfo.collider);
            // Debug.Log("hi");
            if(!(hitInfo.collider.ToString().Contains("PersonalSpace") || hitInfo.collider.ToString().Contains("Column")))
            {
                float dis = hitInfo.distance;
                Vector3 correction = Vector3.Normalize(Camera.main.transform.TransformDirection(inverse_dir)) * dis;
                // correction.z = 0f;
                // correction.y = 0f;
                Camera.main.transform.position += correction;
            }
        }
    }
 

    private void AvoidCrossWall()
    {
        RaycastHit hitInfo;
        Vector3 fwd = Camera.main.transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(Camera.main.transform.position, fwd, out hitInfo, 0.5f))
        {
            float dis = hitInfo.distance;
            Vector3 correction = Vector3.Normalize(Camera.main.transform.TransformDirection(Vector3.back)) * dis;
            Camera.main.transform.position += correction;
        }
    }

    public void resume()
    {
        pause = false;
    }
}
