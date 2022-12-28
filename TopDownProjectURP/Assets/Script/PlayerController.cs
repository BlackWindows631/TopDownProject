using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera virtualCamera;

    public Slider waterSlider;
    public Slider hungerSlider;

    float sensitivity = 25;
    float currentZoom;
    float zoom;
    float velocity = 0;
    float minZoom = 4;
    float maxZoom = 10;
    float maxFood = 100;
    float maxWater = 100;
    float water;
    float food;

    private void Awake() 
    {
        food = maxFood;
        water = maxWater;
        hungerSlider.maxValue = maxFood;
        waterSlider.maxValue = maxWater;
    }

    void Update()
    {
        HandleZoom();
        HandleFood();
        HandleWater();
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

    void HandleFood()
    {
        food -= Time.deltaTime;
        hungerSlider.value = food;
    }

    void HandleWater()
    {
        water -= Time.deltaTime;
        waterSlider.value = water;
    }
}
