using UnityEngine;
using Mirror;
using UnityEngine.UI;
using TMPro;

public class CustomNetworkManager : MonoBehaviour
{   
    // UI input fields
    private TMP_InputField HostPort_input;
    private TMP_InputField JoinIP_input;
    private TMP_InputField JoinPort_input;


    void Start()
    {
        HostPort_input = GameObject.Find("HostPort").GetComponent<TMP_InputField>();
        JoinIP_input = GameObject.Find("JoinIP").GetComponent<TMP_InputField>();
        JoinPort_input = GameObject.Find("JoinPort").GetComponent<TMP_InputField>();

    }

    // Host button callback
    public void StartServer()
    {
        //   NetworkManager.singleton.networkPort = int.Parse(HostPort_input.text); // Get port and put it in the network manager
        string userInput = (HostPort_input.text == "") ? "7777" : HostPort_input.text;
        this.GetComponent<TelepathyTransport>().port = ushort.Parse(userInput);
        NetworkManager.singleton.StartHost(); // Newtork command allowing to create server + switch to network scene
    }

    // Connection button callback
    public void JoinServer()
    {
        NetworkManager.singleton.networkAddress = JoinIP_input.text; // Get ip address and put it in the network manager
       // NetworkManager.singleton.networkPort = int.Parse(JoinPort_input.text); // Get port and put it in the network manager
       this.GetComponent<TelepathyTransport>().port = ushort.Parse(JoinPort_input.text);
        NetworkManager.singleton.StartClient(); // Network command allowing to given server
    }
}
