using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureUIManager : MonoBehaviour
{

    public GameObject uiMenu;
    public GameObject SofaUiMenu;
    public GameObject TableUiMenu;
    public GameObject BedUiMenu;
    public GameObject ChairUiMenu;
    public GameObject uiManagerUI;
    public GameObject hand;
    // Start is called before the first frame update
    void Start()
    {
       uiMenu.SetActive(false);
    }

    public void LeaveWindow()
    {
        uiMenu.SetActive(false);
        uiManagerUI.SetActive(true);
        
    }

    public void SofaScreen()
    {
        uiMenu.SetActive(false);
        SofaUiMenu.SetActive(true);
    }

    public void TableScreen()
    {
        uiMenu.SetActive(false);
        TableUiMenu.SetActive(true);
    }

    public void ChairScreen()
    {
        uiMenu.SetActive(false);
        ChairUiMenu.SetActive(true);
    }

    public void BedScreen()
    {
        uiMenu.SetActive(false);
        BedUiMenu.SetActive(true);
    }

    private void Update()
    {
        Vector3 offset = new Vector3(0, 0.5f, 0);
        transform.position = hand.transform.position + offset;
        transform.localRotation = hand.transform.localRotation * Quaternion.Euler(180, 0, 0) * Quaternion.Euler(0, 180, 0);
    }



}
