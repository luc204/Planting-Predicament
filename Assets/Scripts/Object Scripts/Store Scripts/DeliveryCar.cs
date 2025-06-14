using UnityEngine;

public class DeliveryCar : MonoBehaviour
{
    public float speed = 5f;
    public float selfDestructTime = 10f;
    public GameObject deliveryBoxPrefab;
    public ItemData cargoItem;
    public bool hasDroppedBox = false;
    public bool CanDropBox = false;


    private void Start()
    {
        
        Invoke(nameof(SelfDestruct), selfDestructTime);
    }

    void Update()
    {
        
        transform.Translate(Vector3.right * speed * Time.deltaTime);
       
        if (CanDropBox && !hasDroppedBox)
        {
            DropBox();
            hasDroppedBox = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DropZone")) 
        {
            CanDropBox = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DropZone"))
        {
            CanDropBox = false;
            
        }
    }

    void DropBox()
    {
        GameObject box = Instantiate(deliveryBoxPrefab, transform.position, Quaternion.identity);
        DeliveryBox pickup = box.GetComponent<DeliveryBox>();
        if (pickup != null)
        {
            pickup.Init(cargoItem);
        }
    }

    void SelfDestruct()
    {
        Destroy(gameObject);
    }
}



