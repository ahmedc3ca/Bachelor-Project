using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagesUIManager : MonoBehaviour
{
    public GameObject[] textFields = new GameObject[8];
    public GameObject furniture;

    public GameObject uiMenu;
    public GameObject commentuiMenu;
    public static bool uiIsShown;
    // Start is called before the first frame update
    void Start()
    {
        furniture = null;
        uiIsShown = false;
        uiMenu.SetActive(false);
    }

    private void Update()
    {
        if(furniture != null)
        {
            FurnitureCommentManager fr = furniture.GetComponent<FurnitureCommentManager>();
            for (int i = 0; i < Mathf.Min(fr.comments.Count, 8); i++)
            {
                textFields[i].GetComponent<Text>().text = (string)fr.comments[i];
            }
        }

    }

    public void LeaveWindow()
    {
        uiMenu.SetActive(false);
        GameObject.Find("PlayerSkin(Clone)").GetComponent<PlayerMover>().gameIsPaused = false;
    }

    public void GoBack()
    {
        uiMenu.SetActive(false);
        commentuiMenu.SetActive(true);
    }

}
