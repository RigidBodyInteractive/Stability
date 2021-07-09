using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class GrabMode : MonoBehaviour
{
    GameObject player;
    GravityGun gravityGun;
    StructureHealth structureHealth;

    private Renderer rend;
    public Material[] material;

    public bool canGrab = false;
    public bool isTouched = false;
    public bool isLocked = false;
    public bool canStick = false;
    public Text boolText;


    void Start()
    {
        structureHealth = GetComponent<StructureHealth>();
        player = GameObject.FindGameObjectWithTag("Player");
        gravityGun = player.GetComponentInChildren<GravityGun>();
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
    }

    void OnMouseOver()
    {
        float playerDistance = Vector3.Distance(transform.position, gravityGun.grabber.transform.position);
        Debug.Log(playerDistance.ToString());

        if (playerDistance <= gravityGun.grabDistance)// && !isLocked) //&& structureHealth.DOF == 3)
        {
            canGrab = true;
            rend.sharedMaterial = material[1];
        }

        else
        {
            canGrab = false;
            rend.sharedMaterial = material[0];
        }
    }

    void OnMouseExit()
    {
        canGrab = false;
        //isGrabbed = false;
        rend.sharedMaterial = material[0];
    }

    void Update()
    {
        boolText.text = "Stick" + canStick.ToString(); //+ "\n" + "Locked" + isLocked.ToString() ;
        float playerDistance = Vector3.Distance(transform.position, gravityGun.grabber.transform.position);

        if (playerDistance >= gravityGun.grabDistance)
        {
            //isGrabbed = false;
        }

        if (canGrab && Input.GetButtonDown("E") && !isLocked)
        {
            //isGrabbed = !isGrabbed;
            //transform.rotation = gunDOF.Emitter.transform.rotation;
        }

        if (gravityGun)
        {
            if (isTouched)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                //isGrabbed = false;
                isTouched = false;
            }
        }
        if (gravityGun.hasObject)
        {
            canStick = true;

            if (Input.GetButton("E")) //|| Input.GetButton("Q"))
            {
                canStick = false;

            }
        }

    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Structure" && canStick && !isLocked && !gravityGun.hasObject && structureHealth.DOF < 3)
        {
           collision.gameObject.AddComponent<Rigidbody>();

            Rigidbody cubeRB = GetComponent<Rigidbody>();
            cubeRB.velocity = Vector3.zero;
            cubeRB.angularVelocity = Vector3.zero;

            Vector3 Location = collision.contacts[0].point;
            Vector3 Rotation = collision.contacts[0].normal;
            isLocked = true;

            transform.position = Location;
            //transform.position = collision.transform.position;
            //transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y, collision.transform.position.z - 0.1f);

            //transform.rotation = collision.transform.rotation;
            //transform.rotation = Quaternion.FromToRotation(Vector3.forward, Rotation);
            transform.rotation = Quaternion.LookRotation(Rotation, Vector3.up);
            collision.transform.SetParent(this.transform);

            canStick = false;

        }

        if (collision.gameObject.tag == "Block")// && structureHealth.DOF == 3)
        {
            isLocked = false;
            collision.transform.DetachChildren();
        }
    }
}