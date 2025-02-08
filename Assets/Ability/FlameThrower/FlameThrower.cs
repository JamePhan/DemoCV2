using Unity.VisualScripting;
using UnityEngine;

public class FlameThrower : MonoBehaviour, IAbility
{
    public float                _distanceToTarget { get ; set ; }
    public AbilityStats         _stats { get ; set ; }
    public Transform            _target;
    private CastConditions      _castCondition;
    private ParticleSystem      _flameProjectile;

    public void SetAbilityStats(AbilityStats stats, float distanceToTarget)
    {
        _stats = stats;
        _distanceToTarget = distanceToTarget;
        _castCondition = new CastConditions();
        _flameProjectile = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    public void Activate()
    {
        Attack();
    }

    public void Attack()
    {
        Collider[] playerCol = Physics.OverlapSphere(transform.position, _stats.radiusDamage, LayerMask.GetMask("Player"));
        if (playerCol.Length != 0)
        {
            foreach (Collider col in playerCol)
            {
                if (col.gameObject.TryGetComponent(out IDamageable damage))
                {
                    damage.GetDamage(_stats.radiusDamage);
                }
            }
        }
    }


    public bool CastCondition()
    {
        _target = _castCondition.InAttackRange(transform, _stats);

        if (_target != null) return true;
        else
        {
            if (_flameProjectile.isPlaying) _flameProjectile.Stop();
            return false;
        }
    }

    public void EffectActivate()
    {
        if(!_flameProjectile.isPlaying) _flameProjectile.Play();
    }

    public void EffectDuration()
    {
        
    }

    public void EffectExecute()
    {
        
    }

    public void Execute()
    {
        
    }

    

}
