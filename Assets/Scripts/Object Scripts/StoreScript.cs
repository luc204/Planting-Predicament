using TMPro;
using UnityEngine;

public class StoreScript : MonoBehaviour
{
    private bool isPlayerPresent = false;
    public TextMeshProUGUI sellPromptText;

    void Start()
    {
        if (sellPromptText != null)
        {
            sellPromptText.gameObject.SetActive(false);
            sellPromptText.text = "Press E to sell items";// hide on start
        }
    }


    void Update()
    {
        // Sell items when player presses 'E' in store area
        if (isPlayerPresent && Input.GetKeyDown(KeyCode.E))
        {
            Inventory2 inventory = FindObjectOfType<Inventory2>();
            if (inventory != null)
            {
                inventory.SellAllItems(10); // 10 coins per item
            }
            else
            {
                Debug.LogWarning("Inventory not found!");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerPresent = true;
            if (sellPromptText != null)
                sellPromptText.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerPresent = false;
            if (sellPromptText != null)
                sellPromptText.gameObject.SetActive(false);
        }
    }
}
