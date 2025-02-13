//Made by: Mathias Sorin
//Last updated: 26/06/2021
using UnityEngine;
using MLAPI;

//This ability will inflict damage to yourself to heal an ally
[CreateAssetMenu(fileName = "SO_DealerAbility3", menuName = "Ability/Player/Dealer/SO_DealerAbility3")]
public class SO_DealerAbility3 : SO_Ability
{
    //Ability variables
    [Header("This ability will inflict damage to yourself to heal an ally")]
    public float heal = 10;
    public float damage = 5;

    public override void CastAbility()
    {
        GetPlayerVariables();

        //Ability Start
        if (!playerTarget)
        {
            return;
        }
        IHealablePlayer healablePlayer = playerTarget.GetComponent<IHealablePlayer>();
        if (healablePlayer == null)
        {
            return;
        }
        GameManager.Instance.abilityCatalog.DamagePlayerSelf(playerClass, damage);
        GameManager.Instance.abilityCatalog.HealPlayerTarget(playerTarget.gameObject, heal);
        //Ability End

        StartCooldown();
    }
}