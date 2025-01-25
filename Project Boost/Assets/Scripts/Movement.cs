using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource; 
    
    [SerializeField] float mainTrhust = 100f; 
    [SerializeField] float rotationTrhust= 0f; 
    [SerializeField] AudioClip mainEngine; 

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;


    bool isAlive ;

    // Start is called before the first frame update
    void Start()
    {
       rb =  GetComponent<Rigidbody>(); 
       audioSource = GetComponent<AudioSource>();
       Debug.Log("Datos "+rb.freezeRotation);
    }

    // Update is called once per frame
    void Update()
    {
       ProcessThrust(); 
       ProcessRotation(); 
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();

        }
        else
        {
            StopThrusting();
        }
    }

    

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainTrhust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {

            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }
    void ProcessRotation()
    {
         if(Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    

    private void RotateRight()
    {
        ApplyRotation(-rotationTrhust);
        if (!leftThrusterParticles.isPlaying) leftThrusterParticles.Play();
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationTrhust);
        if (!rightThrusterParticles.isPlaying) rightThrusterParticles.Play();
    }
    private void StopRotating()
    {
        rightThrusterParticles.Stop();
        leftThrusterParticles.Stop();
    }
    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation =  true; //freeze all rotation para rotar manual
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation =  false; // unfreeze rotation para que siga funcionando bien
    }
}
