using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WallMover : NetworkBehaviour
{

    public float eyeSight = 100f;
    private bool firstHeld = true;
    private float rot = 0f;
    public bool commentIsOn;
    public GameObject commentui;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (isLocalPlayer && Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(transform.Find("Camera").position, transform.Find("Camera").TransformDirection(Vector3.forward), out hit, eyeSight))
            {
                var selection = hit.transform;
                if (firstHeld)
                {
                    rot = transform.rotation.y;
                    firstHeld = false;
                }
                if (selection.tag == "Wall") // The pointed object is a selectable one
                {
                    WallProperties wallProperties = selection.GetComponent<WallProperties>();
                        if (wallProperties.direction == "x")
                        {
                            selection.transform.Translate(new Vector3(0.1f*(rot - transform.rotation.y), 0f, 0f));
                        }
                        else if (wallProperties.direction == "z")
                        {
                            selection.transform.Translate(new Vector3(0f, 0f, rot - transform.rotation.y));
                        }
                }
                if(selection.tag == "Furniture" && commentIsOn)
                {
                    showCommentUi(selection);
                }
            }
        }
        if (isLocalPlayer && Input.GetMouseButton(1))
        {
            if (Physics.Raycast(transform.Find("Camera").position, transform.Find("Camera").TransformDirection(Vector3.forward), out hit, eyeSight))
            {
                var selection = hit.transform;
                if (firstHeld)
                {
                    rot = transform.rotation.y;
                    firstHeld = false;
                }
                if (selection.tag == "Arrow" && commentIsOn)
                {
                    selection.GetComponent<FurnitureArrow>().moveFurniture();
                    Debug.Log(selection.GetComponent<FurnitureArrow>().direction);
                }
            }
        }

        if (isLocalPlayer && Input.GetMouseButtonUp(0))
        {
            firstHeld = true;
        }
    }

    public void showCommentUi(Transform furnitrure)
    {
        GameObject.Find("CommentCanvas").GetComponent<CommentUIManager>().detected(furnitrure.gameObject);
    }
}