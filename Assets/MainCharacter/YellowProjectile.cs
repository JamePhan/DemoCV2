using System;
using System.Collections;
using UnityEngine;

public class YellowProjectile : MonoBehaviour
{
    public Vector3  _target;
    public int      _speed;
    public float    _time;
    public Spawner  _pool;

    private void OnEnable()
    {
        StartCoroutine(DestroyProjectile());
    }

    public void Init(Transform target, int speed, float time, Spawner pool)
    {
        _target = target.transform.position;
        _speed = speed;
        _time = time;
        _pool = pool;
    } 

    public void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
        Vector3 direction = (_target - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    IEnumerator DestroyProjectile()
    {
        yield return new WaitForSeconds(_time);
        _pool.Kill(gameObject);
    }
}
