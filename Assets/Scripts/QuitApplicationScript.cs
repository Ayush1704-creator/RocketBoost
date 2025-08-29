using UnityEngine;
using UnityEngine.InputSystem;

public class QuitApplicationScript : MonoBehaviour
{
    void Update()
    {
        if(Keyboard.current.escapeKey.isPressed)
        {
            Application.Quit();
            Debug.Log("Quitting Application.......");
        }
    }
}
