//Made by: Mathias Sorin
//Last updated: 28/05/2021

using UnityEngine;
using MLAPI.SceneManagement;
using MLAPI.Messaging;

//Switch scene WIP
public class SceneSwitch : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private void Awake()
    {
        if (string.IsNullOrWhiteSpace(sceneName))
        {
            Debug.LogWarningFormat("{0} error string", name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        SwitchSceneServerRpc();
    }

    [ServerRpc]
    private void SwitchSceneServerRpc()
    {
        NetworkSceneManager.OnSceneSwitched += TeleportPlayerClientRpc;
        NetworkSceneManager.SwitchScene(sceneName);
    }

    [ClientRpc]
    private void TeleportPlayerClientRpc()
    {
        GameManager.Instance.localPlayer.transform.position = new Vector3(0, 5, 0);
    }
}
