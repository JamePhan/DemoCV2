using UnityEngine;

public enum EnemyType
{
    Male, Female, Soldier, None
}

public class Enemy
{
    public string name;
    public EnemyType type;
    public int health;
    public int damage;
    public float cooldownAttack;
    public float moveSpeed;
    public float atkRange;
    public int gold;

    public bool haveAbility;
    public Abilities ability;
    public AbilityStats abilityStats;

    public Enemy(string name, EnemyType type, int health, int damage, float speed, 
        float cooldownAtk, float atkRange, int gold, bool haveAbility)
    {
        this.name = name;
        this.type = type;
        this.health = health;
        this.damage = damage;
        this.moveSpeed = speed;
        this.cooldownAttack = cooldownAtk;
        this.atkRange = atkRange;
        this.gold = gold;
        this.haveAbility = haveAbility;
    }

    public void GetEnemyAbility(Abilities ability)
    {
        this.ability = ability;
        AbilitySO abilityStatsSO = Resources.Load<AbilitySO>("Ability/" + ability.ToString());
        this.abilityStats = new AbilityStats(abilityStatsSO);
    }

    public static Enemy GetEnemy(string charName)
    {
        EnemySO _charSO = Resources.Load<EnemySO>("Enemy/" + charName);
        Enemy enemy = new Enemy(_charSO.name, _charSO.EnemyType, _charSO.Health, _charSO.Damage, _charSO.moveSpeed, _charSO.cooldownAttack,
            _charSO.atkRange, _charSO.gold, _charSO.haveAbility);

        if(_charSO.haveAbility)
        {
            enemy.ability = _charSO.ability;
            enemy.GetEnemyAbility(_charSO.ability);
        }
        return enemy;
    }
}