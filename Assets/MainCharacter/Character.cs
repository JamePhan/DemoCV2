using UnityEngine;

public class Character
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Damage { get; set; }
    public float AttackSpeed { get; set; }
    public float MoveSpeed { get; set; }
    public float AttackRange { get; set; }
    public int NumberTarget { get; set; }
    public int PercentExpBonusEarn { get; set; }
    public CharacterAbility Ability1 { get; set; }
    public CharacterAbility Ability2 { get; set; }

    public Character() { }

    public Character(string name, int health, int damage, float atkSpeed, float moveSpeed, float atkRange, int bonusExp,
        CharacterAbility a1, CharacterAbility a2)
    {

        this.Name = name;
        this.Health = health;
        this.Damage = damage;
        this.AttackSpeed = atkSpeed;
        this.MoveSpeed = moveSpeed;
        this.AttackRange = atkRange;
        this.PercentExpBonusEarn = bonusExp;
        this.Ability1 = a1;
        this.Ability2 = a2;
    }

    public static Character GetCharacter(string charName)
    {
        CharacterSO _charSO = Resources.Load<CharacterSO>("Character/" + charName);

        return new Character(_charSO.name, _charSO.Health, _charSO.Damage, _charSO.AttackSpeed, 
            _charSO.MoveSpeed, _charSO.AttackRange, _charSO.PercentExpBonusEarn, _charSO.Ability1, _charSO.Ability2);
    }

    public Character Clone()
    {
        return new Character
        {
            Name = this.Name,
            Health = this.Health,
            Damage = this.Damage,
            AttackSpeed = this.AttackSpeed,
            MoveSpeed = this.MoveSpeed,
            AttackRange = this.AttackRange,
            NumberTarget = this.NumberTarget,
            PercentExpBonusEarn = this.PercentExpBonusEarn,
            Ability1 = this.Ability1,
            Ability2 = this.Ability2,
        };
    }

}
