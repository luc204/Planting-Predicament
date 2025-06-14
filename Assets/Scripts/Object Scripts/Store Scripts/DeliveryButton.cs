using UnityEngine;
using UnityEngine.UI;

public class DeliveryButton : MonoBehaviour
{
    public ItemData itemToDeliver;
    public DeliveryButtonSpawner spawner;

    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClicked);
        }
    }

    void OnButtonClicked()
    {
        if (spawner != null && itemToDeliver != null)
        {
            spawner.SpawnDeliveryCar(itemToDeliver); 
        }
        else
        {
            Debug.LogWarning("Spawner or ItemData not assigned on " + gameObject.name);
        }
    }
}

