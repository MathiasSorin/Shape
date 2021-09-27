//Made by: Mathias Sorin
//Last updated: 25/06/2021

using MLAPI.Messaging;
using MLAPI;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ObjectSpawning : NetworkBehaviour
{
    //Dict containing all of the vfx from the SO that inherit from SO_Ability class
    private Dictionary<string, GameObject> vfxGameObjects = new Dictionary<string, GameObject>();

    private void Awake()
    {
        InitializeVfxDictionary();
    }

    //FIX THIS
    private void Start()
    {
        GameManager.Instance.objectSpawning = this;
    }

    //Called when wanting to spawn object on all clients
    [ServerRpc(RequireOwnership = false)]
    public void SpawnObjectOnAllClientsServerRpc(string SO, Vector3 position)
    {
        SpawnObjectClientRpc(SO, position);
    }

    //Spawn object for Client
    [ClientRpc]
    private void SpawnObjectClientRpc(string SO, Vector3 position)
    {

        Instantiate(vfxGameObjects[SO], position, Quaternion.identity);
    }

    //Get all SO_abilities, put their abilityVFX in dictionary
    public void InitializeVfxDictionary()
    {
        SO_Ability[] abilities = Resources.LoadAll("", typeof(SO_Ability)).Cast<SO_Ability>().ToArray();
        foreach (SO_Ability ability in abilities)
        {
            if (ability.abilityVFX != null)
            {
                vfxGameObjects.Add(ability.name, ability.abilityVFX);
            }
        }
    }
}
