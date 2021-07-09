using UnityEngine;
using System.Collections;
using System;

public class GrabModeOld : MonoBehaviour
{
    GameObject player;
    GravityGunOld gravityGun;
    StructureHealthOld structureHealth;

    public Color GRABColor;
    private Renderer rend;
    public Material[] material;

    public bool canGrab = false;
    public bool isGrabbed = false;
    public bool isTouched = false;
    public bool isLocked = false;


    void Start()
    {
        structureHealth = GetComponent<StructureHealthOld>();
        player = GameObject.FindGameObjectWithTag("Player");
        gravityGun = player.GetComponentInChildren<GravityGunOld>();
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];

    }

    void OnMouseOver()
    {
        float playerDistance = Vector3.Distance(transform.position, gravityGun.Emitter.transform.position);
        //Debug.Log(playerDistance.ToString());

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
        float playerDistance = Vector3.Distance(transform.position, gravityGun.Emitter.transform.position);

        if (playerDistance >= gravityGun.grabDistance)
        {
            //isGrabbed = false;
        }

        if (canGrab && Input.GetButtonDown("E") && !isLocked)
        {
            isGrabbed = !isGrabbed;
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



        if (isLocked)
        {

            if (structureHealth.DOF == 3)
            {
                transform.DetachChildren();
                isLocked = false;
            }
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Structure" && !isLocked && !isGrabbed) // && structureHealth.DOF < 3) // && isGrabbed)
        {
            Rigidbody cubeRB = GetComponent<Rigidbody>();

            Vector3 Location = collision.contacts[0].point;
            Vector3 Rotation = collision.contacts[0].normal;


            //isGrabbed = false;
            isLocked = true;
            transform.position = collision.transform.position;
            //transform.position = Location;
            //transform.rotation = collision.transform.rotation;
            //transform.rotation = Quaternion.FromToRotation(Vector3.forward, Rotation);
            transform.rotation = Quaternion.LookRotation(Rotation, Vector3.up);

            collision.transform.SetParent(this.transform);
            cubeRB.velocity = Vector3.zero;
            cubeRB.angularVelocity = Vector3.zero;
            //cubeRB.centerOfMass = new Vector3(0, 0, 0.5f);

        }

        if (collision.gameObject.tag == "Structure" && !isLocked && structureHealth.DOF == 3)
        {
            GrabModeOld grabMode = GetComponent<GrabModeOld>();
            collision.transform.DetachChildren();

        }

    }
}