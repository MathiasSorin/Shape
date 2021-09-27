//Made by: Mathias Sorin
//Last updated: 26/06/2021

using UnityEngine;

//This ability will heal yourself
[CreateAssetMenu(fileName = "SO_DealerAbility4", menuName = "Ability/Player/Dealer/SO_DealerAbility4")]
public class SO_DealerAbility4 : SO_Ability
{
    //Ability variables
    [Header("This ability will heal yourself")]
    public float heal = 8;

    public override void CastAbility()
    {
        GetPlayerVariables();

        //Ability Start
        GameManager.Instance.abilityCatalog.HealPlayerSelf(playerClass, heal);
        //Ability End

        StartCooldown();
    }
}