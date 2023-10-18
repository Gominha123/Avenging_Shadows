using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvDescription : MonoBehaviour
{
    //[SerializeField] private GameObject uiPanel;
    [SerializeField] private TextMeshProUGUI nameTextTMP;
    [SerializeField] private TextMeshProUGUI descriptionTextTMP;
    [SerializeField] private Button useButton;
    [SerializeField] private Button deleteButton;
    [SerializeField] private Button deleteButtonEquiped;

    public bool isDisplayed = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        Close();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetUp(string nameItem, string descriptionItem, bool interactable)
    {
        nameTextTMP.text = nameItem;
        descriptionTextTMP.text = descriptionItem;
        useButton.interactable = interactable;
        deleteButton.interactable = interactable;
        //uiPanel.SetActive(true);
        nameTextTMP.gameObject.SetActive(true);
        descriptionTextTMP.gameObject.SetActive(true);
        useButton.gameObject.SetActive(true);
        deleteButton.gameObject.SetActive(true);
        deleteButtonEquiped.gameObject.SetActive(false);
        isDisplayed = true;
    }

    public void Close()
    {
        //uiPanel.SetActive(false);
        nameTextTMP.gameObject.SetActive(false);
        descriptionTextTMP.gameObject.SetActive(false);
        useButton.gameObject.SetActive(false);
        deleteButton.gameObject.SetActive(false);
        deleteButtonEquiped.gameObject.SetActive(false);
        useButton.interactable = false;
        deleteButton.interactable = false;
        isDisplayed = false;
    }

    public void SetUpForEquiped(string nameItem, string descriptionItem)
    {
        nameTextTMP.text = nameItem;
        descriptionTextTMP.text = descriptionItem;
        useButton.interactable = false;
        deleteButton.interactable = false;
        //uiPanel.SetActive(true);
        nameTextTMP.gameObject.SetActive(true);
        descriptionTextTMP.gameObject.SetActive(true);
        useButton.gameObject.SetActive(false);
        deleteButton.gameObject.SetActive(false);
        deleteButtonEquiped.gameObject.SetActive(true);
        isDisplayed = true;
    }
}
