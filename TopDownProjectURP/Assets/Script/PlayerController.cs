using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Animations.Rigging;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera virtualCamera;

    float sensitivity = 25;
    float currentZoom;
    float zoom;
    float velocity = 0;
    float minZoom = 4;
    float maxZoom = 10;

    void Update()
    {
        HandleZoom();

    }

    void HandleZoom()
    {
        virtualCamera.m_Lens.OrthographicSize = Mathf.Clamp(virtualCamera.m_Lens.OrthographicSize,minZoom,maxZoom);
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            currentZoom = virtualCamera.m_Lens.OrthographicSize;
            zoom = Mathf.SmoothDamp(currentZoom, currentZoom + Input.GetAxis("Mouse ScrollWheel") * sensitivity, ref velocity , 0.05f);
            virtualCamera.m_Lens.OrthographicSize = zoom;
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            currentZoom = virtualCamera.m_Lens.OrthographicSize;
            zoom = Mathf.SmoothDamp(currentZoom, currentZoom + Input.GetAxis("Mouse ScrollWheel") * sensitivity, ref velocity , 0.05f);
            virtualCamera.m_Lens.OrthographicSize = zoom;
        }
    }

    void HandleHandIK()
    {


    }
}
