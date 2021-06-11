using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CommentUIManager : MonoBehaviour
{
    public GameObject furniture;
    public GameObject commentuiMenu;
    public GameObject messagesuiMenu;
    public GameObject inputField;
    public static bool uiIsShown;
    public int nMessages;
    // Start is called before the first frame update
    void Start()
    {
        nMessages = 0;
        uiIsShown = false;
        commentuiMenu.SetActive(false);
    }

    private void Update()
    {
        if (uiIsShown)
        {
            nMessages = furniture.GetComponent<FurnitureCommentManager>().comments.Count;
            GameObject.Find("LeftCommentsText").GetComponent<Text>().text = "Comments (" + nMessages + ")";
        }
    }

    public void detected(GameObject fr)
    {
        furniture = fr;
        commentuiMenu.SetActive(true);
        uiIsShown = true;
        GameObject.Find("PlayerSkin(Clone)").GetComponent<PlayerMover>().gameIsPaused = true;
    }

    public void LeaveWindow()
    {
        uiIsShown = false;
        commentuiMenu.SetActive(false);
        GameObject.Find("PlayerSkin(Clone)").GetComponent<PlayerMover>().gameIsPaused = false;
    }

    public void OpenMessages()

    {
        GameObject.Find("MessagesCanvas").GetComponent<MessagesUIManager>().furniture = furniture;
        messagesuiMenu.SetActive(true);
        uiIsShown = false;
        commentuiMenu.SetActive(false);
    }

    public void SubmitMessage()
    {
        furniture.GetComponent<FurnitureCommentManager>().comments.Add(inputField.GetComponent<TMP_InputField>().text);
        inputField.GetComponent<TMP_InputField>().text = "";
        GameObject.Find("PlayerSkin(Clone)").GetComponent<PlayerMover>().gameIsPaused = false;
        uiIsShown = false;
        commentuiMenu.SetActive(false);
    }

    public void ToggleArrows()
    {
        if (furniture.GetComponent<FurnitureCommentManager>().showsArrows)
        {
            furniture.GetComponent<FurnitureCommentManager>().DestroyArrows();
            furniture.GetComponent<FurnitureCommentManager>().showsArrows = false;
        }
        else
        {
            furniture.GetComponent<FurnitureCommentManager>().CreateArrows();
            furniture.GetComponent<FurnitureCommentManager>().showsArrows = true;
        }
    }

    public void RotateFurniture()
    {
        furniture.GetComponent<FurnitureCommentManager>().Rotate();
    }

    public void MakeWhite()
    {

        for (int i = 0; i < furniture.transform.childCount; i++)
        {
            GameObject Go = furniture.transform.GetChild(i).gameObject;
            Renderer rend = Go.GetComponent<Renderer>();
            if(rend == null || Go.name == "notif")
            {
                return;
            }
            //rend.material.shader = Shader.Find("HDRenderPipeline/LitTessellation");
            rend.material.SetColor("_BaseColor", Color.white);
        }

    }

    public void MakeRed()
    {
        for (int i = 0; i < furniture.transform.childCount; i++)
        {
            GameObject Go = furniture.transform.GetChild(i).gameObject;
            Renderer rend = Go.GetComponent<Renderer>();
            if (rend == null || Go.name == "notif")
            {
                return;
            }
            //rend.material.shader = Shader.Find("HDRenderPipeline/LitTessellation");
            rend.material.SetColor("_BaseColor", Color.red);
        }
    }

    public void MakeYellow()
    {
        for (int i = 0; i < furniture.transform.childCount; i++)
        {
            GameObject Go = furniture.transform.GetChild(i).gameObject;
            Renderer rend = Go.GetComponent<Renderer>();
            if (rend == null || Go.name == "notif")
            {
                return;
            }
            //rend.material.shader = Shader.Find("HDRenderPipeline/LitTessellation");
            rend.material.SetColor("_BaseColor", Color.yellow);
        }
    }

    public void MakeGreen()
    {
        for (int i = 0; i < furniture.transform.childCount; i++)
        {
            GameObject Go = furniture.transform.GetChild(i).gameObject;
            Renderer rend = Go.GetComponent<Renderer>();
            if (rend == null || Go.name == "notif")
            {
                return;
            }
            //rend.material.shader = Shader.Find("HDRenderPipeline/LitTessellation");
            rend.material.SetColor("_BaseColor", Color.green);
        }
    }

    public void MakeBlue()
    {
        for (int i = 0; i < furniture.transform.childCount; i++)
        {
            GameObject Go = furniture.transform.GetChild(i).gameObject;
            Renderer rend = Go.GetComponent<Renderer>();
            if (rend == null || Go.name == "notif")
            {
                return;
            }
            //rend.material.shader = Shader.Find("HDRenderPipeline/LitTessellation");
            rend.material.SetColor("_BaseColor", Color.blue);
        }
    }

    public void MakeBlack()
    {
        for (int i = 0; i < furniture.transform.childCount; i++)
        {
            GameObject Go = furniture.transform.GetChild(i).gameObject;
            Renderer rend = Go.GetComponent<Renderer>();
            if (rend == null || Go.name == "notif")
            {
                return;
            }
            //rend.material.shader = Shader.Find("HDRenderPipeline/LitTessellation");
            rend.material.SetColor("_BaseColor", Color.black);
        }
    }

    public void MakeCyan()
    {
        for (int i = 0; i < furniture.transform.childCount; i++)
        {
            GameObject Go = furniture.transform.GetChild(i).gameObject;
            Renderer rend = Go.GetComponent<Renderer>();
            if (rend == null || Go.name == "notif")
            {
                return;
            }
            //rend.material.shader = Shader.Find("HDRenderPipeline/LitTessellation");
            rend.material.SetColor("_BaseColor", Color.cyan);
        }
    }

    public void MakeBrown()
    {
        for (int i = 0; i < furniture.transform.childCount; i++)
        {
            GameObject Go = furniture.transform.GetChild(i).gameObject;
            Renderer rend = Go.GetComponent<Renderer>();
            if (rend == null || Go.name == "notif")
            {
                return;
            }
            //rend.material.shader = Shader.Find("HDRenderPipeline/LitTessellation");
            rend.material.SetColor("_BaseColor", new Color(139f, 69f, 19f));
        }
    }

    public void MakeGrey()
    {
        for (int i = 0; i < furniture.transform.childCount; i++)
        {
            GameObject Go = furniture.transform.GetChild(i).gameObject;
            Renderer rend = Go.GetComponent<Renderer>();
            if (rend == null || Go.name == "notif")
            {
                return;
            }
            //rend.material.shader = Shader.Find("_BaseColor");
            rend.material.SetColor("_BaseColor", Color.grey);
        }
    }

    public void MakePink()
    {
        for (int i = 0; i < furniture.transform.childCount; i++)
        {
            GameObject Go = furniture.transform.GetChild(i).gameObject;
            Renderer rend = Go.GetComponent<Renderer>();
            if (rend == null || Go.name == "notif")
            {
                return;
            }
            //rend.material.shader = Shader.Find("_BaseColor");
            Color pink = new Color(255f, 182f, 193f);
            rend.material.SetColor("_BaseColor", pink);
        }
    }

}
