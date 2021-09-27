//Made by: Mathias Sorin
//Last updated: 27/05/2021

using UnityEngine;
using MLAPI;

public interface IDamageableEnemy
{
    void GetDamaged(float damage);
}
public interface IAggroableEnemy
{
    void GetAggroed(float aggro, PlayerClass agressor);
}
public interface IHealableEnemy
{
    void GetHealed(float heal);
}
public interface IDamageablePlayer
{
    void GetDamaged(float damage);
}
public interface IHealablePlayer
{
    void GetHealed(float heal);
}
public interface ITargetable
{
    void GetTargeted(bool on);
}

public interface IInteractable
{
    void GetActivated();
}

public interface IAlertable
{
    void GetAlerted(NetworkObject newTarget);
}
