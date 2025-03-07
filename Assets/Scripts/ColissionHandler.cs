using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColissionHandler : MonoBehaviour
{
    [SerializeField] float delayLevelLoad = 1f;
    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip successAudio;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;
    AudioSource  audioSource;

    bool isTransitioning = false;
    bool CollisionDisabled = false;

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
            Debug.Log("Loaded Next Scene");
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Collision Toggled");
            CollisionDisabled = !CollisionDisabled;
        }
        
    }
    void OnCollisionEnter(Collision other) {

        if(isTransitioning || CollisionDisabled){return;}

        switch(other.gameObject.tag) 
        {
            case "Friendly":
                Debug.Log("This is friendly.");
                break;

            case "Finish":
                StartSuccessSequence();
                break;
            
            default:
                StartCrashSequence();
                break;
        }
    }
    
    void StartCrashSequence()
    {
        isTransitioning = true;
        crashParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(crashAudio);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayLevelLoad);
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successAudio);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delayLevelLoad);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
