using UnityEngine;
using UnityEngine.InputSystem;
public class MovementScript : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] public float thrustStrength = 10f;
    [SerializeField] float rotationStrength = 10f;

    [SerializeField] AudioClip thrustSound;

    [SerializeField] ParticleSystem mainThrustersParticles;
    [SerializeField] ParticleSystem leftThrustersParticles;
    [SerializeField] ParticleSystem rightThrustersParticles;

    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();

    }

    void FixedUpdate()
    {
        ThrustProcess();
        RotationProcess(); 
    }

    private void ThrustProcess()
    {
        if (thrust.IsPressed())
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
        rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrustSound);
        }
        if (!mainThrustersParticles.isPlaying)
        {
            mainThrustersParticles.Play();
        }
    }
    private void StopThrusting()
    {
        mainThrustersParticles.Stop();
        audioSource.Stop();
    }

    private void RotationProcess()
    {
        float rotationInput = rotation.ReadValue<float>();
        if (rotationInput < 0)
        {
            StartRotatingLeft();
        }
        else if (rotationInput > 0)
        {
            StartRotatingRight();
        }
        else
        {
            StopRotating();
        }
    }

    private void StartRotatingLeft()
    {
        if (!rightThrustersParticles.isPlaying)
        {
            leftThrustersParticles.Stop();
            rightThrustersParticles.Play();
        }
        ApplyRotation(rotationStrength);
    }
    private void StartRotatingRight()
    {
        if (!leftThrustersParticles.isPlaying)
        {
            rightThrustersParticles.Stop();
            leftThrustersParticles.Play();
        }
        ApplyRotation(-rotationStrength);
    }
    private void StopRotating()
    {
        rightThrustersParticles.Stop();
        leftThrustersParticles.Stop();
    }
    private void ApplyRotation(float rotationValueAtTheFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.fixedDeltaTime * rotationValueAtTheFrame);
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
    }
}
