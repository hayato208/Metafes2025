using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class EnemyManager : UdonSharpBehaviour
{
    public GameObject[] enemies;     // ワールド上の全エネミーを登録
    public GoalZone goalZone;        // ゴールゾーン参照

    void Start()
    {
        // 初期状態ではゴールを無効化
        // ★エネミーが存在していれば、すなわちStartLineを超えたタイミングでエネミーを召喚する、その数が1体以上であれば壁を出現する
        if (goalZone != null)
        {
           // goalZone.SetActive(false);
        }
    }

    // ★デバッグ用の処理、実際はUpdeteでは回さない
    void Update()
    {
        // 残っているかチェック
        bool allGone = true;
        foreach (GameObject e in enemies)
        {
            if (e != null && e.activeSelf)
            {
                allGone = false;
                break;
            }
        }

        if (allGone && goalZone != null)
        {
            goalZone.Goal();
        }
    }

    /*
    ★エネミー消失時に発火させるために、Enemyに持たせるのがよいか？
    public void OnEnemyDestroyed(GameObject enemy)
    {
        // 配列から1体減った扱いにする
        enemy.SetActive(false);

        // 残っているかチェック
        bool allGone = true;
        foreach (GameObject e in enemies)
        {
            if (e != null && e.activeSelf)
            {
                allGone = false;
                break;
            }
        }

        if (allGone && goalZone != null)
        {
            goalZone.SetActive(true); // ゴール解放
        }
    }
    */
}
