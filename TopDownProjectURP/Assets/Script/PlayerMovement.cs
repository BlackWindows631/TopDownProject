using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 mousePosition;
    public int speed = 5;
    private float horizontalInput;
    private float verticalInput;
    private float time;
    public Camera cameraPlayer;
    public LayerMask layerMask;

    float stamina;
    float maxStamina = 100;
    public bool canRun = true;
    bool isUsingStamina = false;
    bool startReset = false;

    [Header("Graphics")]
    public Slider slider;

    private void Awake() 
    {
        stamina = maxStamina;
        slider.maxValue = maxStamina;
    }

    void Update()
    {
        HandleMovement();
        RotateTorwardMouse();   
        slider.value = stamina;

        if(stamina >= maxStamina)
        {
            startReset = false;
            canRun = true;
        }

        if(startReset)
        {
            stamina += Time.deltaTime * 10;
        }

        if(stamina < maxStamina && !isUsingStamina)
        {
            time += Time.deltaTime;
            if(time > 3)
            {
                stamina += Time.deltaTime * 10;
            }
        }
    }
    private void HandleMovement()
    {

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if(stamina <= 0)
        {
            canRun = false;
            ResetStamina();
        }

        if(Input.GetKey(KeyCode.LeftShift) && canRun)
        {
            stamina -= Time.deltaTime * 10;
            time = 0;
            isUsingStamina = true;
        }
        else
        {
            isUsingStamina = false;
        }
    }
    private void RotateTorwardMouse()
    {
        mousePosition = Input.mousePosition;
        Ray ray = cameraPlayer.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray,out RaycastHit hitInfo, float.MaxValue, 1 << LayerMask.NameToLayer("Mousable")))
        {
            var target = hitInfo.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }
    }
    private void ResetStamina()
    {
        canRun = false;
        startReset = true;
        isUsingStamina = false;
    }
}
