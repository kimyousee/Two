using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Block : MonoBehaviour {

    public bool placed;
    private SpawnController spawnCtr;
    private GameController gameCtr;
    private float maxSpeed;
    private Rigidbody2D rb;
    public float gravitySpeed;
    private float origGravitySpeed;
    private bool colliding;

	void Awake () {
        colliding = false;
        placed = false;
        //spawnCtr = GameObject.FindGameObjectWithTag("SpawnController").GetComponent<SpawnController>();
        //gameCtr = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        spawnCtr = GameObject.Find("SpawnController").GetComponent<SpawnController>();
        gameCtr = GameObject.Find("GameController").GetComponent<GameController>();
        rb = GetComponent<Rigidbody2D>();
        gravitySpeed = gameCtr.gravitySpeed;
        origGravitySpeed = gameCtr.gravitySpeed;
    }
	
	// Update is called once per frame
	void Update () {

        Physics2D.gravity = new Vector2(0, gravitySpeed);
        if (!placed && gameCtr.gravity == "down")
        {
            Physics2D.gravity = new Vector2(0, -gravitySpeed);
        } else if (!placed && gameCtr.gravity == "right")
        {
            Physics2D.gravity = new Vector2(gravitySpeed,0);
            //rb.AddForce(-(transform.right * gravitySpeed));
        } else if (!placed && gameCtr.gravity == "left")
        {
            Physics2D.gravity = new Vector2(-gravitySpeed,0 );
        } else if (!placed && gameCtr.gravity == "up")
        {
            Physics2D.gravity = new Vector2(0, gravitySpeed);
        }
    }
    void FixedUpdate()
    {
        if (!placed)
        {
            float movement ;
            string gravity = gameCtr.gravity;
            if (gravity == "up" || gravity == "down")
            {
                // change speed if they hold down/up
                float speed = CrossPlatformInputManager.GetAxis("Vertical");
                if (speed != 0.0f)
                {
                    gravitySpeed += 0.5f;
                } else if (gravitySpeed > origGravitySpeed)
                {
                    gravitySpeed -= 1.0f;
                }
                rb.AddForce(-(transform.up * gravitySpeed));
                movement = CrossPlatformInputManager.GetAxis("Horizontal");
                if (movement != 0.0f && !colliding)
                {
                    float dir = movement < 0 ? -0.04f : 0.04f;
                    transform.position = new Vector3(transform.position.x + dir, transform.position.y, transform.position.z);
                }
                
            }
            else if (gravity == "right" || gravity == "left")
            {
                float speed = CrossPlatformInputManager.GetAxis("Horizontal");
                if (speed != 0.0f)
                {
                    gravitySpeed += 0.5f;
                }
                else if (gravitySpeed > origGravitySpeed)
                {
                    gravitySpeed -= 1.0f;
                }
                rb.AddForce(-(transform.right * gravitySpeed));
                movement = CrossPlatformInputManager.GetAxis("Vertical");
                if (movement != 0.0f && !colliding)
                {
                    float dir = movement < 0 ? -0.04f : 0.04f;
                    transform.position = new Vector3(transform.position.x, transform.position.y + dir, transform.position.z);
                 
                }
                
            }
        }
        colliding = false;
        
    }

    void OnCollisionEnter2D (Collision2D coll)
    {
        //Debug.Log("In block coll");
        //Debug.Log("Collided with" + coll.transform.name);
        if (placed) return;
        if (coll.gameObject.tag == "Border")
        {
            string borderType = coll.gameObject.transform.parent.gameObject.name;
            Debug.Log(borderType);
            if (borderType == "top" && gameCtr.gravity != "up") { return; }
            else if (borderType == "bott" && gameCtr.gravity != "down") { return; }
            else if (borderType == "left" && gameCtr.gravity != "left") { return; }
            else if (borderType == "right" && gameCtr.gravity != "right") { return; }
        }
        rb.isKinematic = true;
        gravitySpeed = origGravitySpeed;
        //rb.velocity = new Vector2(0f, 0f);
        //rb.angularVelocity = 0f;
        placed = true;
        spawnCtr.blockWasPlaced = true;

        

        //Collider[] colliders;
        //colliders = Physics.OverlapSphere(this.transform.position, 3.0f); // for finding multiple collisions
        // snap the block
        //foreach (Collider collider in colliders)
        //{
        //    if (collider.transform.GetComponent<Block>() == spawnCtr.current)
        //    {
        //        Vector3 colTrans = collider.transform.position;
        //        if (gameCtr.gravity == "vertical")
        //        {
        //            if (this.transform.position.x - colTrans.x <= 0.4)
        //            {
        //                collider.transform.position = new Vector3(this.transform.position.x, colTrans.y, colTrans.z);
        //            }
        //        }
        //        else if (gameCtr.gravity == "horizontal")
        //        {
        //            if (this.transform.position.y - colTrans.y <= 0.4)
        //            {
        //                collider.transform.position = new Vector3(colTrans.x, this.transform.position.y, colTrans.z);
        //            }
        //        }
        //collider.transform.GetComponent<Block>().placed = true;
        //break;
        //}
        //}
    }
    void OnCollisionStay2D(Collision2D coll)
    {
        colliding = true;
    }


}



