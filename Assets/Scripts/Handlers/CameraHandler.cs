using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour
{
    public CinemachineVirtualCamera vCam;

    private void Awake()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
        vCam.Follow = GameObject.Find("Player").transform;
        vCam.LookAt = GameObject.Find("Player").transform;
    }
    private void OnEnable()
    {
        PlayerController.OnRespawn += EnableCamera;
        PlayerController.OnDeath += DisableCamera;
    }
    private void OnDisable()
    {
        PlayerController.OnRespawn -= EnableCamera;
        PlayerController.OnDeath -= DisableCamera;
    }

    public void EnableCamera(PlayerController playerController)
    {
        vCam.enabled = true;
    }
    
    public void DisableCamera()
    {
        vCam.enabled = false;
    }
}
