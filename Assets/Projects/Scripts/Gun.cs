using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Gun : UdonSharpBehaviour
{
    public GameObject bulletPrefab;   // 弾丸プレハブ
    public Transform firePoint;       // 発射位置
    public float bulletSpeed = 20f;   // 弾速

    private float pickupCooldown = 0.2f; // 0.2秒だけ撃てない
    private float lastPickupTime = 0f;
    private VRCPlayerApi localPlayer;

    void Start()
    {
        localPlayer = Networking.LocalPlayer;
    }

    public override void OnPickup()
    {
        lastPickupTime = Time.time;
    }

    void Update()
    {
        // 銃をプレイヤーが持っている時だけ入力を受け付ける
        if (localPlayer == null || !IsHeldByLocalPlayer()) return;

        // クールダウン中は撃てない
        if (Time.time - lastPickupTime < pickupCooldown) return;

        // 左クリック or VRトリガー
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    private bool IsHeldByLocalPlayer()
    {
        // VRC_Pickup コンポーネントを取得
        VRC_Pickup pickup = (VRC_Pickup)GetComponent(typeof(VRC_Pickup));
        if (pickup == null) return false;

        return pickup.IsHeld && pickup.currentPlayer == localPlayer;
    }

    public void Fire()
    {
        if (bulletPrefab == null || firePoint == null) return;

        GameObject bullet = VRCInstantiate(bulletPrefab);
        bullet.transform.SetPositionAndRotation(firePoint.position, firePoint.rotation);
        bullet.transform.SetParent(null);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * bulletSpeed;
        }
    }
}
