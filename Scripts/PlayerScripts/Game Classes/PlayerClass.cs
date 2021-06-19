//Made by: Mathias Sorin
//Last updated: 17/06/2021

using UnityEngine;
using UnityEngine.UI;
using MLAPI;
using MLAPI.NetworkVariable;
using System.Collections;

//Parent class for all in game player classes
public class PlayerClass : NetworkBehaviour, IDamageablePlayer, IHealablePlayer, ITargetable
{
    //Network variables
    protected NetworkVariableSettings permissionEveryone = new NetworkVariableSettings { WritePermission = NetworkVariablePermission.Everyone };
    protected NetworkVariableFloat networkHealth;

    //Delegated variable to assign toggleable ability function
    public delegate void Delegate();
    public Delegate toggleFunction = null;

    //In game class wide variables
    [Header("Health Variables")]
    public float health = 100f;
    [SerializeField, Range(1f, 1000f)] protected float healthMax = 100f;

    [Header("Health UI elements")]
    //Internal UI elements (seen by you)
    public Slider intHealthBarSlider;
    //External UI elements (seen by others)
    public GameObject extHealthBarUI;
    public Slider extHealthBarSlider;

    //Player current target
    public GameObject target;
    //Player UI objects
    public GameObject targetRing;

    //Buffs
    public float damageReduction = 0;
    public int shieldCharges = 0;

    //Abilities
    public SO_Ability ability1;
    public SO_Ability ability2;
    public SO_Ability ability3;
    public SO_Ability ability4;
    public SO_Ability ability5;

    //Abilities Cooldown (these variables are not in the abilites SO since you can't run a coroutine in them + they don't reset after play)
    protected bool ability1CanCast = true;
    protected bool ability2CanCast = true;
    protected bool ability3CanCast = true;
    protected bool ability4CanCast = true;
    protected bool ability5CanCast = true;

    #region Virtual Functions
    public virtual void Ability1()
    {
    }

    public virtual void Ability2()
    {
    }

    public virtual void Ability3()
    {
    }

    public virtual void Ability4()
    {
    }

    public virtual void Ability5()
    {
    }

    public virtual void GetDamaged(float damage)
    {
        if (shieldCharges > 0)
        {
            shieldCharges -= 1;
            return;
        }
        damage -= damageReduction/100*damage;
        networkHealth.Value = networkHealth.Value - damage;
    }

    public virtual void GetHealed(float heal)
    {
        GameManager.Instance.floatingTextDisplay.DisplayFloatingNumber(heal, transform, 2, Color.green);
        networkHealth.Value = networkHealth.Value + heal;
    }

    public virtual void GetTargeted(bool on)
    {
        extHealthBarUI.SetActive(on);
        targetRing.SetActive(on);
    }
    #endregion

    protected void InitializeHealthUI()
    {
        intHealthBarSlider.value = health;
        extHealthBarSlider.value = health;
    }

    protected void InitializeAbilityUI()
    {
        //WIP
    }

    protected void UpdateHealthUI()
    {
        intHealthBarSlider.value = health/healthMax;
        extHealthBarSlider.value = health/healthMax;
    }

    protected void UpdateAbilityUI()
    {
        //WIP
    }

    protected void UpdateLocalVariables()
    {
        health = networkHealth.Value;
    }

    public void StartCooldown(float abilityCooldown, int ability)
    {
        switch (ability)
        {
            case 1:
                ability1CanCast = false;
                break;
            case 2:
                ability2CanCast = false;
                break;
            case 3:
                ability3CanCast = false;
                break;
            case 4:
                ability4CanCast = false;
                break;
            case 5:
                ability5CanCast = false;
                break;
            default:
                return;
        }
        StartCoroutine(WaitForCooldown(abilityCooldown, ability));
    }

    public void ResetCooldown(int ability)
    {
        switch (ability)
        {
            case 1:
                ability1CanCast = true;
                return;
            case 2:
                ability2CanCast = true;
                return;
            case 3:
                ability3CanCast = true;
                return;
            case 4:
                ability4CanCast = true;
                return;
            case 5:
                ability5CanCast = true;
                return;
            default:
                return;
        }
    }

    protected IEnumerator WaitForCooldown(float abilityCooldown, int ability)
    {
        yield return new WaitForSeconds(abilityCooldown);
        ResetCooldown(ability);
    }
}