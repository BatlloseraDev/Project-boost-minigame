using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]float levelLoadDelay = 2f; 
    [SerializeField] AudioClip succes;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem succesParticles;
    [SerializeField] ParticleSystem crashParticles;


    AudioSource audioSource;

    bool isTrasitioning = false;
    bool collisionDisable = false;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update() 
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
       if(Input.GetKeyDown(KeyCode.L))
       {
        LoadNextLevel();
       }
       else if(Input.GetKeyDown(KeyCode.C))
       {
            collisionDisable = !collisionDisable;
            Debug.Log("Collision "+collisionDisable); 
       }
    }

    void OnCollisionEnter(Collision other) 
    {
        if(isTrasitioning ||collisionDisable) return; 
        
        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Esto es amigo");
            break;

            case "Fuel":
                Debug.Log("Esto es gasofa");
            break;

            case "Finish":
                StartSuccesSequence();
            break; 

            default:
                Debug.Log("Te has explotado");
                StartCrashSequence();
               
            break;

        }
    }

    void StartSuccesSequence()
    {
        isTrasitioning=true;
        succesParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(succes);
        
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }
    void StartCrashSequence()
    {
        isTrasitioning=true;
        crashParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex +1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0; 
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
