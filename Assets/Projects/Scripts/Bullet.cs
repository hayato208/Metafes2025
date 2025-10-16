using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Bullet : UdonSharpBehaviour
{
    public float lifeTime = 5f; // 弾の寿命（秒）

    void Start()
    {
        // 一定時間後に自動消滅
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision other)
    {
        // 何かに当たったら消滅
        Destroy(gameObject);
    }
}
