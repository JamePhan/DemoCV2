using System;
using UnityEngine;

public enum AbilityType
{
    Damage, BuffSpeed, CrowdControl, BuffHp
}

public class Ability 
{
    public AbilityStats Stats { get; set; }
    public float DurationTime { get; set; }
    public float CooldownTime { get; set; }
    public string Keyboard { get; set; }
    public float cooldown { get; set; }
    public float duration;

    public Func<bool> CastCondition { get; set; }
    public Action Activate { get; set; }
    public Action EffectActivate { get; set; }
    public Action EffectDuration { get; set; }
    public Action Execute { get; set; }
    public Action EffectExecute { get; set; }

    public bool canCast;

    public Ability(Func<bool> castCondition, Action activate, Action effectActivate,
        Action effectDuration, Action execute, Action effectExecute)
    {
        canCast = true;
        this.CastCondition = castCondition;
        this.Activate = activate;
        this.EffectActivate = effectActivate;
        this.EffectDuration = effectDuration;
        this.Execute = execute;
        this.EffectExecute = effectExecute;
    }

    public void LoadAbilityDetail(AbilityStats stats)
    {
        this.Stats = stats;
        this.CooldownTime = stats.cooldownTime;
        this.cooldown = this.CooldownTime;
        this.DurationTime = stats.durationTime;
        this.duration = this.DurationTime;
    }

    public void Ready()
    {
        if (CastCondition() && canCast)
        {
            Activate();
            EffectActivate();
            duration = DurationTime;
            AbilityManager.abilityActiveDelegete += Cast;
            canCast = false;
        }
    }

    public void Cast()
    {
        if (duration > 0)
        {
            EffectDuration();
            duration -= Time.deltaTime;
        }
        else
        {
            Execute();
            EffectExecute();
            DisableEffectDuration();
            AbilityManager.abilityActiveDelegete -= Cast;
            AbilityManager.abilityCooldownDelegete += Cooldown;
            duration = DurationTime;
        }
    }

    public void Cooldown()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            AbilityManager.abilityCooldownDelegete -= Cooldown;
            cooldown = CooldownTime;
            canCast = true;
        }
    }

    public virtual void GetAbilityStats() { }

    public virtual void DisableEffectDuration() { }
}