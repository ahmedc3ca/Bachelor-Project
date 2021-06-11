using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using System;

public class WinOrLose : NetworkBehaviour
{
    // SyncVars for current gameobjects states
    [SyncVar]
    private GameObject screen;
    [SyncVar]
    private Color screenColor;
    [SyncVar]
    private GameObject table;
    [SyncVar]
    private Color tableColor;

    // SyncVars for current token sentence
    [SyncVar] private String constructedSentence;


    // Executed when client joins server
    public override void OnStartClient()
    {
        base.OnStartClient();
        if(screen !=null){
            screen.GetComponent<MeshRenderer>().material.color = screenColor; // Update screen color
            screen.GetComponent<TextMeshPro>().text = constructedSentence; // Update screen to display current token sentence
        }

        if(table !=null){
            table.GetComponent<MeshRenderer>().material.color = tableColor; // Update table color
        }
    }

    // Change token sequence on screen + check if it is correct
    public void UpdateTokenSentence(GameObject[] arrayTokens){
        if(isServer){
            constructedSentence ="";
            List<GameObject> listTokens = new List<GameObject>(arrayTokens);
            ScanSequence(listTokens); // Compute sequence from tokens

            // Change screen on client and host
            screen = GameObject.Find("Screen").transform.Find("UserSentence").gameObject;
            RpcChangeText(screen,constructedSentence);
            screen.GetComponent<TextMeshPro>().text = constructedSentence; // Change on host side directly because RPC can take time

            CheckIfWin(); // Check if sentence is correct
        }
    }

    // Compute string sequence from tokens
    public void ScanSequence(List<GameObject> listTokens){
        foreach (var token in listTokens)
        {
            string word = token.transform.Find("TXT1").gameObject.GetComponent<TextMeshPro>().text;
            constructedSentence = word + " " + constructedSentence;
        }
    }

    // Change token sentence on all clients
    [ClientRpc]
    void RpcChangeText(GameObject obj, String str){
        obj.GetComponent<TextMeshPro>().text = str;
    }

    // Check if both sentence correspond and update environment 
    public void CheckIfWin()
    {
        if (isServer){
            // Get sentence on screen
            String userSentence = GameObject.Find("Screen").transform.Find("UserSentence").gameObject.GetComponent<TextMeshPro>().text; 
            String refSentence = GameObject.Find("Screen").transform.Find("RefSentence").gameObject.GetComponent<TextMeshPro>().text;    

            // Format token sentence: Uppercase for first letter, dot at the end
            if(!string.IsNullOrEmpty(userSentence)){
                userSentence = char.ToUpper(userSentence[0]) + userSentence.Substring(1);
                userSentence = userSentence.Substring(0, userSentence.Length - 1);
                userSentence = userSentence + ".";
            }
            
            // Check if both sentence are equal and update world according to sentence
            if(userSentence.Equals(refSentence)){
                // Sentence about table color
                if(userSentence.Equals("The table looks green.")){
                    table = GameObject.Find("Table").gameObject;
                    tableColor = Color.green;
                    RpcChangeColor(tableColor, table); // Change color of table on all clients
                }
                
                // Sentence about screen color
                else if(userSentence.Equals("The screen is becoming yellow.")){
                    screen = GameObject.Find("Screen").gameObject;
                    screenColor = Color.yellow;
                    RpcChangeColor(screenColor,screen); // Change color of screen on all clients
                }
            }
        }
    }

    // Change color of object on all clients
    [ClientRpc]
    void RpcChangeColor(Color col, GameObject obj){
        obj.GetComponent<MeshRenderer>().material.color = col;
    }

}
