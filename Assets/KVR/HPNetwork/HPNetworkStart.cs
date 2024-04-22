using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Relay.Models;
using Unity.Services.Relay;

public class HPNetworkStart : MonoBehaviour
{
    [SerializeField] private NetworkManager networkManager;
    private async void Awake()
    {
        if (!networkManager.IsHost)
        {
            await UnityServices.InitializeAsync();
            AuthenticationService.Instance.SignedIn += () =>
            {
                Debug.Log("Signed in " + AuthenticationService.Instance.PlayerId);
            };
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            networkManager.StartHost(); 
            createRelay(); 
        }
    }

    private async void createRelay()
    {
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(3);
            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            Debug.Log(joinCode);
        }
        catch (RelayServiceException e)
        { Debug.Log(e); }
    }
}
