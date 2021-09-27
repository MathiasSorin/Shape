//Made by: Mathias Sorin
//Last updated: 25/06/2021

using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using UnityEngine.UI;

public class ClassChange : NetworkBehaviour, IInteractable
{
    //Player Class to switch to
    public GameObject playerPrefab;

    //Image to display class icon
    [SerializeField] private Image classIcon;

    private void Start()
    {
        classIcon.sprite = playerPrefab.GetComponent<PlayerClass>().classIcon;
    }

    public void GetActivated()
    {
        SpawnPlayerServerRpc(NetworkManager.LocalClientId);
    }

    //Instantiate new desired player prefab, destroy old prefab, spawn as player (updates this information on all clients)
    [ServerRpc(RequireOwnership = false)]
    private void SpawnPlayerServerRpc(ulong id)
    {
        GameObject newPlayerPrefab = Instantiate(playerPrefab, transform.position, Quaternion.identity);
        Destroy(NetworkManager.Singleton.ConnectedClients[id].PlayerObject.gameObject);
        newPlayerPrefab.GetComponent<NetworkObject>().SpawnAsPlayerObject(id);
    }
}
