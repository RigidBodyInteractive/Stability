using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class StructureHealth : MonoBehaviour
{
    GrabMode grabMode;
    GravityGun gravityGun;
    Rigidbody m_Rigidbody;
    public int DOF;
    public Color FIXEDColour = new Color();
    public Color PINColour = new Color();
    public Color ROLLERColour = new Color();
    public Color FREEColour = new Color();

    //public GameObject psFixed;
    //public GameObject psPin;
    //public GameObject psRoller;
    Renderer rend;


    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        //ps = GetComponent<ParticleSystem>();
        grabMode = GetComponent<GrabMode>();
        gravityGun = GetComponent<GravityGun>();
    }


    void Update()
    {
        if (grabMode.isLocked)
        {

            if (DOF <= 0)
            {
                m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;

            }

            if (DOF == 1)
            {
                m_Rigidbody.constraints =
        RigidbodyConstraints.FreezePositionX |
        RigidbodyConstraints.FreezePositionY |
        RigidbodyConstraints.FreezePositionZ;
                //m_Rigidbody.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }

            if (DOF == 2)
            {
                m_Rigidbody.constraints =
        RigidbodyConstraints.FreezeRotationX |
        RigidbodyConstraints.FreezeRotationY |
        RigidbodyConstraints.FreezeRotationZ;
                //m_Rigidbody.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            }



        }
        else
        {
            if (DOF <= 0)
            {
                rend.material.SetColor("_Color", FIXEDColour);
            }
            if (DOF == 1)
            {
                rend.material.SetColor("_Color", PINColour);
            }
            if (DOF == 2)
            {
                rend.material.SetColor("_Color", ROLLERColour);
            }
            if (DOF >= 3)
            {
                rend.material.SetColor("_Color", FREEColour);
            }

            m_Rigidbody.constraints = RigidbodyConstraints.None;
        }
    }



    public void addDegreeOfFreedom(int amount, Vector3 hitPoint)
    {
       // enemyAudio.Play();

        if (DOF == 0 | DOF == 1 | DOF == 2)
            DOF += amount;

        else
            return;
    }

    
    public void deleteDegreeOfFreedom(int amount, Vector3 hitPoint)
    {
       // enemyAudio.Play();

        if (DOF == 3 | DOF == 2 | DOF == 1)
            DOF -= amount;

        else
            return;
    }
    
}