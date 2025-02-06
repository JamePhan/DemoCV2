using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemySO))]
public class EnemySOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EnemySO scriptableObject = (EnemySO)target;
        scriptableObject.Name = EditorGUILayout.TextField("Name Enemy", scriptableObject.Name);
        scriptableObject.EnemyType = (EnemyType)EditorGUILayout.EnumPopup("Enemy Type", scriptableObject.EnemyType);
        scriptableObject.Health = EditorGUILayout.IntField("Hp", scriptableObject.Health);
        scriptableObject.Damage = EditorGUILayout.IntField("Damage", scriptableObject.Damage);
        scriptableObject.cooldownAttack = EditorGUILayout.FloatField("Cooldown attack", scriptableObject.cooldownAttack);
        scriptableObject.moveSpeed = EditorGUILayout.FloatField("Speed", scriptableObject.moveSpeed);
        scriptableObject.atkRange = EditorGUILayout.FloatField("Atk Range", scriptableObject.atkRange);
        scriptableObject.gold = EditorGUILayout.IntField("Gold", scriptableObject.gold);
        scriptableObject.haveAbility = EditorGUILayout.Toggle("Have ability", scriptableObject.haveAbility);

        if (scriptableObject.haveAbility)
        {
            EditorGUILayout.Space();
            scriptableObject.ability = (Abilities)EditorGUILayout.EnumPopup("Kind of ability", scriptableObject.ability);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }

        serializedObject.ApplyModifiedProperties();
    }
}