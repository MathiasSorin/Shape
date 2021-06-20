//Made by: Mathias Sorin
//Last updated: 17/06/2021

using UnityEngine;

public class SO_Ability : ScriptableObject
{
    //Ability VFX
    public GameObject abilityVFX;

    //Ability Icon
    public Sprite abilityIcon;

    //Cooldown
    public float cooldown;

    //Ability slot
    public int abilitySlot;

    //Ability charges
    public int chargesMax = 1;
    public int chargesCurrent = 1;

    //Player variables
    protected PlayerClass playerClass;
    protected Transform playerTransform;
    protected GameObject playerTarget;
    protected PlayerUI playerUI;

    public virtual void CastAbility()
    {
    }

    public virtual void ToggleAbility()
    {
    }

    //Start cooldown of ability
    protected void StartCooldown()
    {
        playerClass.StartCooldown(cooldown, abilitySlot, this);
    }

    //Get Player Variables
    protected void GetPlayerVariables()
    {
        playerClass = GameManager.Instance.localPlayer.GetComponent<PlayerClass>();
        playerTransform = GameManager.Instance.localPlayer.transform;
        playerTarget = playerClass.target;
    }
}
