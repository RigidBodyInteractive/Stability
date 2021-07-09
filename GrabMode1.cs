using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class GrabMode1 : MonoBehaviour
{
    public int DegOfFreedom;

    GameObject player;
    GravityGun2 gravityGun;
    StructureHealth structureHealth;

    private Renderer rend;
    public Material[] material;

    public bool canGrab = false;
    public bool isTouched = false;
    //public bool isLocked = false;
    public bool canStick = false;

    public Color FIXEDColour = new Color();
    public Color PINColour = new Color();
    public Color ROLLERColour = new Color();
    public Color FREEColour = new Color();

    public Transform SpawnPoint;





    void Start()
    {
        structureHealth = GetComponent<StructureHealth>();
        player = GameObject.FindGameObjectWithTag("Player");
        gravityGun = player.GetComponentInChildren<GravityGun2>();
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        //rend.sharedMaterial = material[0];
        rend.material.EnableKeyword("_EMISSION");

        if (DegOfFreedom <= 0)
        {
            //rend.material.SetColor("_Color", FIXEDColour);
            rend.material.SetColor("_EmissionColor", FIXEDColour);

            //rend.sharedMaterial = material[0];

        }
        if (DegOfFreedom == 1)
        {
            //rend.material.SetColor("_Color", PINColour);
            rend.material.SetColor("_EmissionColor", PINColour);

            //rend.sharedMaterial = material[1];

        }
        if (DegOfFreedom == 2)
        {
            //rend.material.SetColor("_Color", ROLLERColour);
            rend.material.SetColor("_EmissionColor", ROLLERColour);

            //rend.sharedMaterial = material[2];

        }
        if (DegOfFreedom >= 3)
        {
            //rend.material.SetColor("_Color", FREEColour);
            rend.material.SetColor("_EmissionColor", FREEColour);

            //rend.sharedMaterial = material[3];

        }
    }

    void OnMouseOver()
    {

        float playerDistance = Vector3.Distance(transform.position, gravityGun.grabber.transform.position);

        Debug.Log(playerDistance.ToString());

        if (playerDistance <= gravityGun.grabDistance)// && !isLocked) //&& structureHealth.DOF == 3)
        {
            canGrab = true;
            //rend.sharedMaterial = material[1];
        }

        else
        {
            canGrab = false;
            //rend.sharedMaterial = material[0];
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
        float playerDistance = Vector3.Distance(transform.position, gravityGun.grabber.transform.position);

        if (playerDistance >= gravityGun.grabDistance)
        {
            //isGrabbed = false;
        }

        if (gravityGun.hasObject)
        {
            canStick = false;
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
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Structure" && canStick) //&& !isLocked && !gravityGun.hasObject && structureHealth.DOF < 3)
        {
            if (!collision.rigidbody)
            {
                Rigidbody collRigidbody = collision.gameObject.AddComponent<Rigidbody>();
                collRigidbody.angularDrag = 10;

                Renderer colRend = collision.gameObject.GetComponent<Renderer>();

                if (DegOfFreedom == 1)
                {
                    collRigidbody.constraints = RigidbodyConstraints.FreezePosition;
                    colRend.material.SetColor("_Color", PINColour);
                }

                if (DegOfFreedom == 2)
                {
                    collRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                    colRend.material.SetColor("_Color", ROLLERColour);
                }

                GameObject TempBullet;
                TempBullet = Instantiate(gameObject, SpawnPoint.transform.position, SpawnPoint.transform.rotation) as GameObject;
                Destroy(gameObject);
            }

            if (collision.rigidbody && DegOfFreedom == 3)
            {
                Destroy(collision.gameObject.GetComponent<Rigidbody>());
                Destroy(gameObject);
            }
        }
    }
}
