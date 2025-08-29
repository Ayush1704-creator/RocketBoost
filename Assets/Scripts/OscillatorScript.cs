using UnityEngine;

public class OscillatorScript : MonoBehaviour
{
    Vector3 startPosition;
    Vector3 endPosition;
    float movementFactor;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float movementSpeed = 1f;
    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + movementVector;
    }

    void Update()
    {
        movementFactor = Mathf.PingPong(Time.time * movementSpeed, 1);
        transform.position = Vector3.Lerp(startPosition, endPosition, movementFactor);
    }
}
