using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    // --- Config ---
    public float speed = 100;
    public LayerMask collisionLayerMask;

    // --- Explosion VFX ---
    public GameObject rocketExplosion;

    // --- Projectile Mesh ---
    public MeshRenderer projectileMesh;

    // --- Script Variables ---
    private bool targetHit;

    // --- Audio ---
    //public AudioSource inFlightAudioSource;

    // --- VFX ---
    public ParticleSystem disableOnHit;

    public Vector3 _target;
    public int _damage;

    private void FixedUpdate()
    {
        // --- Check to see if the target has been hit. We don't want to update the position if the target was hit ---
        if (targetHit) return;

        transform.position = Vector3.MoveTowards(transform.position, _target, speed * Time.deltaTime);
        Vector3 direction = (_target - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 0.5f, LayerMask.GetMask("Player", "Wall")))
        {
            if (hit.transform == null) return;

            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Collider[] newCol = new Collider[1] { hit.collider };
                Attack(newCol);
            }
            Explosion();
        }
        if (transform.position == _target) Explosion();
    }



    public void SetTarget(Vector3 target)
    {
        _target = new Vector3(target.x, target.y +1f, target.z);
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

    public void Explosion()
    {
        if (!enabled) return;


        // --- Explode when hitting an object and disable the projectile mesh ---
        Explode();
        projectileMesh.enabled = false;
        targetHit = true;
        SoundManager.Instance.RocketExplosion();
        foreach (Collider col in GetComponents<Collider>())
        {
            col.enabled = false;
        }
        disableOnHit.Stop();

        // --- Destroy this object after 2 seconds. Using a delay because the particle system needs to finish ---
        Destroy(gameObject, 5f);
    }

    /// <summary>
    /// Instantiates an explode object.
    /// </summary>
    private void Explode()
    {
        // --- Instantiate new explosion option. I would recommend using an object pool ---
        GameObject newExplosion = Instantiate(rocketExplosion, transform.position, rocketExplosion.transform.rotation, null);


    }
}
