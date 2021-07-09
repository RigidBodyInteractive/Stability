using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelFinish : MonoBehaviour
{
    int id;
    public static bool levelCleared;

    AudioSource audioSource;
    public AudioClip finishSound;

    private void Awake()
    {
        id = SceneManager.GetActiveScene().buildIndex;
        levelCleared = false;

        audioSource = GetComponent<AudioSource>();


    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player") //&& EnemyManager.enemyInGame <=0)
            
        {
            //PlayerPrefs.SetInt("level", id);          
            //PlayerPrefs.SetInt("PlayerNewLevel", EXPERIENCE.playerLevel);
            //PlayerPrefs.SetInt("CurrentExperience", EXPERIENCE.currentExp);
            //PlayerPrefs.SetInt("ExpereinceRemaining", EXPERIENCE.expRemaining);

            audioSource.clip = finishSound;
            audioSource.PlayOneShot(finishSound);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


            levelCleared = true;
        }

    }
}
