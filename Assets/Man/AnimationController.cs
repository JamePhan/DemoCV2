using UnityEngine;

public class AnimationController
{
    public Animator animator;
    public EnemyType type;

    public AnimationController(Animator animator)
    {
        this.animator = animator;
    }

    public AnimationController(Animator animator, EnemyType type)
    {
        this.animator = animator;
        this.type = type;
    }

    public void Idle()
    {
        switch (type)
        {
            case EnemyType.Male:
                MaleIdle();
                break;

            case EnemyType.Female:
                FemaleIdle();
                break;

            case EnemyType.Soldier:
                SoldierIdle();
                break;
        }
    }

    public void Run()
    {
        switch (type)
        {
            case EnemyType.Male:
                MaleRun();
                break;

            case EnemyType.Female:
                FemaleWalk();
                break;

            case EnemyType.Soldier:
                SoldierRun();
                break;
        }
    }

    public void Die()
    {
        switch (type)
        {
            case EnemyType.Male:
                MaleDie();
                break;

            case EnemyType.Female:
                FemaleDie();
                break;

            case EnemyType.Soldier:
                SoldierDie();
                break;
        }
    }

    public void Attack()
    {
        switch (type)
        {
            case EnemyType.Male:

                break;

            case EnemyType.Female:

                break;

            case EnemyType.Soldier:
                SoldierShoot();
                break;
        }
    }

    public void MaleIdle()
    {
        animator.Play("m_idle_A");
    }

    public void MaleRun()
    {
        
    }

    public void MaleDie()
    {
        animator.Play("m_death_A");
    }


    public void FemaleIdle()
    {

    }

    public void FemaleWalk()
    {
        animator.Play("f_walk_rm");
    }

    public void FemaleDie()
    {
        animator.Play("f_death_A");
    }


    public void SoldierIdle()
    {
        animator.Play("Soldier_Idle");
    }

    public void SoldierRun()
    {
        animator.Play("Soldier_Run");
    }

    public void SoldierDie()
    {
        animator.Play("Soldier_Die");
    }


    public void MainCharacterIdle()
    {
        animator.Play("m_idle_A");
    }

    public void MainCharacterRun()
    {
        animator.Play("m_pistol_run");
    }

    public void MainCharacterShoot()
    {
        animator.Play("m_pistol_shoot");
    }

    public void SoldierShoot()
    {
        animator.Play("Soldier_Shoot");
    }
}
