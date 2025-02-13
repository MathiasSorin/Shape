//Made by: Mathias Sorin
//Last updated: 27/05/2021

using UnityEngine;

//This ability will damage and aggro the target
[CreateAssetMenu(fileName = "SO_DrunkAbility1", menuName = "Ability/Player/Drunk/SO_DrunkAbility1")]
public class SO_DrunkAbility1 : SO_Ability
{
    [Header("Damage and aggro target")]
    //Ability variables
    public float damage = 5f;
    public float aggro = 5f;
    public float range = 3f;
    public float angle = 45f;

    public override void CastAbility()
    {
        GetPlayerVariables();

        //Ability Start
        GameManager.Instance.abilityCatalog.DamageAndAggroEnemyInCone(damage, aggro, playerClass, playerTransform.position, playerTransform.forward, range, angle);
        //Ability End

        StartCooldown();
    }
}
