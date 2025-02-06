using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "ScriptableObject/Character")]
public class CharacterSO : ScriptableObject
{
    public string Name;
    public int Health;
    public int Damage;
    public float AttackSpeed;
    public float MoveSpeed;
    public float AttackRange;
    public int PercentExpBonusEarn;
    public CharacterAbility Ability1;
    public CharacterAbility Ability2;
}