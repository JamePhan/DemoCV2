using System.Collections;
using Terresquall;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterBehaviour2 : MonoBehaviour, IDamageable
{
    public delegate void PlayerDieDelegate();
    public static event PlayerDieDelegate Del_PlayerDie;

    [Header("References")]
    public Movement             _movement;
    public PlayerAttack         _playerAttack;
    public VirtualJoystick      _joystick;
    public Animator             _playerAnim;
    public HpbarFollow          _hpbarFollow;
    public LayerMask            _layerMask;

    public int                  Health { get ; set; }
    public int                  currentHealth;
    
    [Header("Check")]
    public bool                 _isDead;

    private AnimationController _animController;

    public void Init(Character character)
    {
        this.Health = character.Health;
        this.currentHealth = this.Health;

        InitMovement(character);
        InitAttack(character);
        InitHPBar();

        _animController = new AnimationController(_playerAnim);
        _isDead = false;
    }

    public void InitMovement(Character character)
    {
        _movement = transform.AddComponent<Movement>();
        _movement.Init(character, _joystick, _playerAnim);
        Del_PlayerDie += _movement.PlayerIsDead;
    }

    public void DestroyMovement()
    {
        if (_movement != null)
        {
            Del_PlayerDie -= _movement.PlayerIsDead;
            Destroy(_movement);
            _movement = null;
        }
    }

    public void InitAttack(Character character)
    {
        _playerAttack = transform.AddComponent<PlayerAttack>();
        _playerAttack.Init(character, _layerMask, _playerAnim);
        Del_PlayerDie += _playerAttack.PlayerIsDead;
    }

    public void DestroyAttack()
    {
        if (_playerAttack != null)
        {
            Del_PlayerDie -= _playerAttack.PlayerIsDead;
            Destroy(_playerAttack);
            _playerAttack = null;
        }
    }

    public void InitHPBar()
    {
        _hpbarFollow.gameObject.SetActive(true);
        _hpbarFollow.Init();
        _hpbarFollow.UpdateHpBar(this.currentHealth, this.Health);
    }

    public void GetDamage(int amount)
    {
        if(_isDead) return;
        currentHealth -= amount;
        
        if (currentHealth < 0)
        {
            currentHealth = 0;
            _isDead = true;
            Die();
        }
        _hpbarFollow.UpdateHpBar(this.currentHealth, this.Health);
    }

    public void Die()
    {
        Del_PlayerDie?.Invoke();
        _animController.MaleDie();
        DestroyMovement();
        DestroyAttack();
        StartCoroutine(GetReward());
    }

    public IEnumerator GetReward()
    {
        yield return new WaitForSeconds(1.5f);
        SoundManager.Instance.UI_Win();
        RewardManager.Instance.GetRandomReward();
    }
}