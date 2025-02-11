using UnityEngine;

[CreateAssetMenu(fileName = "NewUtility", menuName = "ScriptableObject/Utility")]
public class UtilitySO : ScriptableObject
{
    public      Sprite               Icon;
    public      int                  UpgradeStat;
    public      int                  Price;
}
