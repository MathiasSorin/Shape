//Made by: Mathias Sorin
//Last updated: 27/05/2021
using UnityEngine;

//This ability will heal the target if friendly, damage and aggro if foe
[CreateAssetMenu(fileName = "SO_DealerAbility1", menuName = "Ability/Player/Dealer/SO_DealerAbility1")]
public class SO_DealerAbility1 : SO_Ability
{
    //Ability variables
    [Header("This ability will heal the target if friendly, damage and aggro if foe")]
    public float heal = 5;
    public float damage = 2;
    public float aggro = 0;
    public float range = 20;

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
        GameManager.Instance.abilityCatalog.HealPlayerOrDamageAndAggroEnemyTarget(playerTarget, heal, damage, aggro, playerClass);
        //Ability End

        StartCooldown();
    }
}