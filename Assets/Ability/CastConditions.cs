using UnityEngine;

public class CastConditions 
{
    public Transform InAttackRange(Transform _transform, AbilityStats _stats)
    {
        Collider[] playerCol = Physics.OverlapSphere(_transform.position, _stats.radiusDamage, LayerMask.GetMask("Player"));
        if (playerCol.Length != 0)
        {
            return playerCol[0].transform;
        }
        return null;
    }

}
