using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class EnemyManager : UdonSharpBehaviour
{
    public GameObject[] enemies;     // ワールド上の全エネミーを登録
    public GoalZone goalZone;        // ゴールゾーン参照

    private int aliveCount;

    public void OnEnemyDefeated(Enemy enemy)
    {
        aliveCount--;

        if (aliveCount <= 0 && goalZone != null)
        {
            goalZone.Goal(); // ゴール解放
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

        // ゴールを閉じる
        if (goalZone != null)
        {
            goalZone.ResetGoal();
        }
    }
}
