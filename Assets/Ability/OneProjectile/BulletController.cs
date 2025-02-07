using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10;
    private bool targetHit;
    public int _damage;

    void FixedUpdate()
    {
        if (targetHit) return;

        transform.position += transform.forward * speed * Time.deltaTime;

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 0.5f, LayerMask.GetMask("Player", "Wall")))
        {
            if (hit.transform == null) return;

            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Collider[] newCol = new Collider[1] { hit.collider };
                Attack(newCol);
            }
            DestroyBullet();
        }
    }

    public void SetDirection(Vector3 target)
    {
        transform.forward = (new Vector3(target.x, target.y + 1f, target.z) - transform.position).normalized;
    }

    public void SetDamage(int damage)
    {
        _damage = damage;
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

    public void DestroyBullet()
    {
        if (!enabled) return;
        targetHit = true;
        Destroy(gameObject);
    }
}
