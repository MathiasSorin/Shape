//Made by: Mathias Sorin
//Last updated: 19/06/2021

using MLAPI;
using UnityEngine;
using MLAPI.Transports.PhotonRealtime;

public class NetworkConnectionManager : MonoBehaviour
{
    //Transport component
    PhotonRealtimeTransport transport;

    //Menu ui strings
    public string roomName;
    public string roomPassword;

    //Spawn position
    private Vector3 spawnPosition;

    public void StartHost()
    {
        NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
        NetworkManager.Singleton.StartHost(spawnPosition, Quaternion.identity, true, GetPlayerHash());
    }

    public void StartClient()
    {
        NetworkManager.Singleton.NetworkConfig.ConnectionData = System.Text.Encoding.ASCII.GetBytes(roomPassword);
        NetworkManager.Singleton.StartClient();
    }

    //Approval check is called server side when someone tries to connect
    private void ApprovalCheck(byte[] connectionData, ulong clientID, NetworkManager.ConnectionApprovedDelegate callback)
    {
        //Check incoming password
        bool approve = System.Text.Encoding.ASCII.GetString(connectionData) == roomPassword;
        //Callback will define with wich prefab and where to spawn connecting player
        callback(true, GetPlayerHash(), approve, spawnPosition, Quaternion.identity);
    }

    //Assign menu ui roomName to the RoomName user wants to connect to
    public void SetRoomName()
    {
        transport = NetworkManager.Singleton.GetComponent<PhotonRealtimeTransport>();
        transport.RoomName = roomName;
    }

    public void SetSpawnPosition(Vector3 newSpawnPostition)
    {
        spawnPosition = newSpawnPostition;
    }

    //Get prefab hash (wich prefab to spawn) currently only host can determine the prefab :(
    //TO BE REMOVED
    public GameObject[] playerClasses;
    private int playerClassSelection = 0;
    private ulong GetPlayerHash()
    {
        return playerClasses[playerClassSelection].GetComponent<NetworkObject>().PrefabHash;
    }
}
