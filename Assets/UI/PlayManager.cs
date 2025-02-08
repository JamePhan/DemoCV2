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
        Invoke("AutoSpawnEnemy", 0f);
    }

    public void AutoSpawnEnemy()
    {
        //SpawnManager.Instance.SpawnMonsters("Female_2", 2);
        //SpawnManager.Instance.SpawnMonsters("Female_1", 2);
        //SpawnManager.Instance.SpawnMonsters("MaleA_1", 2);
        //SpawnManager.Instance.SpawnMonsters("Soldier", 1);
        SpawnManager.Instance.SpawnMonsters("FlameThrower", 1);
    }

    public void ResetSpawnEnemy()
    {
        CancelInvoke();
        _UI_InGame.SetActive(false);
        _camera.GetComponent<Animator>().Play("OutGame");
        _UI_OutGame.SetActive(true);
    }
}
