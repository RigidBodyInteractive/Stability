using UnityEngine;
using System.Collections;

public class GravityGun : MonoBehaviour
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

    //public LayerMask layerMask = -1;
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
            StartCoroutine(ShotEffect());
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            RaycastHit hit;

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                GrabMode grabMode = hit.collider.GetComponent<GrabMode>();
                StructureHealth structureHealth = hit.collider.GetComponent<StructureHealth>();

                float playerDistance = Vector3.Distance(hit.collider.transform.position, transform.position);

                if (hit.rigidbody != null && !hasObject)
                {
                    hit.rigidbody.AddForce(-grabber.transform.up * attractForce, forceMode);

                    if (playerDistance <= grabDistance)
                    {
                        if (!grabMode || !grabMode.isLocked)
                        {
                            hasObject = true;
                            heldObject = hit.collider.gameObject;

                        }
                    }
                }
                if (grabMode != null && !hasObject && grabMode.isLocked)
                {
                    if (structureHealth.DOF == 1)
                    {
                        hit.rigidbody.AddRelativeTorque(0, rotationForce, 0, ForceMode.Impulse);
                        //hit.transform.Rotate(new Vector3(0, 400, 0) * Time.deltaTime);
                        //hit.rigidbody.AddTorque(0, rotationForce, 0);


                    }
                    if (structureHealth.DOF == 2)
                    {
                        hit.rigidbody.AddForce(-grabber.transform.up * attractForce, ForceMode.Acceleration);
                        //hit.rigidbody.AddForce(-hit.normal * attractForce);
                        //hit.transform.localPosition += Vector3.forward  * 2;


                    }
                }
            }
        }


        if (Input.GetButton("Fire2") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            StartCoroutine(ShotEffect());
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            RaycastHit hit;

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                GrabMode grabMode = hit.collider.GetComponent<GrabMode>();
                StructureHealth structureHealth = hit.collider.GetComponent<StructureHealth>();

                float playerDistance = Vector3.Distance(hit.collider.transform.position, transform.position);

                if (hit.rigidbody != null && !hasObject)
                {
                    hit.rigidbody.AddForce(grabber.transform.up * attractForce, forceMode);
                }
                if (grabMode != null && !hasObject && grabMode.isLocked)
                {
                    if (structureHealth.DOF == 1)
                    {
                        hit.rigidbody.AddRelativeTorque(0, -rotationForce, 0, ForceMode.Impulse);
                        //hit.transform.Rotate(new Vector3(0, 400, 0) * Time.deltaTime);

                    }
                    if (structureHealth.DOF == 2)
                    {
                        hit.rigidbody.AddForce(grabber.transform.up * attractForce, ForceMode.Acceleration);
                        //hit.rigidbody.AddForce(-hit.normal * attractForce, forceMode);

                    }
                }
            }
        }

        if (hasObject)
        {
            heldObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            heldObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            heldObject.GetComponent<Rigidbody>().useGravity = false;
            heldObject.GetComponent<Rigidbody>().detectCollisions = true;
            heldObject.transform.position = Vector3.Lerp(heldObject.transform.position, grabber.position, lerpSpeed * Time.deltaTime);

            if (Input.GetButton("Q"))
            {
                heldObject.GetComponent<Rigidbody>().isKinematic = false;
                heldObject.GetComponent<Rigidbody>().AddForce(grabber.transform.up * shootForce, forceMode);
                heldObject.transform.SetParent(null);
                heldObject.GetComponent<Rigidbody>().useGravity = true;
                heldObject.GetComponent<Rigidbody>().detectCollisions = true;
                hasObject = false;
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
                //heldObject.transform.Rotate(new Vector3(400, 0, 0) * Time.deltaTime);
                //heldObject.transform.Rotate(Vector3.left * 0.5f, Space.Self);
                heldObject.transform.localPosition += Vector3.up * Input.GetAxis("Mouse ScrollWheel") * 2;
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




    private IEnumerator ShotEffect()
    {
        // Play the shooting sound effect
        gunAudio.Play();

        if (hasObject)
        {
            gunAudio.clip = audioClip[1];

        }
        else
        {
            gunAudio.clip = audioClip[0];

        }


        // Turn on our line renderer

        //Wait for .07 seconds
        yield return shotDuration;

        // Deactivate our line renderer after waiting
    }
}