using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class StartZone : UdonSharpBehaviour
{
    public GameManager gameManager;
    private Collider zoneCollider;
    private Renderer zoneRenderer;

    void Start()
    {
        zoneCollider = GetComponent<BoxCollider>();
        zoneRenderer = GetComponent<Renderer>();
    }

    // 銃を持った時に呼ばれる
    public void OnGunPickup()
    {
        if (zoneCollider != null) zoneCollider.isTrigger = true;
        if (zoneRenderer != null) zoneRenderer.enabled = false;
    }

    // 銃を離した時に呼ばれる
    public void OnGunDrop()
    {
        if (zoneCollider != null) zoneCollider.isTrigger = false;
        if (zoneRenderer != null) zoneRenderer.enabled = true;
    }

    // ゲームスタート
    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        // 自分自身でなければ無視
        if (!player.isLocal) return;

        // LocalPlayer がスタートゾーンに入った時だけ計測開始
        gameManager.OnStartZoneEntered(player);
    }
}
