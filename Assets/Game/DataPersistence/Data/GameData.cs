using System.Collections.Generic;

[System.Serializable]
public class CharacterData
{
    public string Name;
    public int Cost;
    public bool IsUnlocked;

    public CharacterData(string name, int cost, bool isUnlocked)
    {
        this.Name = name;
        this.Cost = cost;
        this.IsUnlocked = isUnlocked;
    }
}

[System.Serializable]
public class GameData
{
    public int Gold;
    public int Diamond;
    public List<string> ListInventory;
    public List<CharacterData> ListCharacter;

    public GameData()
    {
        Gold = 10000;
        Diamond = 0;
        CreateNewListInventory();
        CreateNewListCharacter();
    }

    public void CreateNewListInventory()
    {
        ListInventory = new List<string>
        {
            "CommonRedBow",
            "CommonTornado",
            "LegendaryPhantomCloak",
            "LegendTornado",
            "PurpleBearRing",
        };
    }

    public void CreateNewListCharacter()
    {
        ListCharacter = new List<CharacterData>
        {
            new CharacterData("John", 0, true),
            new CharacterData("James", 1000, false),
            new CharacterData("Sheriff", 5000, false),
            new CharacterData("Bob", 10000, false),
        };
    }
}
