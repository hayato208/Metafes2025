using UdonSharp;
using UnityEngine;

public class EnemyGroup : UdonSharpBehaviour
{
    [SerializeField] private GameObject[] enemies;   // このグループに属する敵
    [SerializeField] private ProgressGate progressGate; // 関連付けられた進行ゲート
    private int aliveCount;

    void Update()
    {
        aliveCount = 0;

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null && enemies[i].activeSelf)
            {
                aliveCount++;
            }
        }

        // 生存数が0になった瞬間にゴール解放
        if (aliveCount == 0)
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
