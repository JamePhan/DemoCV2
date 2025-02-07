using UnityEngine;

public class OneProjectile : MonoBehaviour, IAbility
{
    public Transform _transform { get ; set ; }
    public float _distanceToTarget { get ; set ; }
    public AbilityStats _stats { get ; set ; }
    public Transform _target;
    private CastConditions _castCondition;

    public OneProjectile()
    {

    }

    public void SetAbilityStats(AbilityStats stats, Transform rocketLauncher, float distanceToTarget)
    {
        _stats = stats;
        _transform = rocketLauncher;
        _distanceToTarget = distanceToTarget;
        _castCondition = new CastConditions();
    }

    public void Activate()
    {
        SpawnBullet();
    }

    public bool CastCondition()
    {
        _target = _castCondition.InAttackRange(_transform, _stats);

        if (_target != null) return true;
        else return false;
    }

    public void SpawnBullet()
    {
        AssetsManager.Instance.InitPrefab(AssetsManager.Instance._redProjectile, (GameObject bullet) =>
        {
            bullet.transform.position = Vector3.zero;
            SoundManager.Instance.BulletSpawn();
            bullet.transform.position = FindDeepChild(transform, "Weapon").position;
            
            BulletController prj = bullet.GetComponent<BulletController>();
            prj.SetDirection(_target.position);
            prj.SetDamage(_stats.damage);
        });
    }

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

    public void EffectExecute()
    {
        
    }

    public void Execute()
    {
        
    }

    

}
