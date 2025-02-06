using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : Singleton<AbilityManager>
{
    public delegate void AbilityHolderDelegate();
    public static event AbilityHolderDelegate abilityReadyDelegete;
    public static event AbilityHolderDelegate abilityActiveDelegete;
    public static event AbilityHolderDelegate abilityCooldownDelegete;

    public Character character;

    public void Init(Character character)
    {
        this.character = character;
    }

    private void FixedUpdate()
    {
        AbilitySystemRun();
    }

    private void AbilitySystemRun()
    {
        if (abilityReadyDelegete != null) abilityReadyDelegete();
        if (abilityActiveDelegete != null) abilityActiveDelegete();
        if (abilityCooldownDelegete != null) abilityCooldownDelegete();
    }
}