using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureCommentManager : MonoBehaviour
{
    public ArrayList comments;
    public bool showsArrows;
    public GameObject arrowPrefab;
    public GameObject[] arrows = new GameObject[4];
    private GameObject notificationBall;
    
    // Start is called before the first frame update
    void Start()
    {

        showsArrows = false;
        comments = new ArrayList();
        notificationBall = transform.Find("notif").gameObject;
        notificationBall.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        notificationBall.SetActive(comments.Count != 0);
    }

    public void DestroyArrows()
    {
        for(int i = 0; i < arrows.Length; i++)
        {
            Destroy(arrows[i]);
        }
    }

    public void CreateArrows()
    {
        // First Arrow
        GameObject arrow = Instantiate(arrowPrefab, transform.position + Vector3.forward * 1.5f + Vector3.up * 0.5f, Quaternion.Euler(new Vector3(0, 180, 0)));
        arrow.GetComponent<FurnitureArrow>().direction = 0;
        arrow.GetComponent<FurnitureArrow>().controlledFurniture = this.gameObject;
        arrows[0] = arrow;

        //Second Arrow
        arrow = Instantiate(arrowPrefab, transform.position + Vector3.right * 1.5f + Vector3.up * 0.5f, Quaternion.Euler(new Vector3(0, 270, 0)));
        arrow.GetComponent<FurnitureArrow>().direction = 1;
        arrow.GetComponent<FurnitureArrow>().controlledFurniture = this.gameObject;
        arrows[1] = arrow;

        //Third Arrow
        arrow = Instantiate(arrowPrefab, transform.position - Vector3.forward * 1.5f + Vector3.up * 0.5f, Quaternion.identity);
        arrow.GetComponent<FurnitureArrow>().direction = 2;
        arrow.GetComponent<FurnitureArrow>().controlledFurniture = this.gameObject;
        arrows[2] = arrow;

        //Fourth Arrow
        arrow = Instantiate(arrowPrefab, transform.position - Vector3.right * 1.5f + Vector3.up * 0.5f, Quaternion.Euler(new Vector3(0, 90, 0)));
        arrow.GetComponent<FurnitureArrow>().direction = 3;
        arrow.GetComponent<FurnitureArrow>().controlledFurniture = this.gameObject;
        arrows[3] = arrow;
    }

    public void Move(int direction)
    {
        switch (direction)
        {
            case 0:
                foreach (GameObject arrow in arrows)
                {
                    arrow.transform.position += Vector3.forward * Time.deltaTime;
                }
                transform.position += Vector3.forward * Time.deltaTime;
                break;
            case 1:
                foreach (GameObject arrow in arrows)
                {
                    arrow.transform.position += Vector3.right * Time.deltaTime;
                }
                transform.position += Vector3.right * Time.deltaTime;
                break;
            case 2:
                foreach (GameObject arrow in arrows)
                {
                    arrow.transform.position -= Vector3.forward * Time.deltaTime;
                }
                transform.position -= Vector3.forward * Time.deltaTime;
                break;
            case 3:
                foreach (GameObject arrow in arrows)
                {
                    arrow.transform.position -= Vector3.right * Time.deltaTime;
                }
                transform.position -= Vector3.right * Time.deltaTime;
                break;
            default:
                break;

        }
    }

    public void Rotate()
    {
        transform.Rotate(new Vector3(0, 1, 0) * 90f, Space.World);
    }

}
