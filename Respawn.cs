using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{

    public Transform[] SpawnPoint;
    public GameObject[] Prefab;
    public GameObject[] Clone;
    

    private void OnTriggerEnter(Collider other)

    {
        if (other.tag == "Player")

        {
          SceneManager.LoadScene(2);
           

        }

        if (other.tag == "pillar1")
        {
            Destroy(other.gameObject);
            Clone[1] = Instantiate(Prefab[1], SpawnPoint[1].transform.position, SpawnPoint[1].rotation) as GameObject;
        }

        if (other.tag == "pillar2")
        {
            Destroy(other.gameObject);
            Clone[2] = Instantiate(Prefab[2], SpawnPoint[2].transform.position, SpawnPoint[2].rotation) as GameObject;
        }

        if (other.tag == "slab1")
        {
            Destroy(other.gameObject);
            Clone[3] = Instantiate(Prefab[3], SpawnPoint[3].transform.position, SpawnPoint[3].rotation) as GameObject;
        }

        if (other.tag == "slab2")
        {
            Destroy(other.gameObject);
            Clone[4] = Instantiate(Prefab[4], SpawnPoint[4].transform.position, SpawnPoint[4].rotation) as GameObject;
        }

        if (other.tag == "slab3")
        {
            Destroy(other.gameObject);
            Clone[5] = Instantiate(Prefab[5], SpawnPoint[5].transform.position, SpawnPoint[5].rotation) as GameObject;
        }

        if (other.tag == "qube1")
        {
            Destroy(other.gameObject);
            Clone[6] = Instantiate(Prefab[6], SpawnPoint[6].transform.position, SpawnPoint[6].rotation) as GameObject;
        }

        if (other.tag == "qube2")
        {
            Destroy(other.gameObject);
            Clone[7] = Instantiate(Prefab[7], SpawnPoint[7].transform.position, SpawnPoint[7].rotation) as GameObject;
        }

        if (other.tag == "qube3")
        {
            Destroy(other.gameObject);
            Clone[8] = Instantiate(Prefab[8], SpawnPoint[8].transform.position, SpawnPoint[8].rotation) as GameObject;
        }

        if (other.tag == "qube4")
        {
            Destroy(other.gameObject);
            Clone[9] = Instantiate(Prefab[9], SpawnPoint[9].transform.position, SpawnPoint[9].rotation) as GameObject;
        }

    }

}
