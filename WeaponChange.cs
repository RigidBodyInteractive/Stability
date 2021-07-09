using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange : MonoBehaviour
{

    //public GameObject weapon00;
    public GameObject[] gun;
    //public GameObject weapon02;
    //public GameObject weapon03;

    void Start()
    {
        gun[0].SetActive(true);
        gun[1].SetActive(false);
        gun[2].SetActive(false);
    }

    void Update()
    {

        if (Input.GetKeyDown("`"))
        {
            gun[0].SetActive(true);
            gun[1].SetActive(false);
            gun[2].SetActive(false);
        }

        if (Input.GetKeyDown("1"))
        {
            gun[0].SetActive(false);
            gun[1].SetActive(true);
            gun[2].SetActive(false);

        }

        if (Input.GetKeyDown("2"))
        {
            gun[0].SetActive(false);
            gun[1].SetActive(false);
            gun[2].SetActive(true);

        }
    }
}