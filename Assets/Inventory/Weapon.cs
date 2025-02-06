using UnityEngine;

[System.Serializable]
public class Weapon : Inventory
{
    public int Damage;
    public int Health;

    public Weapon(string name, Sprite icon, string description, int damage)
    {
        this.Kind = InventoryKind.Weapon;
        this.Name = name;
        this.Icon = icon;
        this.Description = description;

        this.Damage = damage;
    }

    public Weapon(InventorySO scriptableObject)
    {
        this.Kind = InventoryKind.Weapon;
        this.Name = scriptableObject.Name;
        this.Icon = scriptableObject.Icon;
        this.Description = scriptableObject.Description;

        this.Damage = scriptableObject.DamageIncrease;
        this.Health = scriptableObject.HealthIncrease;
    }

    public override void Effect()
    {
        Debug.Log("WEAPON EFFECT");
    }
}


