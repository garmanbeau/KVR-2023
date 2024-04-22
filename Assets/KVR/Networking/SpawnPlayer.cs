using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Connection;
using FishNet.Object.Synchronizing;

public class SpawnPlayer : NetworkBehaviour
{
    [SerializeField] private GameObject playerPrefab;

  
    public override void OnStartClient()
    {
        Debug.Log("Inside OnStartClient");
        base.OnStartClient();

        Debug.Log("Past OnStartClient");
       // if (!IsOwner) return;

        PlayerSpawn();
        Debug.Log("Past PlayerSpawn");
    }

    [ServerRpc(RequireOwnership = false)]
    private void PlayerSpawn(NetworkConnection client = null)
    {
        GameObject go = Instantiate(playerPrefab);

        Debug.Log("Past Gamobject go");

        Spawn(go, client);

        Debug.Log("Past Spawn call");
    }
}
