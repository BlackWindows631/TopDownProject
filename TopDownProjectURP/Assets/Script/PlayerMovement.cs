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

    float stamina;
    float maxStamina = 100;
    public bool canRun = true;

    private void Awake() 
    {
        stamina = maxStamina;
    }

    void Update()
    {
        HandleMovement();
        RotateTorwardMouse();   
    }

    private void HandleMovement()
    {
        Debug.Log(stamina);
        Debug.Log(canRun);

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if(stamina <= 0)
        {
            canRun = false;
        }

        if(Input.GetKey(KeyCode.LeftShift) && canRun)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 7f * verticalInput);
            transform.Translate(Vector3.right * Time.deltaTime * 7f * horizontalInput);
            stamina -= Time.deltaTime * 10;
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 3.5f * verticalInput);
            transform.Translate(Vector3.right * Time.deltaTime * 3.5f * horizontalInput);
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
