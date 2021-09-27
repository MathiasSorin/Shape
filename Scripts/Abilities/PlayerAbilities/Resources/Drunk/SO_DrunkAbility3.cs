//Made by: Mathias Sorin
//Last updated: 27/05/2021

using UnityEngine;
using UnityEngine.InputSystem;

//Toggle target an area damage and aggro enemies in range
[CreateAssetMenu(fileName = "SO_DrunkAbility3", menuName = "Ability/Player/Drunk/SO_DrunkAbility3")]
public class SO_DrunkAbility3 : SO_Ability
{
    [Header("Toggle target an area damage and aggro enemies in range")]
    //Ability variables
    public float damage = 5;
    public float aggro = 5;
    public float radius = 5;
    public float range = 50;
    public LayerMask ignoredLayer;

    //Ability UI
    public GameObject abilityUI;

    //Instantiated abilityUI
    private GameObject tempAbilityUI;

    public override void CastAbility()
    {
        GetPlayerVariables();

        //Ability Start
        if (tempAbilityUI != null)
        {
            GameManager.Instance.abilityCatalog.ToggleFunctionOff(playerClass);
            Destroy(tempAbilityUI);
            return;
        }
        tempAbilityUI = Instantiate(abilityUI, playerTransform);
        playerClass.toggleFunction = ToggleAbility;
        tempAbilityUI.transform.localScale = new Vector3(radius*2, radius*2, 1);
        //Ability End
    }

    public override void ToggleAbility()
    {
        Ray ray = GameManager.Instance.playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, range, ~ignoredLayer))
        {
            tempAbilityUI.transform.position = new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z);
            if (GameManager.Instance.mouseLeftClick)
            {
                GameManager.Instance.abilityCatalog.DamageAndAggroEnemyInSphere(damage, aggro, playerClass, radius, tempAbilityUI.transform.position);
                GameManager.Instance.objectSpawning.SpawnObjectOnAllClientsServerRpc(this.name, tempAbilityUI.transform.position);
                StartCooldown();
                Destroy(tempAbilityUI);
                GameManager.Instance.abilityCatalog.ToggleFunctionOff(playerClass);
            }
        }
    }
}
