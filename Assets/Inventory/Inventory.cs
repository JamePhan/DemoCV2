using UnityEngine;

public enum InventoryKind
{
    Weapon, Armor, Spirit, Locket, Ring, Bracelet
}

[System.Serializable]
public abstract class Inventory
{
    public InventoryKind Kind;
    public string Name;
    public Sprite Icon;
    public string Description;

    public abstract void Effect();

}
