using UnityEngine;

public interface IAttack
{
    int Damage { get; set; }
    float AttackSpeed { get; set; }
    float AttackRange { get; set; }
    LayerMask Target { get; set; }

    void Detect();

    void Rotate(Vector3 playerPosition);

    void Attack(Collider[] enemy);

}
