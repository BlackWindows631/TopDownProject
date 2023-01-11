using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponPickup : MonoBehaviour
{
    public TextMeshProUGUI textMeshH;
    WeaponInventory weaponInventory;
    WeaponIndex weaponIndex;
    Vector3 mousePosition;
    [SerializeField] GameObject playerTransform;
    public Camera cameraPlayer;
    public LayerMask layerMask;

    private void Awake() 
    {
        weaponInventory = GetComponentInChildren<WeaponInventory>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Primary") || other.gameObject.CompareTag("Secondary"))
        {
            Outline outline = other.gameObject.GetComponent<Outline>();
            outline.enabled = true;
        }

        if(other.gameObject.CompareTag("Pickable"))
        {
            Outline outline = other.gameObject.GetComponent<Outline>();
            outline.enabled = true;
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        mousePosition = Input.mousePosition;
        Ray ray = cameraPlayer.ScreenPointToRay(Input.mousePosition);
        
        if(other.gameObject.CompareTag("Primary") || other.gameObject.CompareTag("Secondary") || other.gameObject.CompareTag("Pickable"))
        {
            if(Physics.Raycast(ray,out RaycastHit hitInfo, float.MaxValue, ~layerMask))
            {
                if(hitInfo.collider.CompareTag("Primary") || hitInfo.collider.CompareTag("Secondary") || hitInfo.collider.CompareTag("Pickable"))
                {
                    textMeshH.text = "E to pick up " + hitInfo.collider.name;
                    if(Input.GetKey(KeyCode.E))
                    {
                        if(hitInfo.collider.CompareTag("Primary"))
                        {
                            weaponIndex = hitInfo.collider.gameObject.GetComponent<WeaponIndex>();
                            weaponInventory.SetupPrimaryWeapon(weaponIndex.weaponIndex);
                            textMeshH.text = "";
                            Destroy(hitInfo.collider.gameObject);
                        }
                        else if(hitInfo.collider.CompareTag("Secondary"))
                        {
                            weaponIndex = hitInfo.collider.gameObject.GetComponent<WeaponIndex>();
                            weaponInventory.SetupSecondaryWeapon(weaponIndex.weaponIndex);
                            textMeshH.text = "";
                            Destroy(hitInfo.collider.gameObject);
                        }
                        else if(hitInfo.collider.CompareTag("Pickable"))
                        {
                            textMeshH.text = "";
                            Destroy(hitInfo.collider.gameObject);
                        }
                    }
                }
                else
                {
                    textMeshH.text = "";
                }
            }
            else
            {
                textMeshH.text = "";
            }
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.CompareTag("Primary") || other.gameObject.CompareTag("Secondary"))
        {
            textMeshH.text = "";
            Outline outline = other.gameObject.GetComponent<Outline>();
            outline.enabled = false;
        }

        if(other.gameObject.CompareTag("Pickable"))
        {
            textMeshH.text = "";
            Outline outline = other.gameObject.GetComponent<Outline>();
            outline.enabled = false;
        }
    }
}
