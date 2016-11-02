using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private Transform myTransform;
    public Rigidbody rb;

    public float walkSpeed;
    public float jumpForce;
    private float distToGround;
    private float distToWall;

    public GameObject playerModel;
    private int facingDirection = 1;
    private Quaternion facingRotation;

    public bool key = false;

    // Use this for initialization
    void Start ()
    {
        myTransform = this.transform;
        rb = GetComponent<Rigidbody>();

        distToGround = myTransform.GetComponent<Collider>().bounds.extents.y;
        distToWall = myTransform.GetComponent<Collider>().bounds.extents.x;
    }

    void FixedUpdate()
    {
        Controls();
    }

    // Update is called once per frame
    void Update ()
    {
        DirectionFacing();
    }

    void Controls()
    {
        //Move Right
        if (Input.GetKey("d"))
        {
            rb.MovePosition(transform.position + transform.forward * walkSpeed * Time.fixedDeltaTime);
            facingDirection = 1;
        }
        //Move Left
        else if (Input.GetKey("a"))
        {
            rb.MovePosition(transform.position + transform.forward * walkSpeed * Time.fixedDeltaTime);
            facingDirection = -1;
        }

        //Jumping
        if (Input.GetKeyDown("space") && CheckGrounded() == true)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void DirectionFacing()
    {
        facingRotation = playerModel.transform.rotation;

        facingRotation.eulerAngles = new Vector3(facingRotation.eulerAngles.x,
            90 * facingDirection,
                facingRotation.eulerAngles.z);

        playerModel.transform.rotation = facingRotation;
    }

    void OnTriggerEnter(Collider otherObject)
    {
        if (otherObject.tag == "Spike")
        {
            Destroy(this.gameObject);
        }

        if (otherObject.tag == "Key")
        {
            key = true;
            Destroy(otherObject.gameObject);
        }

        if (otherObject.tag == "Door")
        {
            if (key == false)
            {
                Debug.Log("Door is locked, Find the key to continue");
                otherObject.isTrigger = false;
            }

            else if (key == true)
            {
                otherObject.isTrigger = true;
            }
        }
    }

    void OnTriggerExit(Collider otherObject)
    {
        if (otherObject.tag == "Door")
        {
            key = false;
        }
    }

    public bool CheckGrounded()
    {
        return Physics.Raycast(myTransform.position, -Vector3.up, distToGround + 0.1f);
    }
}
