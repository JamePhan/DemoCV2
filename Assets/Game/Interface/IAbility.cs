public interface IAbility
{
    public float        _distanceToTarget { get; set; }
    public AbilityStats _stats { get; set; }

    void SetAbilityStats(AbilityStats stats, float distanceToTarget);

    bool CastCondition();
    void Activate();
    void EffectActivate();
    void EffectDuration();
    void Execute();
    void EffectExecute();
}
