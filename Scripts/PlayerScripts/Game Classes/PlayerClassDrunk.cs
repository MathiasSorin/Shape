//Made by: Mathias Sorin
//Last updated: 11/05/2021

using MLAPI.NetworkVariable;

//In game class "Drunk"
public class PlayerClassDrunk : PlayerClass
{
    #region Unity Callbacks
    private void Awake()
    {
        networkHealth = new NetworkVariableFloat(permissionEveryone, health);
    }

    private void Start()
    {
        InitializeHealthUI();
        InitializeAbilityUI();
    }

    private void Update()
    {
        UpdateLocalVariables();
        UpdateHealthUI();

        //Check if toggleable ability is active
        if (toggleFunction == null)
        {
            return;
        }
        else
        {
            toggleFunction();
        }
    }
    #endregion

    #region Class Abilities
    public override void Ability1()
    {
        if (!abilitiesCanCast[abilities[0].abilitySlot])
        {
            return;
        }
        abilities[0].CastAbility();
    }
    public override void Ability2()
    {
        if (!abilitiesCanCast[abilities[1].abilitySlot])
        {
            return;
        }
        abilities[1].CastAbility();
    }
    public override void Ability3()
    {
        if (!abilitiesCanCast[abilities[2].abilitySlot])
        {
            return;
        }
        abilities[2].CastAbility();
    }
    public override void Ability4()
    {
        if (!abilitiesCanCast[abilities[3].abilitySlot])
        {
            return;
        }
        abilities[3].CastAbility();
    }
    public override void Ability5()
    {
        if (!abilitiesCanCast[abilities[4].abilitySlot])
        {
            return;
        }
        abilities[4].CastAbility();
    }
    #endregion

    #region Get Damaged/Healed/Targeted
    public override void GetDamaged(float damage)
    {
        base.GetDamaged(damage);
    }

    public override void GetHealed(float heal)
    {
        base.GetHealed(heal);
    }

    public override void GetTargeted(bool on)
    {
        base.GetTargeted(on);
    }
    #endregion
}