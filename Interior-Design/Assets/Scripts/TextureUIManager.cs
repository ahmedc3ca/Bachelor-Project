using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureUIManager : MonoBehaviour
{
    public Material material1;
    public GameObject hand;
    public GameObject uiMenu;
    public GameObject uiManagerUI;
    // Start is called before the first frame update
    void Start()
    {
        uiMenu.SetActive(false);
    }

    public void LeaveWindow()
    {
        uiManagerUI.SetActive(true);
        uiMenu.SetActive(false);
    }


    private void Update()
    {
        Vector3 offset = new Vector3(0, 0.5f, 0);
        transform.position = hand.transform.position + offset;
        transform.localRotation = hand.transform.localRotation * Quaternion.Euler(180, 0, 0) * Quaternion.Euler(0, 180, 0);
    }


    public void FloorTexture1()
    {
        GameObject[] floors = GameObject.FindGameObjectsWithTag("Floor");
        GameObject floor = floors[0];
        //var texture = Resources.Load<Texture>("Assets/Resources/FloorTextures/Woodenfloor01/Materials/Wooden_floor_diffuse");
        //floor.GetComponent<Renderer>().material.shader = Shader.Find("Lit");
        floor.GetComponent<Renderer>().material = material1;
    }
}
