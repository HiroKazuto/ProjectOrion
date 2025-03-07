using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustForce = 100;
    [SerializeField] float rotateForce = 100;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainThrusterParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;
    Rigidbody rb;
    AudioSource  audioSource;
    BoxCollider boxCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider>();
    }

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

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * Time.deltaTime * thrustForce);
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            if(!mainThrusterParticles.isPlaying)
            {
                mainThrusterParticles.Play();
            }
    }
    private void StopThrusting()
    {
        audioSource.Stop();
        mainThrusterParticles.Stop();
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if( Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopParticleEffects();
        }
    }
    private void RotateLeft()
    {
        ApplyRotation(rotateForce);
            if(!rightThrusterParticles.isPlaying)
            {
                rightThrusterParticles.Play();
            }
    }
    private void RotateRight()
    {
        ApplyRotation(-rotateForce);
        if(!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }
    }
    private void StopParticleEffects()
    {
        rightThrusterParticles.Stop();
        leftThrusterParticles.Stop();
    }

    void ApplyRotation(float rotateThisForce)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.deltaTime * rotateThisForce);
        rb.freezeRotation = false;

    }
}
