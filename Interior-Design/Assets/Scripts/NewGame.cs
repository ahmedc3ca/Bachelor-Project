using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using System;

public class NewGame : NetworkBehaviour
{

    [SerializeField] List<String> sentencePool = new List<String>();

    // SyncVar allow to have same values on all clients
    [SyncVar] private String sentence;
    [SyncVar] private GameObject screen;
    private String prevSentence;

    // Executed when client joins server
    public override void OnStartClient()
    {
        base.OnStartClient();
        if(screen != null){
            screen.GetComponent<TextMeshPro>().text = sentence; // If sentence has been changed before joining -> update based on the syncVar
        }
    }

    // Find new random sentence in the pool and update screen for each client
    public void ComputeNewSentence()
    {
        // Compute new sentence only on host
        if (isServer){
            
            screen = GameObject.FindWithTag("Screen").transform.Find("RefSentence").gameObject;
            prevSentence = screen.GetComponent<TextMeshPro>().text;
            sentence = sentencePool[UnityEngine.Random.Range(0, sentencePool.Count)]; //Find new sentence
            
            // If same as previous, find new one
            while(prevSentence.Equals(sentence))
            {
                sentence = sentencePool[UnityEngine.Random.Range(0, sentencePool.Count)];
            }

            RpcChangeRefText(sentence,screen); // Update screen on each client
        }
    }

    // Executed on all clients
    [ClientRpc]
    void RpcChangeRefText(String str, GameObject obj){
        obj.GetComponent<TextMeshPro>().text = str;
    }
}
