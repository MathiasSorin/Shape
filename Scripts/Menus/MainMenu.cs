//Made by: Mathias Sorin
//Last updated: 28/05/2021

using UnityEngine;

//Main menu script to host or connect to a room
public class MainMenu : MonoBehaviour
{
    //Main menu panel (contains a camera too)
    public GameObject mainMenuButtonPanel;

    //Scene to load
    public string scene;

    //Game Manager components reference
    private NetworkConnectionManager networkConnectionManager;
    private SceneLoader sceneLoader;

    private void Start()
    {
        networkConnectionManager = GameManager.Instance.networkConnectionManager;
        sceneLoader = GameManager.Instance.sceneLoader;
    }

    //Called when creating a room
    public void Host()
    {
        if(!CheckRoomNameAndPasswordValidity())
        {
            return;
        }
        networkConnectionManager.SetRoomName();
        sceneLoader.LoadScene(scene, true);
    }

    //Called when clients attempt to join a room
    public void Join()
    {
        if (!CheckRoomNameAndPasswordValidity())
        {
            return;
        }
        networkConnectionManager.SetRoomName();
        sceneLoader.LoadScene(scene, false);
    }

    //Check if room name and room password fields are empty
    private bool CheckRoomNameAndPasswordValidity()
    {
        if (networkConnectionManager.roomName == "" || networkConnectionManager.roomPassword == "")
        {
            Debug.Log("Please Enter a Valid Room Name and Room Password");
            return false;
        }
        else
        {
            return true;
        }
    }

    //Main menu ui functions
    public void RoomNameChanged(string newRoomName)
    {
        networkConnectionManager.roomName = newRoomName;
    }

    public void RoomPasswordChanged(string newRoomPassword)
    {
        networkConnectionManager.roomPassword = newRoomPassword;
    }
}
