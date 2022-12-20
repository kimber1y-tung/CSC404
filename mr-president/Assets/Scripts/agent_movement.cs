using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class agent_movement : MonoBehaviour
{
    public float horizontalinput;
    public float verticalinput;
    float speed = 10.0f;
    float speedmultiple;
    float dragVar = 5f;

    public AudioSource randomSound;
    public AudioClip[] dyingSounds;
    public AudioClip[] smackSounds;
    public GameObject target;
    public GameObject president;
    private List<int> shooters = new List<int>();
    public List<Collider> RagdollSegments = new List<Collider>();
    public Rigidbody[] RagdollRigidBodies;
    private Vector3 targetpos;
    public float jumpAmount = 5;
    float getDownActivationTime = 2f;
    float getDownCooldownTime = 6f;
    public bool isRagdolled = false;
    public bool isGettingDown = false;
    public bool isGetDownReady = true;

    private Animator animator;

    // public bool EndScreenOn = false;

    // GameOverManager gameOverManager;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(6, 10);
        Physics.IgnoreLayerCollision(10, 10);
        SetBones();
        RagdollRigidBodies = GetComponentsInChildren<Rigidbody>();
        target = GameObject.Find("MoveTarget");
        president = GameObject.Find("President");
        targetpos = target.transform.position;
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        //speedmultiple = 2000f;

    }

    // Update is called once per frame
    void Update()
    {

        // check if president is still in the game, if not end game
        // if (!president){
        //     Debug.Log("game over");
        //     gameOverManager.SetGameOver();
        //     // SceneManager.LoadScene("End");
        //     // EndScreenOn = true;
        // }

        // else {
        animator.SetFloat("speed", rb.velocity.magnitude);
        if (president)
        {
            targetpos = target.transform.position;

            /*if (Input.GetKeyDown(KeyCode.Space))
            {
                Invoke("addJumpForce", Vector3.Distance(transform.position, targetpos) * Vector3.Distance(transform.position, targetpos) * 0.02f);
            }*/
            moveAgent();
            if ((Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.JoystickButton1)) && isGetDownReady)
            {
                isGettingDown = true;
                StartCoroutine(GetDown());
            }


        }
        // }

    }

    void addJumpForce()
    {
        rb.AddForce(Vector3.up * jumpAmount, ForceMode.Impulse);
    }

    void SetBones()
    {
        Collider[] colliders = this.gameObject.GetComponentsInChildren<Collider>();

        foreach (Collider c in colliders)
        {
            if (c.gameObject != this.gameObject)
            {
                c.isTrigger = true;
                RagdollSegments.Add(c);
            }
        }
    }

    IEnumerator GetDown()
    {
        if (!isRagdolled)
        {
            isGetDownReady = false;
            //get down motion
            transform.rotation *= Quaternion.AngleAxis(90, Vector3.right);

            // shake cam
            // CameraShake.Instance.ShakeCamera(5f, 2f);

            AudioSource.PlayClipAtPoint(smackSounds[Random.Range(0, smackSounds.Length)], transform.position);

            //stay in place on the ground
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            yield return new WaitForSeconds(getDownActivationTime);

            //restore normal constraints
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            //get up motion
            transform.rotation *= Quaternion.AngleAxis(-90, Vector3.right);

            //resume movement, get down cooldown
            isGettingDown = false;
            yield return new WaitForSeconds(getDownCooldownTime);

            //cooldown complete
            isGetDownReady = true;
        }
    }

    public void Unalive(bool shouldRagdoll, bool byAnvil)
    {
        AudioSource.PlayClipAtPoint(dyingSounds[Random.Range(0, dyingSounds.Length)], transform.position);
        if (shouldRagdoll)
        {
            this.EnableRagdoll();
        }
        else if (byAnvil)
        {
            // TODO: Fix this
            // animator.SetFloat("hasDiedByAnvil", 1f);
            // rb.useGravity = false;
            // this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            // animator.enabled = false;
            // animator.avatar = null;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EnableRagdoll()
    {
        isRagdolled = true;
        rb.useGravity = false;
        this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        this.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
        animator.enabled = false;
        animator.avatar = null;
        foreach (Rigidbody rb in RagdollRigidBodies)
        {
            rb.isKinematic = false;
        }
        foreach (Collider c in RagdollSegments)
        {
            c.isTrigger = false;
            c.attachedRigidbody.velocity = Vector3.zero;
        }
    }

    void moveAgent()
    {
        /*while (isGettingDown)
        {
            yield return null;
        }*/
        float distance = Vector3.Distance(transform.position, targetpos);
        Debug.DrawLine(transform.position, targetpos + new Vector3(0, 1, 0), Color.white, 100f, false);

        Vector3 angle = transform.position - president.transform.position;


        if ((transform.position.y < 1.5f) & !isGettingDown & !isRagdolled)
        {
            Vector3 movedir = Vector3.Normalize(targetpos - transform.position) * 30;
            if (distance < 12)
            {
                movedir = Vector3.Normalize(targetpos - transform.position) * 17;
            }
            rb.AddForce(-rb.velocity);
            rb.AddForce(movedir);
            targetpos.y = transform.position.y;
            transform.LookAt(targetpos);
        }
        // Debug.Log("force added");

        //transform.LookAt(angle);
    }

    public void Push(Vector3 dir)
    {
        Rigidbody r = GetComponent<Rigidbody>();
        r.AddForce(dir);
    }

    public void Victory()
    {
        // Currently commented out due to broken
        // animator.SetFloat("hasWon", Random.Range(1f, 4f));
    }
}
