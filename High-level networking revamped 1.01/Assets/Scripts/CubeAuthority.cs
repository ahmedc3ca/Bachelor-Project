using UnityEngine;
using Mirror;

public class CubeAuthority : NetworkBehaviour
{
    void Update()
    {
        // Allows to detect when the local player loses authority on the object (another player picked it while he was carrying it)
        if (!hasAuthority)
        {
            // If a joint exists it must be removed
            if (GetComponent<FixedJoint>() != null) 
            {
                GetComponent<FixedJoint>().connectedBody = null;
                Destroy(GetComponent<FixedJoint>()); // Remove the link between the cube and the local player
                
                ResetCarryingLocalPlayer(); // Reset carrying boolean for local player
            }   
        }
    }

    // Action to perform when token is destroyed
    public override void OnStopClient(){ //CHANGED https://mirror-networking.com/docs/Articles/General/Deprecations.html
        ResetCarryingLocalPlayer();
    }

    // Reset carrying boolean for local player
    void ResetCarryingLocalPlayer(){
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                player.GetComponent<ObjectSelection>().carrying = false; // Player is not carrying the cube anymore -> the boolean must be updated
            }
        }
    }
}
