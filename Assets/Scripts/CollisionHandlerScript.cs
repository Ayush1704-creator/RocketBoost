using System;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class CollisionHandlerScript : MonoBehaviour
{
    [SerializeField] float startSequenceDelay = 2f;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip successSFX;
    [SerializeField] AudioClip fuelSFX;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    MovementScript movementScript;

    
    AudioSource audioSource;

    bool isControllable = true;
    bool isCollidable = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        movementScript = GetComponent<MovementScript>();
    }

    void Update()
    {
        RespondToDebugKey();
    }

    void RespondToDebugKey()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadNextLevel();
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isControllable || !isCollidable) { return; }
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Don't worry , that's a friendly object!");
                break;

            case "Finish":
                StartFinishSequence();
                Debug.Log("Congratulations! You won!");
                break;

            default:
                StartCrashSequence();
                Debug.Log("You hit an object");
                break;
        }

    }

    private void StartFinishSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(successSFX);
        successParticles.Play();
        GetComponent<MovementScript>().enabled = false;
        Invoke("LoadNextLevel", startSequenceDelay);
    }

    private void StartCrashSequence()
    {
        isControllable = false;
        audioSource.Stop();
        crashParticles.Play();
        audioSource.PlayOneShot(crashSFX);
        GetComponent<MovementScript>().enabled = false;
        Invoke("ReloadLevel", startSequenceDelay);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
            Debug.Log("All levels completed! Restarting from the beginning.");
        }
        SceneManager.LoadScene(nextSceneIndex);

    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }


    void OnTriggerEnter(Collider other)
    {
        if (!isControllable) { return; }
        else if (other.gameObject.tag == "Fuel")
        {
            audioSource.Stop();
            audioSource.PlayOneShot(fuelSFX);
            Destroy(other.gameObject);
            Debug.Log("Fuel collected!");
            movementScript.thrustStrength *= 2f;

        }
    }
    
}
