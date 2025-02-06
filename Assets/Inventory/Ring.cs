using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : Inventory
{
    public int Damage;
    public int Health;

    public Ring(string name, Sprite icon, string description, int damage)
    {
        this.Kind = InventoryKind.Ring;
        this.Name = name;
        this.Icon = icon;
        this.Description = description;

        this.Damage = damage;
    }

    public Ring(InventorySO scriptableObject)
    {
        this.Kind = InventoryKind.Ring;
        this.Name = scriptableObject.Name;
        this.Icon = scriptableObject.Icon;
        this.Description = scriptableObject.Description;

        this.Damage = scriptableObject.DamageIncrease;
        this.Health = scriptableObject.HealthIncrease;
    }

    public override void Effect()
    {
        Debug.Log("ARMOR EFFECT");
    }
}
