//Made by: Mathias Sorin
//Last updated: 24/06/2021
using UnityEngine;
using UnityEngine.InputSystem;

//This ability will heal players in a radius
[CreateAssetMenu(fileName = "SO_DealerAbility2", menuName = "Ability/Player/Dealer/SO_DealerAbility2")]
public class SO_DealerAbility2 : SO_Ability
{
    //Ability variables
    [Header("This ability will heal players in a radius")]
    public float heal = 5;
    public float radius = 3;
    public float range = 10;
    public LayerMask ignoredLayer;

    public override void CastAbility()
    {
        GetPlayerVariables();

        //Ability Start
        if (tempAbilityUI != null)
        {
            ToggleFunctionOff();
            return;
        }
        tempAbilityUI = Instantiate(abilityUI, playerTransform);
        playerClass.toggleAbility = this;
        tempAbilityUI.transform.localScale = new Vector3(radius * 2, radius * 2, 1);
        //Ability End
    }

    public override void ToggleAbility()
    {
        Ray ray = GameManager.Instance.playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, range, ~ignoredLayer))
        {
            tempAbilityUI.transform.position = new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z);
        }
    }

    public override void ConfirmAbility()
    {
        GameManager.Instance.abilityCatalog.HealPlayerInSphere(heal, radius, tempAbilityUI.transform.position);
        GameManager.Instance.objectSpawning.SpawnObjectOnAllClientsServerRpc(this.name, tempAbilityUI.transform.position);
        StartCooldown();
        ToggleFunctionOff();
    }
}
