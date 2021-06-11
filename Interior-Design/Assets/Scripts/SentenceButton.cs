using UnityEngine;
using Mirror;

public class SentenceButton : NetworkBehaviour
{
    [SerializeField] private float maxDistance = 3;
    RaycastHit hit;

    void Update()
    {
        if (Physics.Raycast(transform.Find("Camera").position, transform.Find("Camera").TransformDirection(Vector3.forward), out hit, maxDistance))
        {
            var selection = hit.transform;
            if (selection != null){
                // Check when Sentence Button is clicked
                if (selection.name == "SentenceButton" && isLocalPlayer && Input.GetMouseButtonDown(0)) 
                {
                    CmdNewSentence();
                }
            }
        }
    }

    // Call ComputeNewSentence method on server (GameManager game object)
    [Command]
    void CmdNewSentence(){
        GameObject.Find("GameManager").GetComponent<NewGame>().ComputeNewSentence();
    }     
}
