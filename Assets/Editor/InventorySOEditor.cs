using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InventorySO))]
public class InventorySOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        InventorySO scriptableObject = (InventorySO)target;
        scriptableObject.Name = EditorGUILayout.TextField("Name inventory", scriptableObject.Name);
        scriptableObject.Icon = (Sprite)EditorGUILayout.ObjectField("Icon", scriptableObject.Icon, typeof(Sprite), false);
        scriptableObject.Description = EditorGUILayout.TextField("Description", scriptableObject.Description);
        scriptableObject.Kind = (InventoryKind)EditorGUILayout.EnumPopup("Kind of inventory", scriptableObject.Kind);
        switch (scriptableObject.Kind)
        {
            case InventoryKind.Weapon:
                scriptableObject.DamageIncrease = EditorGUILayout.IntField("Damage Increase", scriptableObject.DamageIncrease);
                scriptableObject.HealthIncrease = EditorGUILayout.IntField("Hp Increase", scriptableObject.HealthIncrease);
                break;

            case InventoryKind.Armor:
                scriptableObject.HealthIncrease = EditorGUILayout.IntField("Hp Increase", scriptableObject.HealthIncrease);
                break;

            case InventoryKind.Ring:
                scriptableObject.DamageIncrease = EditorGUILayout.IntField("Damage Increase", scriptableObject.DamageIncrease);
                scriptableObject.HealthIncrease = EditorGUILayout.IntField("Hp Increase", scriptableObject.HealthIncrease);
                break;
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
