using System.Collections;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, IDamageable, IAttack
{
    public Enemy                enemy;
    public int                  Health { get; set ; }
    public int                  Damage { get; set ; }
    public float                AttackSpeed { get ; set ; }
    public float                AttackRange { get ; set ; }
    public LayerMask            Target { get ; set ; }

    public int                  currentHealth;
    public int                  _damage;
    public float                _moveSpeed;
    public float                _moveSpeedCurrent;
    public int                  _gold;
    public Animator             animator;
    public AnimationController  animController;
    public Spawner              _spawner;

    public bool                 _isRunning;
    public bool                 _isDead;
    public bool                 _isGameReseting;
    public bool                 _isFollowing;
    public bool                 _isAllowAttack;
    public bool                 _haveAbility;

    public Material             _damageFlash;
    private Renderer            _renderer;

    public MeshRenderer         _headRenderer;
    public SkinnedMeshRenderer  _allRenderer;
    public AbilityFactory       _abilityFactory;
    public Ability              _ability;


    public void Init(Enemy enemy, LayerMask target, Material damageFlash, Spawner spawner)
    {
        this.enemy                  = enemy;
        this.Health                 = enemy.health;
        this.currentHealth          = this.Health;
        this._damage                = enemy.damage;
        this.AttackSpeed            = 0;
        this._moveSpeed             = enemy.moveSpeed;
        this._moveSpeedCurrent      = this._moveSpeed;
        this.AttackRange            = enemy.atkRange;
        this._gold                  = enemy.gold;
        animator                    = GetComponentInChildren<Animator>();
        animController              = new AnimationController(animator, enemy.type);
        Target                      = target;
        this._spawner               = spawner;

        InitCheck();
        if (enemy.haveAbility)
        {
            _abilityFactory         = new AbilityFactory(enemy, transform);
            _ability                = _abilityFactory.GetAbility(enemy.ability);
        }

        this._damageFlash           = damageFlash;
        _renderer = enemy.type == EnemyType.Soldier
        ? GetComponentInChildren<SkinnedMeshRenderer>()
        : GetComponentInChildren<MeshRenderer>();
    }

    private void OnEnable()
    {
        InitCheck();
        currentHealth = this.Health;
    }

    public void InitCheck()
    {
        _isRunning = false;
        _isDead = false;
        _isGameReseting = false;
        _isFollowing = true;
        _isAllowAttack = true;
        if (enemy == null) return;
        if (enemy.haveAbility)
        {
            _haveAbility = true;
        }
        else _haveAbility = false;
    }

    public void Move(Vector3 playerPosition)
    {
        if (_isDead) return;
        Rotate(playerPosition);

        if (_isFollowing) FollowPlayer(playerPosition);
        else HoldThisPosition();

        if (AttackSpeed <= 0)
        {
            Detect();
            AttackSpeed = enemy.cooldownAttack;
        }
        else
        {
            DelayAttack();
        }

        if(_haveAbility)_ability.Ready();
    }

    public void HoldThisPosition()
    {
        _isRunning = false;
        animController.Idle();
    }

    public void DelayAttack()
    {
        AttackSpeed -= Time.deltaTime;
    }

    public void Rotate(Vector3 playerPosition)
    {
        transform.LookAt(playerPosition);
    }

    public void FollowPlayer(Vector3 playerPosition)
    {
        if(!_isRunning) animController.Run();
        _isRunning = true;

        Vector3 direction = (playerPosition - transform.position).normalized;
        transform.Translate(direction * _moveSpeedCurrent, Space.World);
    }

    public void Detect()
    {
        Collider[] playerCol = Physics.OverlapSphere(transform.position, this.AttackRange, Target);
        if (playerCol.Length != 0 && _isAllowAttack) 
        {
            _isFollowing = false;
            Attack(playerCol);
        }
        else _isFollowing = true;
    }

    public void Attack(Collider[] playerCol)
    {
        foreach (Collider col in playerCol)
        {
            if (col.gameObject.TryGetComponent(out IDamageable damage))
            {
                damage.GetDamage(_damage);
            }
        }
    }

    public void GetDamage(int amount)
    {
        if (_renderer != null) StartCoroutine(GetDamageFlashEffect());

        currentHealth -= amount;
        if (currentHealth > 0) return;

        currentHealth = 0;
        Die();
    }

    private IEnumerator GetDamageFlashEffect()
    {
        if (_renderer == null) yield break;
        Material classicMaterial = _renderer.material;
        _renderer.material = _damageFlash;
        yield return new WaitForSeconds(0.2f);
        _renderer.material = classicMaterial;
    }

    public void IsGameReset()
    {
        _isGameReseting = true;
        Die();
    }

    public void Die()
    {
        gameObject.layer = LayerMask.NameToLayer("Corpse");
        _isDead = true;
        animController.Die();
        if (!_isGameReseting) GainCoin();
        if (gameObject.activeSelf) StartCoroutine(Kill());
    }

    public void GainCoin()
    {
        CoinAnimation.Instance.AnimateCoin(transform);
        GoldManager.Instance.IncreaseGold(_gold);
    }

    public IEnumerator Kill()
    {
        yield return new WaitForSeconds(1f);
        this._spawner.Kill(gameObject);
    }
}
