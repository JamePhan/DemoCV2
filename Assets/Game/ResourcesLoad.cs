using UnityEngine;

public class ResourcesLoad
{
    public UtilitySO[] LoadAllUtility()
    {
        UtilitySO[] all = Resources.LoadAll<UtilitySO>("Utility/");
        return all;
    }

    public UtilitySO LoadUtility(string utilityName)
    {
        UtilitySO scriptableObject = Resources.Load<UtilitySO>("Utility/" + utilityName);
        return scriptableObject;
    }

}
