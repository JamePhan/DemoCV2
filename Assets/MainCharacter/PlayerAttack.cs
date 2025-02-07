using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour, IAttack
{
    public event Action ProjectileMove;

    [Header("Player Stats")]
    public      Character               _character;
    public      int                     Damage { get; set; }
    public      float                   AttackSpeed { get; set; }
    public      float                   AttackRange { get; set; }
    public      LayerMask               Target { get; set; }
    public      int                     _projectileSpeed;

    [Header("Other")]
    public      Transform               _target;
    private     AnimationController     _animController;
    public      Collider[]              _hitColliders;
    public      Spawner                 _projectilePool;

    [Header("Check")]
    public      bool                    _allowAttack;
    public      bool                    _enemyDetected;

    public void Init(Character character, LayerMask layerMask, Animator anim)
    {
        this._character = character;
        this.AttackSpeed = character.AttackSpeed;
        this.Damage = this._character.Damage;
        this.AttackRange = this._character.AttackRange;
        this.Target = layerMask;
        _projectileSpeed = 30;

        _animController = new AnimationController(anim);
        _allowAttack = true;

        _projectilePool = transform.AddComponent<Spawner>();

        AssetsManager.Instance.InitPrefab(AssetsManager.Instance._yellowProjectile, (GameObject prj) =>
        {
            _projectilePool.Init(prj, true, 5);
            _projectilePool.Kill(prj);
        });
        
    }

    private void FixedUpdate()
    {
        if (!_allowAttack) return;
        if (AttackSpeed <= 0)
        {
            Detect();
            Attack(_hitColliders);
            AttackSpeed = _character.AttackSpeed;
        }
        else
        {
            DelayAttack();
        }
        ProjectileMove?.Invoke();
    }

    public void PlayerIsDead()
    {
        _allowAttack = false;
    }

    public void DelayAttack()
    {
        AttackSpeed -= Time.deltaTime;
    }

    public bool AttackEnemy()
    {
        if (_target == null) return false;

        if (_target.gameObject.TryGetComponent(out IDamageable damage))
        {
            damage.GetDamage(Damage);
        }
        return true;
    }

    public void Detect()
    {
        _hitColliders = Physics.OverlapSphere(transform.position, AttackRange, Target);
        if (_hitColliders.Length == 0)
        {
            _target = null;
            return;
        }

        if (GetClosestEnemy(_hitColliders) != null)
        {
            _target = _hitColliders[_hitColliders.Length - 1].transform;
            Rotate(_target.position);
        }
    }

    public Transform GetClosestEnemy(Collider[] hitColliders)
    {
        var closestCollider = hitColliders
        .OrderBy(collider => Vector3.Distance(transform.position, collider.transform.position))
        .FirstOrDefault();
        if (closestCollider != null) return closestCollider.transform;
        return null;
    }

    public void Rotate(Vector3 _target)
    {
        transform.LookAt(_target);
    }

    public void Attack(Collider[] enemy)
    {
        if (_target == null) return;
        SoundManager.Instance.Shoot();
        InitProjectile();
        if (_target.gameObject.TryGetComponent(out IDamageable damage))
        {
            _animController.MainCharacterShoot();
            damage.GetDamage(Damage);
        }
    }

    public void InitProjectile()
    {
        GameObject prj = _projectilePool.Spawn();
        prj.transform.position = transform.position;

        YellowProjectile ylprj = prj.GetComponent<YellowProjectile>();
        if (ylprj == null)
        {
            ylprj = prj.AddComponent<YellowProjectile>();
            ProjectileMove += () => ylprj.Move();
        }
        ylprj.Init(FixTargetPosition(), _projectileSpeed, TimeToTarget(), _projectilePool);
    }

    public Vector3 FixTargetPosition()
    {
        return new Vector3(_target.transform.position.x, _target.transform.position.y + 1f, _target.transform.position.z);
    }

    public float TimeToTarget()
    {
        if(_target == null) 
        { 
            Debug.LogWarning("Target is not assigned!");
            return 0f;
        }
        float distance = Vector3.Distance(transform.position, _target.position);
        float time = distance / _projectileSpeed;
        return time;
    }
}
