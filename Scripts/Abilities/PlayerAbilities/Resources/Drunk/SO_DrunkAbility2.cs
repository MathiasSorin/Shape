//Made by: Mathias Sorin
//Last updated: 27/05/2021
using UnityEngine;

//This ability will grant x shieldCharges (shieldCharges = ignore next attacks)
[CreateAssetMenu(fileName = "SO_DrunkAbility2", menuName = "Ability/Player/Drunk/SO_DrunkAbility2")]
public class SO_DrunkAbility2 : SO_Ability
{
    [Header("Grant x shieldCharges")]
    //Ability variables
    public int shieldCharges = 1;

    public override void CastAbility()
    {
        GetPlayerVariables();

        //Ability Start
        GameManager.Instance.abilityCatalog.ShieldPlayerSelf(playerClass, shieldCharges);
        //Ability End

        StartCooldown();
    }
}
