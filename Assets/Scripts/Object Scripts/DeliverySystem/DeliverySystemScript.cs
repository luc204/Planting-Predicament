using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeliverySystemScript : MonoBehaviour
{
    public PlayerStats playerStats;

    public bool PlayerInRange = false;
    public List<ItemData> itemsToDeliver;
    public bool RemoveItemsAfterDelivery = true;
    public ItemData newPlantItem;

    private AudioSource audioSource;
    public AudioClip pickupsound;
    public AudioClip trim;


    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }
    void PlayClip(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    void Update()
    {
        if (PlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Inventory2 inventory = FindObjectOfType<Inventory2>();
            PlayClip(pickupsound);
            if (inventory != null)
            {
                foreach (ItemData item in itemsToDeliver)
                {
                    inventory.AddItem(item);
                    Debug.Log($"Delivered: {item.itemName}");
                    
                }

                if (RemoveItemsAfterDelivery)
                {
                    itemsToDeliver.Clear(); // So it doesn’t repeat
                }
                
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("Inventory not found!");
            }
        }
       
    }
    public void AddToDelivery()
    {
        if (newPlantItem != null)
        {
            itemsToDeliver.Add(newPlantItem);
            Debug.Log($"{newPlantItem.name} added to delivery system.");
        }
        else
        {
            Debug.LogWarning("Missing reference to delivery system or item.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = false;
        }
    }
}


