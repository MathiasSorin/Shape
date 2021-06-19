//Made by: Mathias Sorin
//Last updated: 28/05/2021

using UnityEngine;
using MLAPI;

//Contains all in game abilities player/enemies 
public class AbilityCatalog : NetworkBehaviour
{
    #region Enemy Abilities
    public void TableHeadAbility1(Transform transform)
    {
        Vector3 extent = new Vector3(2f, 3f, 3f);
        Collider[] colliders = Physics.OverlapBox(transform.position + (transform.forward * 2f), extent, Quaternion.identity);
        foreach (Collider collider in colliders)
        {
            IDamageablePlayer player =  collider.gameObject.GetComponent<IDamageablePlayer>();
            if (player != null)
            {
                player.GetDamaged(5);
            }
        }
    }
    #endregion

    #region Generic Ability Functions
    public void ShieldPlayerSelf(PlayerClass player, int shieldCharges)
    {
        player.shieldCharges += shieldCharges;
    }

    public void HealPlayerSelf(PlayerClass player, float heal)
    {
        player.GetHealed(heal);
    }

    public void HealPlayerTarget(GameObject target, float heal)
    {
        IHealablePlayer healablePlayer = target.GetComponent<IHealablePlayer>();
        if (healablePlayer != null)
        {
            healablePlayer.GetHealed(heal);
        }
    }

    public void HealPlayerInSphere(float heal, float radius, Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, radius);
        foreach (Collider collider in colliders)
        {
            IHealablePlayer healablePlayer = collider.GetComponent<IHealablePlayer>();
            if (healablePlayer != null)
            {
                healablePlayer.GetHealed(heal);
            }
        }
    }

    public void DamagePlayerSelf(PlayerClass player, float damage)
    {
        player.GetDamaged(damage);
    }

    public void DamageAndAggroEnemyTarget(GameObject target, float damage, float aggro, PlayerClass agressor)
    {
        IDamageableEnemy damageableEnemy = target.GetComponent<IDamageableEnemy>();
        if (damageableEnemy != null)
        {
            damageableEnemy.GetDamaged(damage);
        }
        IAggroableEnemy aggroableEnemy = target.GetComponent<IAggroableEnemy>();
        if (aggroableEnemy != null)
        {
            aggroableEnemy.GetAggroed(aggro, agressor);
        }
    }

    public void DamageAndAggroEnemyInSphere(float damage, float aggro, PlayerClass agressor, float radius, Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, radius);
        foreach (Collider collider in colliders)
        {
            IDamageableEnemy damageableEnemy = collider.GetComponent<IDamageableEnemy>();
            if (damageableEnemy != null)
            {
                damageableEnemy.GetDamaged(damage);
            }
            IAggroableEnemy aggroableEnemy = collider.GetComponent<IAggroableEnemy>();
            if (aggroableEnemy != null)
            {
                aggroableEnemy.GetAggroed(aggro, agressor);
            }
        }
    }

    public void HealPlayerOrDamageAndAggroEnemyTarget(GameObject target, float heal, float damage, float aggro, PlayerClass agressor)
    {
        IHealablePlayer healablePlayer = target.GetComponent<IHealablePlayer>();
        IDamageableEnemy damageableEnemy = target.GetComponent<IDamageableEnemy>();
        if (healablePlayer != null)
        {
            healablePlayer.GetHealed(heal);
        }
        else if (damageableEnemy != null)
        {
            IAggroableEnemy aggroableEnemy = target.GetComponent<IAggroableEnemy>();
            damageableEnemy.GetDamaged(damage);
            aggroableEnemy.GetAggroed(aggro, agressor);
        }
    }

    public bool RangeCheck(Vector3 position1, Vector3 position2, float range)
    {
        if (Vector3.Distance(position1, position2) <= range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ToggleFunctionOff(PlayerClass playerClass)
    {
        playerClass.toggleFunction = null;
    }
    #endregion
}
