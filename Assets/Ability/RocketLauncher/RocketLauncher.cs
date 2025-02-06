using BigRookGames.Weapons;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class RocketLauncher : MonoBehaviour, IAbility
{
    public      Transform       _transform                  { get; set; }
    public      float           _distanceToTarget           { get; set; }
    public      AbilityStats    _stats                      { get; set; }
    public      Transform       _target;

    public RocketLauncher()
    {
        
    }

    public void SetAbilityStats(AbilityStats stats, Transform rocketLauncher, float distanceToTarget)
    {
        _stats = stats;
        _transform = rocketLauncher;
        _distanceToTarget = distanceToTarget;
    }

    public bool CastCondition()
    {
        Collider[] playerCol = Physics.OverlapSphere(_transform.position, 8f, LayerMask.GetMask("Player"));
        if (playerCol.Length != 0)
        {
            _target = playerCol[0].transform;
            return true;
        }
        
        return false;
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
