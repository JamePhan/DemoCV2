using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbilitySO))]
public class AbilitySOEditor : Editor
{
    private SerializedProperty abilityType;
    private SerializedProperty skillSprite;
    private SerializedProperty nameAbility;
    private SerializedProperty cooldownTime;
    private SerializedProperty durationTime;
    private SerializedProperty keyBoard;
    private SerializedProperty abilityHpAmount;
    private SerializedProperty abilityDamage;
    private SerializedProperty abilityRadiusDamage;
    private SerializedProperty abilitySpeedAmount;
    private SerializedProperty abilityRadiusCC;

    private void OnEnable()
    {
        abilityType = serializedObject.FindProperty("AbilityType");
        skillSprite = serializedObject.FindProperty("skillSprite");
        nameAbility = serializedObject.FindProperty("nameAbility");
        cooldownTime = serializedObject.FindProperty("cooldownTime");
        durationTime = serializedObject.FindProperty("durationTime");
        keyBoard = serializedObject.FindProperty("keyBoard");
        abilityHpAmount = serializedObject.FindProperty("abilityHpAmount");
        abilityDamage = serializedObject.FindProperty("abilityDamage");
        abilityRadiusDamage = serializedObject.FindProperty("abilityRadiusDamage");
        abilitySpeedAmount = serializedObject.FindProperty("abilitySpeedAmount");
        abilityRadiusCC = serializedObject.FindProperty("abilityRadiusCC");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(skillSprite);
        EditorGUILayout.PropertyField(nameAbility);
        EditorGUILayout.PropertyField(cooldownTime);
        EditorGUILayout.PropertyField(durationTime);
        EditorGUILayout.PropertyField(keyBoard);
        EditorGUILayout.PropertyField(abilityType);

        AbilitySO scriptableObject = (AbilitySO)target;

        switch (scriptableObject.AbilityType)
        {
            case AbilityType.Damage:
                EditorGUILayout.PropertyField(abilityDamage, new GUIContent("Damage"));
                EditorGUILayout.PropertyField(abilityRadiusDamage, new GUIContent("Radius"));
                break;

            case AbilityType.BuffHp:
                EditorGUILayout.PropertyField(abilityHpAmount, new GUIContent("HP Amount"));
                break;

            case AbilityType.BuffSpeed:
                EditorGUILayout.PropertyField(abilitySpeedAmount, new GUIContent("Speed Amount"));
                break;

            case AbilityType.CrowdControl:
                EditorGUILayout.PropertyField(abilityRadiusCC, new GUIContent("CC Radius"));
                break;
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
