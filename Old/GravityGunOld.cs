using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGunOld : MonoBehaviour
{
    private Rigidbody objectRB;
    private GameObject objectIHave;
    public Camera cam;
    public Transform Emitter;
    private Vector3 rotateVector = Vector3.one;
    // ME
    //GrabMode grabMode;

    public float grabDistance;
    public float attractSpeed;
    public float minThrowForce;
    public float maxThrowForce;
    public float throwForce;
    public bool grabbed = false;
    public float hitForce;


    private void Start()
    {
        throwForce = minThrowForce;

    }

    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        if (!grabbed)
        {
            if (Input.GetMouseButton(0))
            {
                ShootRayPlus();
            }
            if (Input.GetMouseButton(1))
            {
                ShootRayMinus();
            }
        }

        if (Input.GetMouseButton(1) && grabbed)
        {
            throwForce += 1f;
        }

        if (Input.GetMouseButtonUp(1) && grabbed)
        {
            ShootObj();
        }

        if (Input.GetKeyDown(KeyCode.E) && grabbed)
        {
            DropObj();
        }

        if (grabbed)
        {
            RotateObj();

            if (CheckDist() >= 1f)
            {
                MoveObjToPos();
            }
        }
    }

    private void CalculateRotVector()
    {
        float x = Random.Range(-0.5f, 0.5f);
        float y = Random.Range(-0.5f, 0.5f);
        float z = Random.Range(-0.5f, 0.5f);
        rotateVector = new Vector3(x, y, z);
    }

    private void RotateObj()
    {
        //objectIHave.transform.Rotate(rotateVector);
    }


    public float CheckDist()
    {
        float dist = Vector3.Distance(objectIHave.transform.position, Emitter.transform.position);
        return dist;
    }

    private void MoveObjToPos()
    {
        objectIHave.transform.position = Vector3.Lerp(objectIHave.transform.position, Emitter.position, attractSpeed * Time.deltaTime);
    }

    private void DropObj()
    {
        objectRB.constraints = RigidbodyConstraints.None;
        objectIHave.transform.parent = null;
        objectIHave = null;
        grabbed = false;

        //
        objectRB.useGravity = true;
        objectRB.detectCollisions = true;
        objectRB.isKinematic = false;
    }

    private void ShootObj()
    {
        throwForce = Mathf.Clamp(throwForce, minThrowForce, maxThrowForce);
        objectRB.AddForce(cam.transform.forward * throwForce, ForceMode.Impulse);
        throwForce = minThrowForce;
        DropObj();
    }

    private void ShootRayPlus()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, grabDistance))
        {
            GrabModeOld grabMode = hit.collider.GetComponent<GrabModeOld>();
            StructureHealthOld structureHealth = hit.collider.GetComponent<StructureHealthOld>();
            Debug.Log(hit.collider.name.ToString());


            if (grabMode != null && !grabMode.isLocked)
            {
                if (hit.collider.CompareTag("Block"))
                {
                    objectIHave = hit.collider.gameObject;
                    objectIHave.transform.SetParent(Emitter);
                    objectRB = objectIHave.GetComponent<Rigidbody>();
                    objectRB.constraints = RigidbodyConstraints.FreezeAll;
                    //
                    objectRB.velocity = Vector3.zero;
                    objectRB.angularVelocity = Vector3.zero;
                    objectRB.useGravity = false;
                    objectRB.detectCollisions = true;
                    grabbed = true;
                    //
                    //objectRB.isKinematic = true;

                    CalculateRotVector();

                }
            }

            if (grabMode != null && grabMode.isLocked)
            {
                if (structureHealth.DOF == 1)
                {
                    hit.rigidbody.AddRelativeTorque(Vector3.up * 100);
                }
                if (structureHealth.DOF == 2)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
            }
        }
    }

    private void ShootRayMinus()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, grabDistance))
        {
            GrabModeOld grabMode = hit.collider.GetComponent<GrabModeOld>();
            StructureHealthOld structureHealth = hit.collider.GetComponent<StructureHealthOld>();



            if (grabMode != null && grabMode.isLocked)
            {
                if (structureHealth.DOF == 1)
                {
                    hit.rigidbody.AddRelativeTorque(Vector3.up * -100);
                }
                if (structureHealth.DOF == 2)
                {
                    hit.rigidbody.AddForce(hit.normal * hitForce);
                }
            }
        }
    }
}