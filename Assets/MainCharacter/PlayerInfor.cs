public class PlayerInfor
{
    private int _hp;
    private int _atk;

    public int _hpInven {  get; set; }
    public int _atkInven { get; set; }
    public int _hpChar { get; set; }
    public int _atkChar { get; set; }

    public PlayerInfor() { }

    public PlayerInfor(int hp, int atk)
    {
        this._hp = hp;
        this._atk = atk;
        _hpInven = 0; _atkInven = 0;
        _hpChar = 0; _atkChar = 0;
    }

    public void SetInventoryStats(int hp, int atk)
    {
        this._hpInven = hp;
        this._atkInven = atk;
    }

    public void SetCharacterStats(int hp, int atk)
    {
        this._hpChar = hp;
        this._atkChar = atk;
    }

    public void ResetInventoryStats()
    {
        _hpInven = 0;
        _atkInven = 0;
    }

    public void ResetCharacterStats()
    {
        _hpChar = 0;
        _atkChar = 0;
    }

    public int GetHp()
    {
        return _hp = _hpInven + _hpChar;
    }

    public int GetAtk()
    {
        return _atk = _atkInven + _atkChar;
    }
}
