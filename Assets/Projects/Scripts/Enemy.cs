using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Enemy : UdonSharpBehaviour
{
    public EnemyManager enemyManager;

    void OnCollisionEnter(Collision other)
    {
        // 衝突相手に Bullet コンポーネントが付いているかチェック
        Bullet bullet = other.gameObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            // 弾と自分を削除
            gameObject.SetActive(false);
            Destroy(other.gameObject);

            // 敵マネージャーに通知
            if (enemyManager != null)
            {
                enemyManager.OnEnemyDefeated(this);
            }
        }
    }
}
