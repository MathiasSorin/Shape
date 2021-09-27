//Made by: Mathias Sorin
//Last updated: 20/06/2021

using UnityEngine;

//Grant damage reduction during x seconds
[CreateAssetMenu(fileName = "SO_DrunkAbility4", menuName = "Ability/Player/Drunk/SO_DrunkAbility4")]
public class SO_DrunkAbility4 : SO_Ability
{
    [Header("Grant damage reduction during x seconds")]
    //Ability variables
    public float duration = 5f;
    public float damageReduction = 50f;

    public override void CastAbility()
    {
        GetPlayerVariables();

        //Ability Start
        GameManager.Instance.abilityCatalog.DamageModifierPlayerSelf(playerClass, damageReduction, duration, false);
        //Ability End

        StartCooldown();
    }
}
