using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceSystem : MonoBehaviour
{
    public GameObject StoreInterface;
    public GameObject StorePrompt;

    private bool showInterface = false;

    void Start()
    {
        showInterface = false;
        StoreInterface.SetActive(false);
        StorePrompt.SetActive(false);
    }

    void Update()
    {
        if (showInterface && Input.GetKeyDown(KeyCode.E))
        {
            StoreInterface.SetActive(!StoreInterface.activeSelf);
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            showInterface = true;
            StorePrompt.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            showInterface = false;
            StorePrompt.SetActive(false);
            StoreInterface.SetActive(false); 
        }
    }
}

