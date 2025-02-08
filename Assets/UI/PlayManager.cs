using UnityEngine;
using UnityEngine.UI;

public class PlayManager : Singleton<PlayManager>
{
    [Header("Preference")]
    public Button       btn_Play;
    public Camera       _camera;
    public GameObject   _UI_OutGame;
    public GameObject   _UI_InGame;

    protected override void InAwake()
    {
        btn_Play.onClick.AddListener(Play);
        GameManager.resetGameDelegate += ResetSpawnEnemy;
    }

    public void Play()
    {
        _UI_OutGame.SetActive(false);
        _camera.GetComponent<Animator>().Play("InGame");
        _UI_InGame.SetActive(true);
        GameManager.Instance.ResetGameTime();
        GameManager.Instance.InitPlayer();
        InvokeRepeating("SpawnFemale2Enemy", 5f, 15f);
        InvokeRepeating("SpawnFemale1Enemy", 1f, 12f);
        InvokeRepeating("SpawnMale2Enemy", 0f, 7f);
        InvokeRepeating("SpawnSoldier", 25f, 40f);
        InvokeRepeating("SpawnTerrorist", 15f, 15f);
        InvokeRepeating("SpawnFlameThrower", 10f, 20f);

        InvokeRepeating("SpawnMale2Group", 60f, 60f);
        InvokeRepeating("SpawnSoldierGroup", 150f, 120f);
        InvokeRepeating("SpawnTerroristGroup", 90f, 90f);

    }

    public void SpawnFemale2Enemy()
    {
        SpawnManager.Instance.SpawnMonsters("Female_2", 1);
    }

    public void SpawnFemale1Enemy()
    {
        SpawnManager.Instance.SpawnMonsters("Female_1", 1);
    }

    public void SpawnMale2Enemy()
    {
        SpawnManager.Instance.SpawnMonsters("MaleA_2", 1);
    }

    public void SpawnSoldier()
    {
        SpawnManager.Instance.SpawnMonsters("Soldier", 1);
    }

    public void SpawnTerrorist()
    {
        SpawnManager.Instance.SpawnMonsters("Terrorist", 1);
    }

    public void SpawnFlameThrower()
    {
        SpawnManager.Instance.SpawnMonsters("FlameThrower", 1);
    }

    public void SpawnMale2Group()
    {
        SpawnManager.Instance.SpawnMonsters("MaleA_2", 7);
    }

    public void SpawnSoldierGroup()
    {
        SpawnManager.Instance.SpawnMonsters("Soldier", 3);
    }

    public void SpawnTerroristGroup()
    {
        SpawnManager.Instance.SpawnMonsters("Terrorist", 5);
    }

    public void ResetSpawnEnemy()
    {
        CancelInvoke();
        _UI_InGame.SetActive(false);
        _camera.GetComponent<Animator>().Play("OutGame");
        _UI_OutGame.SetActive(true);
    }
}
