using FishNet;
using FishNet.Discovery;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSeverManager : MonoBehaviour
{
    [SerializeField] private bool isServer;
private void Awake()
    {
        if(isServer)
        {
            InstanceFinder.ServerManager.StartConnection();
            FindObjectOfType<NetworkDiscovery>().AdvertiseServer();

            Debug.Log("Inside");
        }
        InstanceFinder.ClientManager.StartConnection();
        FindObjectOfType<NetworkDiscovery>().SearchForServers();
        Debug.Log("Outside");
       
    }
}
