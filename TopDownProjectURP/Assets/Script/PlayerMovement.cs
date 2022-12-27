using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 mousePosition;
    public int speed = 5;
    private float horizontalInput;
    private float verticalInput;
    public Camera cameraPlayer;

    void Update()
    {
        HandleMovement();
        RotateTorwardMouse();   
    }

    private void HandleMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if(Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 7 * verticalInput);
            transform.Translate(Vector3.right * Time.deltaTime * 7 * horizontalInput);
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 4 * verticalInput);
            transform.Translate(Vector3.right * Time.deltaTime * 4 * horizontalInput);
        }

        
    }
    
    private void RotateTorwardMouse()
    {
        mousePosition = Input.mousePosition;
        Ray ray = cameraPlayer.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray,out RaycastHit hitInfo, maxDistance: 300f))
        {
            var target = hitInfo.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }
    }
}
