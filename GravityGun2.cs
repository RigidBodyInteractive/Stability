using UnityEngine;
using System.Collections;

public class GravityGun2 : MonoBehaviour
{
    public int gunDamage = 1;
    public float fireRate = 0.25f;
    public float weaponRange = 50f;
    private float nextFire;

    private Camera fpsCam;
    private GameObject heldObject;
    public Transform grabber;

    public float grabDistance = 10;
    public float attractForce = 3f;
    public float rotationForce = 10000;
    public ForceMode forceMode;
    public float shootForce = 30f;


    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private AudioSource gunAudio;
    public AudioClip[] audioClip;

    public float lerpSpeed = 10f;
    public bool hasObject = false;



    void Start()
    {
        gunAudio = GetComponent<AudioSource>();
        fpsCam = GetComponentInParent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }


    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            RaycastHit hit;

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                float playerDistance = Vector3.Distance(hit.collider.transform.position, transform.position);

                if (hit.rigidbody != null && !hasObject)
                {
                    hit.rigidbody.AddForceAtPosition(-grabber.transform.forward * attractForce, hit.point, forceMode);

                    if (playerDistance <= grabDistance && hit.collider.tag != "Structure")
                    {
                        hasObject = true;
                        heldObject = hit.collider.gameObject;
                    }
                }
            }
        }


        if (Input.GetButton("Fire2") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            RaycastHit hit;

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                float playerDistance = Vector3.Distance(hit.collider.transform.position, transform.position);

                if (hit.rigidbody != null && !hasObject)
                {
                    hit.rigidbody.AddForceAtPosition(grabber.transform.forward * attractForce, hit.point, forceMode);
                }
            }
        }

        if (hasObject)
        {
            GrabMode1 grabMode = heldObject.GetComponent<GrabMode1>();

            if (grabMode)
            {
                grabMode.canStick = false;
            }

            heldObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            heldObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            heldObject.GetComponent<Rigidbody>().useGravity = false;
            heldObject.GetComponent<Rigidbody>().detectCollisions = true;
            heldObject.transform.position = Vector3.Lerp(heldObject.transform.position, grabber.position, lerpSpeed * Time.deltaTime);

            if (Input.GetButton("Q"))
            {
                heldObject.GetComponent<Rigidbody>().isKinematic = false;
                heldObject.GetComponent<Rigidbody>().AddForce(grabber.transform.forward * shootForce, forceMode);
                heldObject.transform.SetParent(null);
                heldObject.GetComponent<Rigidbody>().useGravity = true;
                heldObject.GetComponent<Rigidbody>().detectCollisions = true;
                hasObject = false;

                if (grabMode)
                {
                    grabMode.canStick = true;
                }
            }

            if (Input.GetButtonDown("E"))
            {
                hasObject = !hasObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = false;
                heldObject.transform.SetParent(null);
                heldObject.GetComponent<Rigidbody>().useGravity = true;
                heldObject.GetComponent<Rigidbody>().detectCollisions = true;
                hasObject = false;
            }

            if (Input.GetButton("Fire1"))
            {
                heldObject.transform.Rotate(new Vector3(0, 400, 0) * Time.deltaTime);
                //transform.rotation *= Quaternion.AngleAxis(-90, transform.forward);
            }

            if (Input.GetButton("Fire2"))
            {
                heldObject.transform.Rotate(new Vector3(0, -400, 0) * Time.deltaTime);
            }

            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                Vector3 Ndirection = (heldObject.transform.position - grabber.transform.position);
                heldObject.transform.Translate(Ndirection * Input.GetAxis("Mouse ScrollWheel") * 10);
                //heldObject.transform.Rotate(new Vector3(400, 0, 0) * Time.deltaTime);
                //heldObject.transform.Rotate(Vector3.left * 0.5f, Space.Self);
                //heldObject.transform.position += (Vector3.forward * Input.GetAxis("Mouse ScrollWheel") * 2) - heldObject.transform.position;
            }
            /*if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                heldObject.transform.Rotate(new Vector3(-400, 0, 0) * Time.deltaTime);
            }*/
        }
        else
        {
            return;

        }
    }



}