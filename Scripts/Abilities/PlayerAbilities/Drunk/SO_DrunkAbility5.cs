//Made by: Mathias Sorin
//Last updated: 20/06/2021

using UnityEngine;

//Drain drinking bar deal damage and aggro every x second in a cone in front of you
[CreateAssetMenu(fileName = "SO_DrunkAbility5", menuName = "Ability/Player/Drunk/SO_DrunkAbility5")]
public class SO_DrunkAbility5 : SO_Ability
{
    [Header("Drain vomit bar deal damage and aggro every x second in a cone in front of you")]
    //Ability variables
    public float drain = 5f;
    public float interval = 1f;
    public float damage = 1f;
    public float aggro = 2f;

    public override void CastAbility()
    {
        GetPlayerVariables();

        //Ability Start
        
        //Ability End

        StartCooldown();
    }
}
