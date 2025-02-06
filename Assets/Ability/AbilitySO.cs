using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility", menuName = "Ability")]
public class AbilitySO : ScriptableObject
{
    public Sprite skillSprite;

    public string nameAbility;

    public float cooldownTime;

    public float durationTime;

    public string keyBoard;

    public AbilityType AbilityType;

    public int abilityHpAmount;

    public int abilityDamage;

    public int abilityRadiusDamage;

    public float abilitySpeedAmount;

    public float abilityRadiusCC;
}