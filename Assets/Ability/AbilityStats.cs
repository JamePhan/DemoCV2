public class AbilityStats 
{
    public float cooldownTime;
    public float durationTime;
    public AbilityType AbilityType;
    public int hpAmount;
    public int damage;
    public int radiusDamage;
    public float speedAmount;
    public float radiusCC;

    public AbilityStats(AbilitySO scriptableObject)
    {
        this.cooldownTime = scriptableObject.cooldownTime;
        this.durationTime = scriptableObject.durationTime;
        this.AbilityType = scriptableObject.AbilityType;
        this.hpAmount = scriptableObject.abilityHpAmount;
        this.damage = scriptableObject.abilityDamage;
        this.radiusDamage = scriptableObject.abilityRadiusDamage;
        this.speedAmount = scriptableObject.abilitySpeedAmount;
        this.radiusCC = scriptableObject.abilityRadiusCC;
    }

}
