public enum CurrencyKind
{
    Gold, Diamond
}

[System.Serializable]
public class Currency
{
    public CurrencyKind Kind;
    public int amount;

    public Currency(CurrencyKind kind, int amount)
    {
        this.Kind = kind;
        this.amount = amount;
    }
} 