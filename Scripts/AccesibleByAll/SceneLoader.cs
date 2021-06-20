//Made by: Mathias Sorin
//Last updated: 19/06/2021

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    //Game Manager components reference
    private NetworkConnectionManager networkConnectionManager;

    //Scene to load after menu, loading progress
    private AsyncOperation loadingOperation;

    private void Start()
    {
        networkConnectionManager = GameManager.Instance.networkConnectionManager;
    }

    public void LoadScene(string scene, bool isHost)
    {
        loadingOperation = SceneManager.LoadSceneAsync(scene);
        StartCoroutine(StartLoad(isHost));
    }

    IEnumerator StartLoad(bool isHost)
    {
        while (!loadingOperation.isDone)
        {
            yield return null;
        }
        networkConnectionManager.SetSpawnPosition(GameObject.FindGameObjectWithTag("Spawn").transform.position);
        EndLoad(isHost);
    }

    private void EndLoad(bool isHost)
    {
        if (isHost)
        {
            networkConnectionManager.StartHost();
        }
        else
        {
            networkConnectionManager.StartClient();
        }
    }
}
