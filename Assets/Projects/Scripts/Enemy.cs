using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Enemy : UdonSharpBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        // 衝突相手に Bullet コンポーネントが付いているかチェック
        Bullet bullet = other.gameObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            // 弾と自分を削除
            Destroy(gameObject);  // 自分(敵)を消す
            Destroy(other.gameObject);  // 弾を消す
        }
    }
}
