using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class StartZone : UdonSharpBehaviour
{
    public GameManager gameManager;
    private Collider zoneCollider;
    private Renderer zoneRenderer;
    public EnemyManager enemyManager;

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

        gameManager.OnStartZoneEntered(player);
    }
}
