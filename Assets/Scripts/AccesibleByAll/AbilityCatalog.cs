//Made by: Mathias Sorin
//Last updated: 28/05/2021

using UnityEngine;
using MLAPI;
using System.Collections;
using System.Collections.Generic;

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

    public void DamageModifierPlayerSelf(PlayerClass player, float damageReduction, float duration, bool reset)
    {
        player.damageModifier += damageReduction;
        if (reset)
        {
            return;
        }
        StartCoroutine(ReduceDamageModifierAfterTime(player, damageReduction, duration));
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

    public void DamageAndAggroEnemyInCone(float damage, float aggro, PlayerClass agressor, Vector3 position, Vector3 direction, float radius, float angle)
    {
        foreach(Collider collider in GetCollidersInCone(position, direction, radius, angle))
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

    //Returns list of colliders in cone
    public List<Collider> GetCollidersInCone(Vector3 position, Vector3 direction, float radius, float angle)
    {
        List<Collider> inCone = new List<Collider>();
        //Get all colliders in sphere add layer mask
        Collider[] colliders = Physics.OverlapSphere(position, radius);
        foreach (Collider collider in colliders)
        {
            //Get direction of collider compared to player
            Vector3 targetDirection = (collider.transform.position - position).normalized;
            //Compare the Dot product (compare 2 directions to check how close they are from facing same direction)
            //Cos returns a value between 1 & -1 we can compare those 2 value to check if the dot product is inside the wanted angle
            float dot = Vector3.Dot(direction, targetDirection);
            if (dot >= 0.0f && dot >= Mathf.Cos(angle * Mathf.Deg2Rad))
            {
                inCone.Add(collider);
            }
        }
        return inCone;
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

    public IEnumerator ReduceDamageModifierAfterTime(PlayerClass player, float damageReduction, float duration)
    {
        yield return new WaitForSeconds(duration);
        DamageModifierPlayerSelf(player, -damageReduction, duration, true);
    }
    #endregion
}
