//Made by: Mathias Sorin
//Last updated: 27/05/2021
using UnityEngine;

//This ability will TBD
[CreateAssetMenu(fileName = "SO_DealerAbility2", menuName = "Ability/Player/Dealer/SO_DealerAbility2")]
public class SO_DealerAbility2 : SO_Ability
{
    //Ability variables
    public float damage = 3;
    public float aggro = 0;
    public float range = 10;

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
