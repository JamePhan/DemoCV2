using UnityEngine;

public class RocketLauncher : MonoBehaviour, IAbility
{
    
    public      float           _distanceToTarget           { get; set; }
    public      AbilityStats    _stats                      { get; set; }
    public      Transform       _target;
    private     CastConditions  _castCondition;

    public RocketLauncher()
    {
        
    }

    public void SetAbilityStats(AbilityStats stats, float distanceToTarget)
    {
        _stats = stats;
        _distanceToTarget = distanceToTarget;
        _castCondition = new CastConditions();
    }

    public bool CastCondition()
    {
        _target = _castCondition.InAttackRange(transform, _stats);

        if(_target != null) return true;
        else return false;
    }

    public void Activate()
    {
        SpawnRocket();
    }

    public void SpawnRocket()
    {
        AssetsManager.Instance.InitPrefab(AssetsManager.Instance._rocket, (GameObject rocket) =>
        {
            rocket.transform.position = Vector3.zero;
            SoundManager.Instance.RocketSpawn();
            rocket.transform.position = FindDeepChild(transform, "Weapon").position;
            ProjectileController prj = rocket.GetComponent<ProjectileController>();
            prj.SetTarget(_target.position);
            prj.SetDamage(_stats.damage);
        });
    }

    // X
    public Transform FindDeepChild(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name) return child;
            Transform result = FindDeepChild(child, name);
            if (result != null) return result;
        }
        return null;
    }


    public void EffectActivate()
    {
        
    }

    public void EffectDuration()
    {
        
    }

    public void Execute()
    {
        
    }

    public void EffectExecute()
    {
        
    }

    

}
