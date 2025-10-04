using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class EnemyManager : UdonSharpBehaviour
{
    public GameObject[] enemies;     // ワールド上の全エネミーを登録
    public ProgressGate progressGate;        // ゴールゾーン参照

    [SerializeField]
    private int aliveCount;

    public void OnEnemyDefeated(Enemy enemy)
    {
        aliveCount--;

        if (aliveCount <= 0 && progressGate != null)
        {
            progressGate.Open();
        }
    }

    public void ResetEnemies()
    {

        aliveCount = enemies.Length;

        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.SetActive(true);
            }
        }

        if (progressGate != null)
        {
            progressGate.Close();
        }
    }
}
