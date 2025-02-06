using UnityEngine;

[CreateAssetMenu(fileName = "NewInventory", menuName = "ScriptableObject/Inventory")]
public class InventorySO : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public string Description;
    public InventoryKind Kind;
    public int HealthIncrease;
    public int DamageIncrease;
    public float SpeedIncrease;
}


