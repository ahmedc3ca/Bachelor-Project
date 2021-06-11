using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureArrow : MonoBehaviour
{
    public int direction;
    public GameObject controlledFurniture;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void moveFurniture()
    {
        if(controlledFurniture == null)
        {
            return;
        }
        controlledFurniture.GetComponent<FurnitureCommentManager>().Move(direction);
    }
}
