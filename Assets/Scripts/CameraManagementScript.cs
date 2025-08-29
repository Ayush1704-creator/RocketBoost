using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManagementScript : MonoBehaviour
{
    [SerializeField] CinemachineCamera cam1;
    [SerializeField] CinemachineCamera cam2;

    private bool isCam1Active = true;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchCamera();
        }
        
    }

    private void SwitchCamera()
    {
        if (isCam1Active)
        {
            cam1.Priority = 0;
            cam2.Priority = 10;
        }
        else
        {
            cam1.Priority = 10;
            cam2.Priority = 0;
        }
        isCam1Active = !isCam1Active;

        Debug.Log("Camera Switched");
    }
}   
