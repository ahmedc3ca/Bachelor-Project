using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Mirror;
using InputTracking = UnityEngine.XR.InputTracking;
using Node = UnityEngine.XR.XRNode;
using XRNodeState = UnityEngine.XR.XRNodeState;
using UnityEngine.EventSystems;

public class LocalPlayerController : NetworkBehaviour
{
    public GameObject ovrCamRig;
    public Transform leftHand;
    public Transform rightHand;
    public GameObject head;
    public Camera leftEye;
    public Camera rightEye;
    public float speed = 3f;
    private List<XRNodeState> nodeStates = new List<XRNodeState>();
    Vector3 pos;


    public OVRCursor laserPointer;
    private RaycastHit hit;
    private GameObject dragged;
    //ui
    public GameObject bedsCanvas;
    public GameObject chairsCanvas;
    public GameObject tablesCanvas;
    public GameObject sofasCanvas;
    public GameObject uiCanvas;
    public GameObject textureCanvas;
    public GameObject furnitureCanvas;
    public string currentCanvas;

    void Start()
    {

        currentCanvas = "uiCanvas";
        pos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            Destroy(ovrCamRig);
            Destroy(bedsCanvas);
            Destroy(chairsCanvas);
            Destroy(tablesCanvas);
            Destroy(sofasCanvas);
            Destroy(uiCanvas);
            Destroy(textureCanvas);
            Destroy(furnitureCanvas);

            
        }
        else
        {
            //raycaster
            GameObject.Find("EventSystem").GetComponent<OVRInputModule>().m_Cursor = laserPointer;


            if (Physics.Raycast(rightHand.transform.position, rightHand.right, out hit))
            {
                if (hit.collider.gameObject.tag == "Furniture" && OVRInput.GetDown(OVRInput.Button.One) && dragged == null)
                {
                    Debug.Log("selected");
                    dragged = hit.collider.gameObject;
                }
                else if (OVRInput.GetDown(OVRInput.Button.One) && dragged != null)
                {
                    Debug.Log("deselected");
                    dragged = null;
                }


                if (hit.collider.gameObject.tag == "Floor")
                {
                    Debug.Log("detected floor");
                    if (dragged != null)
                    {
                        Debug.Log("dragging on the floor");
                        dragged.transform.position = hit.point;
                    }
                }
            }

            //Destroy(head);
            //take care of camera when other player joins
            if (leftEye.tag != "MainCamera")
            {
                leftEye.tag = "MainCamera";
                leftEye.enabled = true;
            }
            if (rightEye.tag != "MainCamera")
            {
                rightEye.tag = "MainCamera";
                rightEye.enabled = true;
            }

            //take care of hand position tracking
            InputTracking.GetNodeStates(nodeStates);
            var leftHandState = nodeStates.FirstOrDefault(node => node.nodeType == Node.LeftHand);
            var rightHandState = nodeStates.FirstOrDefault(node => node.nodeType == Node.RightHand);
            Quaternion rotation;
            Vector3 position;

            leftHandState.TryGetPosition(out position);
            leftHandState.TryGetRotation(out rotation);
            rotation *= Quaternion.Euler(0, 90, 0);
            rotation *= Quaternion.Euler(90, 0, 0);

            leftHand.localRotation = rotation;
            leftHand.localPosition = position;

            //control canvases
            //bedsCanvas.transform.position = position;
            //sofasCanvas.transform.position = position;
            //tablesCanvas.transform.position = position;
            //chairsCanvas.transform.position = position;
            //furnitureCanvas.transform.position = position;
            //textureCanvas.transform.position = position;
            //uiCanvas.transform.position = position;


            rightHandState.TryGetPosition(out position);
            rightHandState.TryGetRotation(out rotation);
            rotation *= Quaternion.Euler(0, 270, 0);
            rotation *= Quaternion.Euler(270, 0, 0);
            rightHand.localRotation = rotation;
            rightHand.localPosition = position;

            //handle position and rotation of player
            Vector2 primaryAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            if (primaryAxis.y > 0f)
            {
                pos += (primaryAxis.y * transform.forward * Time.deltaTime * speed);
            }
            if (primaryAxis.y < 0f)
            {
                pos += (Mathf.Abs(primaryAxis.y) * -transform.forward * Time.deltaTime * speed); //to be refactored
            }
            if (primaryAxis.x > 0f)
            {
                pos += (primaryAxis.x * transform.right * Time.deltaTime * speed);
            }
            if (primaryAxis.x < 0f)
            {
                pos += (Mathf.Abs(primaryAxis.x) * -transform.right * Time.deltaTime * speed); //to be refactored
            }


            transform.position = pos;

            Vector3 euler = transform.rotation.eulerAngles;
            Vector2 secondaryAxis = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
            euler.y += secondaryAxis.x;
            transform.rotation = Quaternion.Euler(euler);
            transform.localRotation = Quaternion.Euler(euler);



            //if(currentCanvas == "uiCanvas")
            //{
            //    if (OVRInput.GetDown(OVRInput.Button.One)) //OPEN furniture window
            //    {
            //        uiCanvas.SetActive(false);
            //        furnitureCanvas.SetActive(true);
            //        currentCanvas = "furnitureCanvas";
            //    }
            //    if (OVRInput.GetDown(OVRInput.Button.Two)) //open texture window
            //    {
            //        uiCanvas.SetActive(false);
            //        textureCanvas.SetActive(true);
            //        currentCanvas = "textureCanvas";
            //    }
            //}
            //else if(currentCanvas == "furnitureCanvas")
            //{
            //    if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick))
            //    {
            //        furnitureCanvas.SetActive(false);
            //        uiCanvas.SetActive(true);
            //        currentCanvas = "uiCanvas";
             
            //    }
            //    if (OVRInput.GetDown(OVRInput.Button.One)) //sofas
            //    {
            //        furnitureCanvas.SetActive(false);
            //        sofasCanvas.SetActive(true);
            //        currentCanvas = "sofasCanvas";
            //    }
            //    if (OVRInput.GetDown(OVRInput.Button.Two)) //tables
            //    {
            //        furnitureCanvas.SetActive(false);
            //        tablesCanvas.SetActive(true);
            //        currentCanvas = "tablesCanvas";
            //    }
            //    if (OVRInput.GetDown(OVRInput.Button.Three)) //chairs
            //    {
            //        furnitureCanvas.SetActive(false);
            //        chairsCanvas.SetActive(true);
            //        currentCanvas = "chairsCanvas";
            //    }
            //    if (OVRInput.GetDown(OVRInput.Button.Four)) //beds
            //    {
            //        furnitureCanvas.SetActive(false);
            //        bedsCanvas.SetActive(true);
            //        currentCanvas = "bedsCanvas";
            //    }
            //}
            //else if(currentCanvas == "textureCanvas")
            //{
            //    if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick))
            //    {
            //        textureCanvas.SetActive(false);
            //        uiCanvas.SetActive(true);
            //        currentCanvas = "uiCanvas";
            //    }
            //    if (OVRInput.GetDown(OVRInput.Button.One))
            //    {
            //        textureCanvas.GetComponent<TextureUIManager>().FloorTexture1();
            //    }
            //}

            //else if(currentCanvas == "sofasCanvas")
            //{
            //    if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick))
            //    {
            //        sofasCanvas.SetActive(false);
            //        furnitureCanvas.SetActive(true);
            //        currentCanvas = "furnitureCanvas";
            //    }

            //    if (OVRInput.GetDown(OVRInput.Button.One))
            //    {
            //        sofasCanvas.GetComponent<SofasUIManager>().SpawnSofa1();
            //    }
            //    if (OVRInput.GetDown(OVRInput.Button.Two)) 
            //    {
            //        sofasCanvas.GetComponent<SofasUIManager>().SpawnSofa2();
            //    }
            //    if (OVRInput.GetDown(OVRInput.Button.Three)) 
            //    {
            //        sofasCanvas.GetComponent<SofasUIManager>().SpawnSofa3();
            //    }
            //    if (OVRInput.GetDown(OVRInput.Button.Four)) 
            //    {
            //        sofasCanvas.GetComponent<SofasUIManager>().SpawnSofa4();
            //    }
            //}

            //else if (currentCanvas == "tablesCanvas")
            //{
            //    if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick))
            //    {
            //        tablesCanvas.SetActive(false);
            //        furnitureCanvas.SetActive(true);
            //        currentCanvas = "furnitureCanvas";
            //    }
            //    if (OVRInput.GetDown(OVRInput.Button.One))
            //    {
            //        tablesCanvas.GetComponent<SofasUIManager>().SpawnSofa1();
            //    }
            //    if (OVRInput.GetDown(OVRInput.Button.Two))
            //    {
            //        tablesCanvas.GetComponent<SofasUIManager>().SpawnSofa2();
            //    }
            //    if (OVRInput.GetDown(OVRInput.Button.Three))
            //    {
            //        tablesCanvas.GetComponent<SofasUIManager>().SpawnSofa3();
            //    }
            //}

            //else if (currentCanvas == "chairsCanvas")
            //{
            //    if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick))
            //    {
            //        chairsCanvas.SetActive(false);
            //        furnitureCanvas.SetActive(true);
            //        currentCanvas = "furnitureCanvas";
            //    }
            //    if (OVRInput.GetDown(OVRInput.Button.One))
            //    {
            //        chairsCanvas.GetComponent<SofasUIManager>().SpawnSofa1();
            //    }
            //    if (OVRInput.GetDown(OVRInput.Button.Two))
            //    {
            //        chairsCanvas.GetComponent<SofasUIManager>().SpawnSofa2();
            //    }
            //    if (OVRInput.GetDown(OVRInput.Button.Three))
            //    {
            //        chairsCanvas.GetComponent<SofasUIManager>().SpawnSofa3();
            //    }
            //    if (OVRInput.GetDown(OVRInput.Button.Four))
            //    {
            //        chairsCanvas.GetComponent<SofasUIManager>().SpawnSofa4();
            //    }
            //    if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick))
            //    {
            //        chairsCanvas.GetComponent<SofasUIManager>().SpawnSofa5();
            //    }
            //}

            //else if (currentCanvas == "bedsCanvas")
            //{
            //    if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick))
            //    {
            //        bedsCanvas.SetActive(false);
            //        furnitureCanvas.SetActive(true);
            //        currentCanvas = "furnitureCanvas";
            //    }
            //    if (OVRInput.GetDown(OVRInput.Button.One))
            //    {
            //        bedsCanvas.GetComponent<SofasUIManager>().SpawnSofa1();
            //    }
            //    if (OVRInput.GetDown(OVRInput.Button.Two))
            //    {
            //        bedsCanvas.GetComponent<SofasUIManager>().SpawnSofa2();
            //    }
            //    if (OVRInput.GetDown(OVRInput.Button.Three))
            //    {
            //        bedsCanvas.GetComponent<SofasUIManager>().SpawnSofa3();
            //    }
            //}



        }
    }
}
