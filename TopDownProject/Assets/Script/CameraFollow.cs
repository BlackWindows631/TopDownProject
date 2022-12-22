using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    float smoothTime = 0.25f;
    Vector3 velocity = Vector3.zero;

    float minFov = 45f;
    float maxFov = 80f;
    float sensitivity = 17f;

    void Update()
    {
        Vector3 goalPos = target.position;
        goalPos.y = transform.position.y;
        goalPos.z = goalPos.z - 7;
        transform.position = Vector3.SmoothDamp(transform.position, goalPos, ref velocity, smoothTime);


        // Zoom
        float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * -sensitivity;
        fov = Mathf.Clamp(fov, minFov,maxFov);
        Camera.main.fieldOfView = fov;
    }
}
