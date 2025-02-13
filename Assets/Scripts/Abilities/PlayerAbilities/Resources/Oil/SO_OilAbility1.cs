//Made by: Mathias Sorin
//Last updated: 27/05/2021

using UnityEngine;

//This ability will damage and aggro the target
[CreateAssetMenu(fileName = "SO_OilAbility1", menuName = "Ability/Player/Oil/SO_OilAbility1")]
public class SO_OilAbility1 : SO_Ability
{
    [Header("Damage and aggro target")]
    //Ability variables
    public float damage = 5;
    public float aggro = 5;
    public float range = 3;

    public override void CastAbility()
    {
        GetPlayerVariables();

        //Ability Start
        if (!playerTarget)
        {
            return;
        }
        if (!GameManager.Instance.abilityCatalog.RangeCheck(playerTransform.position, playerTarget.transform.position, range))
        {
            return;
        }
        GameManager.Instance.abilityCatalog.DamageAndAggroEnemyTarget(playerTarget, damage, aggro, playerClass);
        //Ability End

        StartCooldown();
    }
}
