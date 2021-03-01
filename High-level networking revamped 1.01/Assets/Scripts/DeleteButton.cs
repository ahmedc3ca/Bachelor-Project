using UnityEngine;
using Mirror;

public class DeleteButton : NetworkBehaviour
{
    [SerializeField] private float maxDistance = 3;

    RaycastHit hit;

    void Update()
    {
        if (Physics.Raycast(transform.Find("Camera").position, transform.Find("Camera").TransformDirection(Vector3.forward), out hit, maxDistance))
        {
            var selection = hit.transform;
            if (selection != null){
                // Check for click on Delete Button
                if (selection.name == "DeleteButton" && isLocalPlayer && Input.GetMouseButtonDown(0)) 
                {
                    CmdDestroyAll(); // Destroy all tokens 
                }
            }
        }
    }

    // Destroy all objects on server for all clients
    [Command]
    void CmdDestroyAll()
    {
        GameObject[] tokens = GameObject.FindGameObjectsWithTag("Token");
        foreach (GameObject go in tokens)
        {
            NetworkServer.Destroy(go);
        }
    }    
}
