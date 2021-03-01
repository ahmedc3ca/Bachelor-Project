using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class CustomNetworkManager : MonoBehaviour
{   
    // UI input fields
    private InputField HostPort_input;
    private InputField JoinIP_input;
    private InputField JoinPort_input;


    void Start()
    {
        HostPort_input = GameObject.Find("HostPort").GetComponent<InputField>();
        JoinIP_input = GameObject.Find("JoinIP").GetComponent<InputField>();
        JoinPort_input = GameObject.Find("JoinPort").GetComponent<InputField>();

    }

    // Host button callback
    public void StartServer()
    {
     //   NetworkManager.singleton.networkPort = int.Parse(HostPort_input.text); // Get port and put it in the network manager
        NetworkManager.singleton.StartHost(); // Newtork command allowing to create server + switch to network scene
    }

    // Connection button callback
    public void JoinServer()
    {
        NetworkManager.singleton.networkAddress = JoinIP_input.text; // Get ip address and put it in the network manager
       // NetworkManager.singleton.networkPort = int.Parse(JoinPort_input.text); // Get port and put it in the network manager
        NetworkManager.singleton.StartClient(); // Network command allowing to given server
    }
}
