using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class EnemyManager : UdonSharpBehaviour
{
    public GameObject[] enemies;     // ワールド上の全エネミーを登録
    public GateController[] gateControllers;        // 中間ポイントやゴール地点など

    [SerializeField]
    private int aliveCount;

    public void OnEnemyDefeated(Enemy enemy)
    {
        aliveCount--;

        if (aliveCount <= 0 && gateControllers != null)
        {
            foreach (var gate in gateControllers)
            {
                gate.Open();
            }
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

        if (gateControllers != null)
        {
            foreach (var gate in gateControllers)
            {
                gate.Close();
            }
        }
    }
}
