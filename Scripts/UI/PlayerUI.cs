//Made by: Mathias Sorin
//Last updated: 19/06/2021

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    //Image icon slots
    public Image[] abilityImages;

    //List of abilities currently in cooldown
    readonly List<SO_Ability> abilitiesInCooldown = new List<SO_Ability>();

    //Update ability ui
    private void Update()
    {
        if (abilitiesInCooldown.Count == 0)
        {
            return;
        }
        UpdateAbilityUICooldown();
    }

    //Init icons for the player ability UI
    public void InitializeAbilityUI(PlayerClass playerClass)
    {
        for (int i = 0; i < abilityImages.Length; ++i)
        {
            //Check if no icon is assigned to said ability
            if (!playerClass.abilities[i].abilityIcon)
            {
                Debug.LogWarningFormat("No icon was found for ability {0}", playerClass.abilities[i]);
                continue;
            }
            abilityImages[i].sprite = playerClass.abilities[i].abilityIcon;
            UpdateAbilityUICharges(abilityImages[i], playerClass.abilities[i]);
        }
    }

    //All abilities that are in cooldown get their icon on the UI updated
    private void UpdateAbilityUICooldown()
    {
        for (int i = 0; i < abilitiesInCooldown.Count; ++i)
        {
            Image abilityImage = abilityImages[abilitiesInCooldown[i].abilitySlot];
            abilityImage.fillAmount += 1.0f / abilitiesInCooldown[i].cooldown * Time.deltaTime;
            UpdateAbilityUICharges(abilityImages[abilitiesInCooldown[i].abilitySlot], abilitiesInCooldown[i]);
            if (abilityImage.fillAmount >= 1.0f)
            {
                abilityImage.fillAmount = 1.0f;
                UpdateAbilityUICharges(abilityImages[abilitiesInCooldown[i].abilitySlot], abilitiesInCooldown[i], true);
                abilitiesInCooldown.Remove(abilitiesInCooldown[i]);
            }
        }
    }

    //Start cooldown for ability ui if already in list restart cooldown
    public void StartAbilityUICooldown(SO_Ability ability)
    {
        if (abilitiesInCooldown.Contains(ability))
        {
            abilitiesInCooldown.Remove(ability);
        }
        Image image = abilityImages[ability.abilitySlot];
        image.fillAmount = 0.0f;
        abilitiesInCooldown.Add(ability);
    }

    //Update ability ui charge
    private void UpdateAbilityUICharges(Image image, SO_Ability ability, bool max = false)
    {
        if (ability.chargesMax <= 1)
        {
            return;
        }
        if (max)
        {
            image.GetComponentInChildren<TextMeshProUGUI>().text = ability.chargesMax.ToString();
            return;
        }
        image.GetComponentInChildren<TextMeshProUGUI>().text = ability.chargesCurrent.ToString();
    }
}
