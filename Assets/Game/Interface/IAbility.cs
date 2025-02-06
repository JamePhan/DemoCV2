using System;
using UnityEngine;

public interface IAbility
{
    public Transform    _transform {  get; set; }
    public float        _distanceToTarget { get; set; }
    public AbilityStats _stats { get; set; }

    void SetAbilityStats(AbilityStats stats, Transform rocketLauncher, float distanceToTarget);

    bool CastCondition();
    void Activate();
    void EffectActivate();
    void EffectDuration();
    void Execute();
    void EffectExecute();
}
