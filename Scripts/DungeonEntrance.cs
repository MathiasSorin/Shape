//Made by: Mathias Sorin
//Last updated: 28/05/2021

using UnityEngine;
using MLAPI.SceneManagement;
using MLAPI.Messaging;

//Switch scene
public class DungeonEntrance : MonoBehaviour
{
    public string scene;


    private void OnTriggerEnter(Collider other)
    {
        SwitchSceneServerRpc();
    }

    [ServerRpc]
    private void SwitchSceneServerRpc()
    {
        NetworkSceneManager.OnSceneSwitched += TeleportPlayerClientRpc;
        NetworkSceneManager.SwitchScene(scene);
        
    }

    [ClientRpc]
    private void TeleportPlayerClientRpc()
    {
        GameObject spawn = GameObject.FindGameObjectWithTag("Spawn");
        GameManager.Instance.localPlayer.transform.position = spawn.transform.position;
    }
}
