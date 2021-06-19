//Made by: Mathias Sorin
//Last updated: 26/05/2021

/*using System.Collections;
using UnityEngine;
using MLAPI.NetworkVariable;

public class PlayerClassX : PlayerClass
{
    #region Unity Callbacks
    private void Awake()
    {
        networkHealth = new NetworkVariableInt(permissionEveryone, health);
    }

    private void Update()
    {
        health = networkHealth.Value;
        //Check if toggleable ability is active
        if (toggleFunction == null)
        {
            return;
        }
        else
        {
            toggleFunction();
        }
    }
    #endregion

    #region Class Abilities
    public override void Ability1()
    {
    }
    public override void Ability2()
    {
    }
    public override void Ability3()
    {
    }
    public override void Ability4()
    {
    }
    public override void Ability5()
    {
    }
    #endregion

    #region Get Damaged/Healed/Targeted
    public override void GetDamaged(int damage)
    {
        networkHealth.Value = networkHealth.Value - damage;
    }

    public override void GetHealed(int heal)
    {
        networkHealth.Value = networkHealth.Value + heal;
        GameManager.Instance.floatingTextDisplay.DisplayFloatingText(heal.ToString(), transform, 2);
    }

    public override void GetTargeted(bool on)
    {
        targetRing.SetActive(on);
    }
    #endregion
}*/