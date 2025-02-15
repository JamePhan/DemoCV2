using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public delegate void ResetGameDelegate();
    public static event ResetGameDelegate resetGameDelegate;

    [Header("Stats")]
    public int                  _timeMinute;
    public float                _timeSecond;

    [Header("Reference")]
    public CharacterManager2    characterManager;
    public SpawnManager         spawnManager;
    public LevelManager         levelManager;
    public TextMeshProUGUI      _txt_time;

    [Header("Other")]
    public GameObject           _player;
    public Character            _character;
    public PlayerInfor          playerInfor;

    protected override void InAwake()
    {
        Init();
    }

    public void Init()
    {
        playerInfor = new PlayerInfor(0, 0);
        AutoSelectCharacter();
        //levelManager.Init(character);

        PanelManager.Instance.ShowPanel("Panel_Play");
        resetGameDelegate += ResetGameTime;
    }

    public void AutoSelectCharacter()
    {
        this._character = Character.GetCharacter("John");
        CharacterChose(this._character);
        playerInfor.ResetCharacterStats();
        playerInfor.SetCharacterStats(_character.Health, _character.Damage);
    }

    public void CharacterChose(Character character)
    {
        this._character = character;
    }

    public void InitPlayer()
    {
        Character _playerStats = new Character();
        if(_character == null) return;
        _playerStats = _character;
        _playerStats.Health = playerInfor.GetHp();
        _playerStats.Damage = playerInfor.GetAtk();
        _player.transform.position = new Vector3(-15.6f, 0.1999998f, 5.45f);
        characterManager.Init(_character);
    }

    private void Update()
    {
        UpdateGameTime();
    }

    public void UpdateGameTime()
    {
        _timeSecond += Time.deltaTime;
        if (_timeSecond >= 60f)
        {
            _timeMinute++;
            _timeSecond = 0;
        }
        if(_timeSecond < 10)
        {
            _txt_time.text = _timeMinute + ":0" + (int)_timeSecond;
        }
        else
        {
            _txt_time.text = _timeMinute + ":" + (int)_timeSecond;
        }
        
    }

    public void ResetGame()
    {
        resetGameDelegate?.Invoke();
    }

    public void ResetGameTime()
    {
        _timeMinute = 0;
        _timeSecond = 0;
    }
}
