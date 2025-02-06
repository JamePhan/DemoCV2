using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Inventory
{
    public int Health;

    public Armor(string name, Sprite icon, string description, int health)
    {
        this.Kind = InventoryKind.Armor;
        this.Name = name;
        this.Icon = icon;
        this.Description = description;

        this.Health = health;
    }

    public Armor(InventorySO scriptableObject)
    {
        this.Kind = InventoryKind.Armor;
        this.Name = scriptableObject.Name;
        this.Icon = scriptableObject.Icon;
        this.Description = scriptableObject.Description;

        this.Health = scriptableObject.HealthIncrease;
    }

    public override void Effect()
    {
        Debug.Log("ARMOR EFFECT");
    }
}
