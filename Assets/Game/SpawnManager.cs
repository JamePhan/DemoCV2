using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public delegate void PlayerPositionDelegate(Vector3 playerPosition);
    public static event PlayerPositionDelegate playerPosDelegate;

    [Header("Enemy Prefab")]
    public      List<GameObject>                ListEnemyPrefabs = new List<GameObject>();
    public      Dictionary<string, Spawner>     DictEnemyPrefabs = new Dictionary<string, Spawner>();

    [Header("Other")]
    public      float                           _radius;
    public      GameObject                      _player;
    public      LayerMask                       _layerMask;
    
    public      Material                        _damageFlash;

    protected override void InAwake()
    {
        Init();
    }

    public void Init()
    {
        GameManager.resetGameDelegate += IsGameReset;
        InitDictEnemySpawn();
        _radius = 40f;
    }

    public void InitDictEnemySpawn()
    {
        foreach (var enemy in ListEnemyPrefabs)
        {
            Spawner _spawner = transform.AddComponent<Spawner>();
            DictEnemyPrefabs.Add(enemy.name, _spawner);
            _spawner.Init(enemy, true, 10);
        }
    }

    void FixedUpdate()
    {
        playerPosDelegate?.Invoke(_player.transform.position);
    }

    public void IsGameReset()
    {
        playerPosDelegate = null;
    }

    public void SpawnMonsters(string enemy, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            SpawnMonster(DictEnemyPrefabs[enemy], Enemy.GetEnemy(enemy));
        }
    }

    private void SpawnMonster(Spawner _spawner, Enemy enemy)
    {
        GameObject enemyInit = _spawner.Spawn();
        enemyInit.transform.position = GetSpawnPosition(_player.transform.position, _radius);
        enemyInit.layer = LayerMask.NameToLayer("Enemy");

        if (!enemyInit.TryGetComponent<EnemyBehaviour>(out var enemyBehaviour))
        {
            enemyBehaviour = enemyInit.AddComponent<EnemyBehaviour>();
            enemyBehaviour.Init(enemy, _layerMask, _damageFlash, _spawner);
            playerPosDelegate += enemyBehaviour.Move;
            GameManager.resetGameDelegate += enemyBehaviour.IsGameReset;
        }

    }

    public Vector3 GetSpawnPosition(Vector3 target, float radius)
    {
        Vector2 randomPoint2D = Random.insideUnitCircle * radius;
        Vector3 randomPoint3D = new Vector3(randomPoint2D.x, 0, randomPoint2D.y);
        return target + randomPoint3D;
    }
}
