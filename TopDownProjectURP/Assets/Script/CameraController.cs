using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    CinemachineVirtualCamera virtualCamera;
    [SerializeField]float cameraRotationSpeed = 2f;
    Vector3 oldMousePosition;

    private void Start() 
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        if( Input.GetMouseButtonDown(1))
        {
            oldMousePosition = Input.mousePosition;
            return;
        }


        if (Input.GetMouseButton(1))
        {
            Vector3 currentMousePosition = Input.mousePosition;                
                               
            if ( currentMousePosition.x < oldMousePosition.x)
            {
                float x = virtualCamera.transform.eulerAngles.x;
                float y = virtualCamera.transform.eulerAngles.y;
                virtualCamera.transform.eulerAngles = new Vector3(x, y - cameraRotationSpeed);
            }

            if (currentMousePosition.x > oldMousePosition.x)
            {
                float x = virtualCamera.transform.eulerAngles.x;
                float y = virtualCamera.transform.eulerAngles.y;
                virtualCamera.transform.eulerAngles = new Vector3(x, y + cameraRotationSpeed);
            }

        }

    }
}
