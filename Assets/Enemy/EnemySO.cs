using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "ScriptableObject/Enemy")]
public class EnemySO : ScriptableObject
{
    public string Name;
    public EnemyType EnemyType;
    public int Health;
    public int Damage;
    public float cooldownAttack;
    public float moveSpeed;
    public int gold;
    public float atkRange;
    public bool haveAbility;
    public Abilities ability;
}
