using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OpenInventory : MonoBehaviour
{
    public GameObject scrollView;
    public GameObject descriptionView;
    public GameObject equipmentView;
    public bool isActive = false;

    public bool letDisable = true;

    private void Start()
    {
        scrollView.SetActive(false);
        descriptionView.SetActive(false);
        equipmentView.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(letDisable)
            {
                CheckInventory();
                Inventory.Instance.ListItems();
            }
        }
    }
    public void CheckInventory()
    {
        if (letDisable)
        {
            if (isActive)
            {
                scrollView.SetActive(false);
                descriptionView.SetActive(false);
                equipmentView.SetActive(false);
                Time.timeScale = 1;
                UnityEngine.Cursor.lockState = CursorLockMode.Locked;
                UnityEngine.Cursor.visible = false;
                isActive = false;
            }
            else
            {
                scrollView.SetActive(true);
                descriptionView.SetActive(true);
                equipmentView.SetActive(true);
                Time.timeScale = 0;
                UnityEngine.Cursor.lockState = CursorLockMode.Confined;
                UnityEngine.Cursor.visible = true;
                isActive = true;
            }
        }
    }
}
