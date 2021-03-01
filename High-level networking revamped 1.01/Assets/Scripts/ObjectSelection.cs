using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Mirror;


public class ObjectSelection : NetworkBehaviour
{
    [SerializeField] private float maxDistance = 3;
    private Transform _selection; // current pointed token
    public bool carrying = false; // is player carrying an object ?
    

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.Find("Camera").position, transform.Find("Camera").TransformDirection(Vector3.forward), out hit, maxDistance)){
            
            var selection = hit.transform;
            
            if (selection.tag == "Token") // The pointed object is a selectable one
            {
                _selection = selection;
                if (isLocalPlayer)
                {
                    ActionOnMouse(); // Wait for mouse input
                }             
            }

            else
            {
                NothingSelected(); // Object pointed is not selectable
            }
        }

        else
        {
            NothingSelected(); // No object pointed
        } 
    }

    // Applies changes according to mouse input
    void ActionOnMouse(){
        Image Knob = GameObject.Find("ImageCrossHair").GetComponent<Image>();
        Knob.color = new Color32(255, 0, 0, 255); // Crosshair colored in red
    
        if (!carrying && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(GiveAuthorityAndAddJoint()); // give authority and when done -> add joint           
        }

        else if (carrying && Input.GetMouseButtonDown(0))
        {
            _selection.GetComponent<FixedJoint>().connectedBody = null;
            Destroy(_selection.GetComponent<FixedJoint>()); // Remove joint between player and object
            carrying = false; // Boolean to indicate we are not carrying an object anymore
        }

        // Change distance of carried object when scrolling with the mouse
        if (carrying && Input.GetAxis("Mouse ScrollWheel") != 0){
            Transform camera = transform.Find("Camera");
            
            _selection.GetComponent<FixedJoint>().connectedBody = null;
            Vector3 direction = camera.TransformDirection(Vector3.forward);
            Vector3 amplitude = direction*Input.GetAxis("Mouse ScrollWheel");

            float nextDistance = Vector3.Distance(_selection.position + amplitude , camera.position);
            
            if (nextDistance > 0.5 && nextDistance < maxDistance){
                _selection.position += amplitude;
            }
            _selection.GetComponent<FixedJoint>().connectedBody = transform.Find("Camera").GetComponent<Rigidbody>();
        }
    }

    // Reset selected object and crosshair when no object selected
    void NothingSelected()
    {
        if (_selection != null)
        {
            if (isLocalPlayer)
            {
                Image Knob = GameObject.Find("ImageCrossHair").GetComponent<Image>();
                Knob.color = new Color32(255, 255, 255, 255); // Crosshair is back to white
            }
            _selection = null; // Global variable containing the selected object is set to null
        }
    }

    // Coroutine to give authority to player and create joint between object and player
    IEnumerator GiveAuthorityAndAddJoint()
    {   
        CmdGiveAuthToClient(_selection.gameObject); // Give authority on object for this player

        yield return new WaitForSeconds(0.04f); // Delay to be sure we have authority on the object

        _selection.gameObject.AddComponent<FixedJoint>();
        _selection.GetComponent<FixedJoint>().connectedBody = transform.Find("Camera").GetComponent<Rigidbody>(); // Fix object to player camera
        carrying = true; // Indicate we are carrying an object
    }

    // Tell server which player has now authority on given object
    [Command]
    void CmdGiveAuthToClient(GameObject obj)
    {
        // Find object by identity and find current owner
        var networkIdentity = obj.GetComponent<NetworkIdentity>();
        var otherOwner = networkIdentity.connectionToClient;

        // Check if we already are the user with authority
        if (otherOwner == GetComponent<NetworkIdentity>().connectionToClient){
            return;
        }
        else    
        {
            if (otherOwner != null)
            {
                networkIdentity.RemoveClientAuthority();   
            }
            networkIdentity.AssignClientAuthority(GetComponent<NetworkIdentity>().connectionToClient); // Assign authority to player
        }
    }

}
