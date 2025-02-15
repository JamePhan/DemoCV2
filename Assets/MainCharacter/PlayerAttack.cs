using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour, IAttack
{
    public event Action ProjectileMove;

    [Header("Player Stats")]
    public      Character               _character;
    public      int                     Damage          { get => _character.Damage;         set => _character.Damage        = value; }
    public      float                   AttackSpeed     { get => _character.AttackSpeed;    set => _character.AttackSpeed   = value; }
    public      float                   AttackRange     { get => _character.AttackRange;    set => _character.AttackRange   = value; }
    public      LayerMask               Target          { get; set; }
    public      int                     _projectileSpeed;
    public      float                   _atkSpeedCooldown;

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
        if (_atkSpeedCooldown <= 0)
        {
            Detect();
            Attack(_hitColliders);
            _atkSpeedCooldown = _character.AttackSpeed;
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
        _atkSpeedCooldown -= Time.deltaTime;
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
