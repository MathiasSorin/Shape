//Made by: Mathias Sorin
//Last updated: 08/05/2021

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;

public class EnemyMain : NetworkBehaviour, IDamageableEnemy, IAggroableEnemy, IHealableEnemy, ITargetable, IAlertable
{
    //Network variables
    protected NetworkVariableSettings permissionEveryone = new NetworkVariableSettings { WritePermission = NetworkVariablePermission.Everyone };
    protected NetworkVariableFloat networkHealth;

    [Header("Health Variables")]
    public float health = 100f;
    public float healthMax = 100f;

    [Header("Health UI")]
    public GameObject healthBarUI;
    public Slider healthBarSlider;

    //Enemy current target
    public NetworkObject target = null;

    //Aggro dictionary
    public Dictionary<ulong, float> aggroDict = new Dictionary<ulong, float>();

    //Target ring activated when being targeted
    public GameObject targetRing;

    //Alert radius (alert other enemies of player presence)
    public float alertRadius = 10f;

    private void Awake()
    {
        networkHealth = new NetworkVariableFloat(permissionEveryone, health);
    }

    public virtual void GetAggroed(float aggro, PlayerClass agressor)
    {
        NetworkObject networkObject = agressor.GetComponent<NetworkObject>();
        if (target == null)
        {
            Alert(networkObject);
        }
        AggroPriorityServerRpc(aggro, networkObject.NetworkObjectId);
    }

    public virtual void GetDamaged(float damage)
    {
        networkHealth.Value = networkHealth.Value - damage;
        GameManager.Instance.floatingTextDisplay.DisplayFloatingNumber(damage, transform, 3.5f, Color.red);
        if (networkHealth.Value <= 0)
        {
            DieServerRpc();
        }
    }

    public virtual void GetHealed(float heal)
    {
    }

    public virtual void GetTargeted(bool on)
    {
        healthBarUI.SetActive(on);
        targetRing.SetActive(on);
    }

    public void GetAlerted(NetworkObject newTarget)
    {
        if (target == null)
        {
            target = newTarget;
        }
    }

    protected void UpdateLocalVariables()
    {
        health = networkHealth.Value;
    }

    protected void InitializeHealthUI()
    {
        healthBarSlider.value = health;
    }

    protected void CalculateHealthUI()
    {
        healthBarSlider.value = health /healthMax;
    }

    [ServerRpc(RequireOwnership = false)]
    protected void DieServerRpc()
    {
        Destroy(gameObject);
    }

    public void Alert(NetworkObject networkAgressor)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, alertRadius);
        foreach (Collider collider in colliders)
        {
            IAlertable alertable = collider.GetComponent<IAlertable>();
            if (alertable != null)
            {
                alertable.GetAlerted(networkAgressor);
            }
        }
    }

    //Manages target priority
    [ServerRpc(RequireOwnership = false)]
    protected void AggroPriorityServerRpc(float aggro, ulong agressor)
    {
        if (aggroDict.ContainsKey(agressor))
        {
            aggroDict[agressor] += aggro;
        }
        else
        {
            aggroDict.Add(agressor, aggro);
            target = GetNetworkObject(agressor);
        }
        if (aggroDict[agressor] >= aggroDict[target.NetworkObjectId])
        {
            target = GetNetworkObject(agressor);
        }
    }
}
