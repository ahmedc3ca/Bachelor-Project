using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Mirror;

public class AlignButton : NetworkBehaviour
{   
    // Fields
    [SerializeField] private float maxDistance = 3;
    [SerializeField] private float distanceAlignment = 0.2f;

    // Coordinates constants
    Vector3 TABLECENTER = new Vector3(0,-0.25f,-2);
    Vector3 BOXDIMENSIONS = new Vector3(3.75f,0.05f,0.5f);
    Vector3 TABLERIGHT = new Vector3(-3.75f,-0.25f,-2);

    // Tokens
    private int numberTokens = 0;
    private List<Collider> listTokensTableCol = new List<Collider>();
    private List<GameObject> listTokensTable = new List<GameObject>();
    Vector3 firstPosition;


    RaycastHit hit;
    
    void Update()
    {   
        if (Physics.Raycast(transform.Find("Camera").position, transform.Find("Camera").TransformDirection(Vector3.forward), out hit, maxDistance))
        {
            var selection = hit.transform;
            if (selection != null){

                // Clicked on Align Button
                if (selection.name == "AlignButton" && isLocalPlayer && Input.GetMouseButtonDown(0)) 
                {
                    // Reset variables
                    numberTokens = 0;
                    listTokensTableCol.Clear(); 
                    listTokensTable.Clear();

                    // Scan Tokens and align them on table
                    CountTokensTable();
                    ColToTransf();
                    SortByCoordinate();
                    ReplaceObjects();
                }
            }
        }
    }

    // Count the number of tokens on table and add their collider to the list
    public void CountTokensTable(){
        Collider[] objectsOnTable = Physics.OverlapBox(TABLECENTER,BOXDIMENSIONS); // Find all tokens on table
        foreach (var collider in objectsOnTable)
        {
            if (collider.transform.tag == "Token"){
                numberTokens++;
                listTokensTableCol.Add(collider); // Add to collider list
            }     
        }      
    }

    // Convert colliders to gameObjects
    public void ColToTransf(){
        foreach (var collider in listTokensTableCol)
        {
            listTokensTable.Add(collider.gameObject);
        }  
    }

    // Sort list by position on table
    public void SortByCoordinate(){
        listTokensTable = listTokensTable.OrderBy(x => Vector3.Distance(x.transform.position, TABLERIGHT)).ToList();
    } 

    // Compute position of first token and move each to its position
    public void ReplaceObjects(){
        // Check if odd or even number of tokens and compute position of first token
        if(numberTokens%2 == 0){
            firstPosition = new Vector3(-numberTokens/2*distanceAlignment+distanceAlignment/2,0,-2);
        }
        else{
            
            firstPosition = new Vector3(-numberTokens/2*distanceAlignment,0,-2);
        }
        StartCoroutine(GiveAuthorityAndMove()); // Assign authority to player who clicked the button and move objects
    }

    // Coroutine to give authority to player and move objects
    IEnumerator GiveAuthorityAndMove()
    {
        foreach (var token in listTokensTable)
        {
            CmdGiveAuthToClient(token);
        }

        yield return new WaitForSeconds(0.2f); // Delay to be sure we have authority on all tokens before moving them

        int i = 0;

        // Move each object to its new location, given the space between each
        foreach (var token in listTokensTable)
        {
            Vector3 shift = new Vector3(i*distanceAlignment,0,0);
            token.transform.position = firstPosition + shift;
            token.transform.rotation = Quaternion.identity;
            i++;
        }
    }

    // Tell server which player has now authority on given object
    [Command]
    void CmdGiveAuthToClient(GameObject token)
    {
        // Find object by identity and find current owner
        var networkIdentity = token.GetComponent<NetworkIdentity>();
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
