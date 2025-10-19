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
    private BoxCollider boxCollider;  // 銃のBoxCollider参照用

    private VRC_Pickup pickup;

    // どちらの手で持っているかを記録する変数
    [SerializeField]
    private int currentHand = -1; // 0=左, 1=右, -1=未保持

    void Start()
    {
        localPlayer = Networking.LocalPlayer;
        boxCollider = GetComponent<BoxCollider>();
        pickup = GetComponent<VRC_Pickup>();
    }

    public override void OnPickup()
    {
        lastPickupTime = Time.time;


        // どちらの手で持たれたか判定
        if (pickup.currentHand == VRC_Pickup.PickupHand.Left)
        {
            currentHand = 0;
        }
        else if(pickup.currentHand == VRC_Pickup.PickupHand.Right)
        {
            // デスクトップモードもこちらの検出
            // ★別途デスクトップモードを識別する処理を書く
            currentHand = 1;
        }

        // 銃を持ったらコライダー無効化
        if (boxCollider != null)
        {
            boxCollider.enabled = false;
        }
    }

    public override void OnDrop()
    {
        // 銃を放したらコライダー有効化
        if (boxCollider != null)
        {
            boxCollider.enabled = true;
        }
    }

    /// <summary>
    /// 銃の射撃
    /// </summary>
    /// <param name="value"></param>
    /// <param name="args"></param>
    public override void InputUse(bool value, VRC.Udon.Common.UdonInputEventArgs args)
    {
        // ★バグとしてInputUseがマウスクリックで2回動作するので、その対応は必要
        if (!value) return; // ボタン押下時のみ
        if (localPlayer == null || !IsHeldByLocalPlayer()) return;

        // どちらの手のトリガー入力かチェック
        if (currentHand == 0 && args.handType != VRC.Udon.Common.HandType.LEFT) return; // 左手専用
        if (currentHand == 1 && args.handType != VRC.Udon.Common.HandType.RIGHT) return; // 右手専用

        // クールダウン中は撃てない
        if (Time.time - lastPickupTime < pickupCooldown) return;

        Fire();
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
