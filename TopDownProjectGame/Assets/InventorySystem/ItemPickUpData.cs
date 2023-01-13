using UnityEngine;

public class ItemPickUpData : MonoBehaviour
{
    public InventoryItemData itemData;

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            var inventory = other.gameObject.GetComponent<InventoryHolder>();

            if(!inventory) return;

            if(inventory.InventorySystem.AddToInventory(itemData,1))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
