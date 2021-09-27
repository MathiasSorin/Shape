//Made by: Mathias Sorin
//Last updated: 20/06/2021

using UnityEngine;

//Drain drinking bar deal damage and aggro every x second in a cone in front of you
[CreateAssetMenu(fileName = "SO_DrunkAbility5", menuName = "Ability/Player/Drunk/SO_DrunkAbility5")]
public class SO_DrunkAbility5 : SO_Ability
{
    [Header("Deal damage and aggro in a cone in front of you")]
    //Ability variables
    public float damage = 5f;
    public float aggro = 5f;
    public float range = 10f;
    public float angle = 60f;

    public override void CastAbility()
    {
        GetPlayerVariables();

        //Ability Start
        GameManager.Instance.abilityCatalog.DamageAndAggroEnemyInCone(damage, aggro, playerClass, playerTransform.position, playerTransform.forward, range, angle);
        //Ability End

        StartCooldown();
    }
}
