//Made by: Mathias Sorin
//Last updated: 28/05/2021

using MLAPI;
using UnityEngine;
using MLAPI.Transports.PhotonRealtime;

//Main menu script to host or connect to a room
public class NetworkUI : MonoBehaviour
{
    PhotonRealtimeTransport transport;

    //Main menu panel (contains a camera too)
    public GameObject mainMenuButtonPanel;

    //Menu ui strings
    public string roomName;
    public string roomPassword;

    //TO BE REMOVED
    public GameObject[] playerClasses;
    private int playerClassSelection = 0;

    //Get prefab hash (wich prefab to spawn) currently only host can determine the prefab :(
    private ulong GetPlayerHash()
    {
        return playerClasses[playerClassSelection].GetComponent<NetworkObject>().PrefabHash;
    }

    //Called when creating a room
    public void Host()
    {
        GetRoomName();
        NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
        NetworkManager.Singleton.StartHost(GameManager.Instance.spawnPoint.position, Quaternion.identity, true, GetPlayerHash());
        mainMenuButtonPanel.SetActive(false);
    }

    //Called when clients attemp to join a room
    public void Join()
    {
        GetRoomName();
        NetworkManager.Singleton.NetworkConfig.ConnectionData = System.Text.Encoding.ASCII.GetBytes(roomPassword);
        NetworkManager.Singleton.StartClient();
        mainMenuButtonPanel.SetActive(false);
    }

    //Approval check is called server side when someone tries to connect
    private void ApprovalCheck(byte[] connectionData, ulong clientID, NetworkManager.ConnectionApprovedDelegate callback)
    {
        //Check incoming password
        bool approve = System.Text.Encoding.ASCII.GetString(connectionData) == roomPassword;
        //Callback will define with wich prefab and where to spawn connecting player
        callback(true, GetPlayerHash(), approve, GameManager.Instance.spawnPoint.position, Quaternion.identity);
    }

    //Assign menu ui roomName to the RoomName user wants to connect to
    private void GetRoomName()
    {
        transport = NetworkManager.Singleton.GetComponent<PhotonRealtimeTransport>();
        transport.RoomName = roomName;
    }

    //Main menu ui functions
    public void RoomNameChanged(string newRoomName)
    {
        this.roomName = newRoomName;
    }

    public void RoomPasswordChanged(string newRoomPassword)
    {
        this.roomPassword = newRoomPassword;
    }

    public void ClassChanged()
    {
        this.playerClassSelection = 1;
    }
}
