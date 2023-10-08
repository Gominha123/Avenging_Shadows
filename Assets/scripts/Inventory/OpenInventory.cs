using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OpenInventory : MonoBehaviour
{
    public GameObject scrollView;
    public bool isActive = false;

    private void Awake()
    {
        scrollView.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            CheckInventory();
        }
    }
    public void CheckInventory()
    {
        if (isActive)
        {
            scrollView.SetActive(false);
            Time.timeScale = 1;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
            isActive = false;
        }
        else
        {
            scrollView.SetActive(true);
            Time.timeScale = 0;
            UnityEngine.Cursor.lockState = CursorLockMode.Confined;
            UnityEngine.Cursor.visible = true;
            isActive = true;
        }
    }

    public void click()
    {
        Debug.Log("click");
    }
}
