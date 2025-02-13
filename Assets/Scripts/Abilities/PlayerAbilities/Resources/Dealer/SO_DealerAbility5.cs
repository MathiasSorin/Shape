//Made by: Mathias Sorin
//Last updated: 03/07/2021

using UnityEngine;

//This ability will deal heavy damage to an enemy
[CreateAssetMenu(fileName = "SO_DealerAbility5", menuName = "Ability/Player/Dealer/SO_DealerAbility5")]
public class SO_DealerAbility5 : SO_Ability
{
    //Ability variables
    [Header("This ability will deal heavy damage to an enemy")]
    public float damage = 15;
    public float aggro = 10;

    public override void CastAbility()
    {
        GetPlayerVariables();

        //Ability Start
        GameManager.Instance.abilityCatalog.DamageAndAggroEnemyTarget(playerTarget, damage, aggro, playerClass);
        //Ability End

        StartCooldown();
    }
}