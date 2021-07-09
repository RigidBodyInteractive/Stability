using UnityEngine;
using UnityEngine.UI;


public class StructureHealthOld : MonoBehaviour
{
    GrabModeOld grabMode;
    Rigidbody m_Rigidbody;
    public int DOF;
    //public Color FIXEDColour = new Color();
    //public Color PINColour = new Color();
    //public Color ROLLERColour = new Color();
    //public Color FREEColour = new Color();

    //public GameObject psFixed;
    //public GameObject psPin;
    //public GameObject psRoller;



    Renderer rend;

    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        //ps = GetComponent<ParticleSystem>();
        grabMode = GetComponent<GrabModeOld>();
    }

    void Update()
    {
        if (DOF <= 0)
        {
            //rend.material.SetColor("_Color", FIXEDColour);
            //psFixed.gameObject.SetActive(true);
            //psPin.gameObject.SetActive(false);
            //psRoller.gameObject.SetActive(false);



            if (grabMode.isLocked)
            {
                //m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            }
        }

        if (DOF == 1)
        {
            //rend.material.SetColor("_Color", PINColour);
            //psFixed.gameObject.SetActive(false);
            //psPin.gameObject.SetActive(true);
            //psRoller.gameObject.SetActive(false);


            if (grabMode.isLocked)
            {
                m_Rigidbody.constraints = 
                  RigidbodyConstraints.FreezePositionX
                | RigidbodyConstraints.FreezePositionY
                | RigidbodyConstraints.FreezePositionZ
                | RigidbodyConstraints.FreezeRotationX 
                | RigidbodyConstraints.FreezeRotationZ;

            }
        }

        if (DOF == 2)
        {
            //rend.material.SetColor("_Color", ROLLERColour);
            //psFixed.gameObject.SetActive(false);
            //psPin.gameObject.SetActive(false);
            //psRoller.gameObject.SetActive(true);


            if (grabMode.isLocked)
            {

                m_Rigidbody.constraints = 
                RigidbodyConstraints.FreezeRotationX
                | RigidbodyConstraints.FreezeRotationY
                | RigidbodyConstraints.FreezeRotationZ;

            }
        }

        if (DOF >= 3)
        {
            //rend.material.SetColor("_Color", FREEColour);
            //psFixed.gameObject.SetActive(false);
            //psPin.gameObject.SetActive(false);
            //psRoller.gameObject.SetActive(false);

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