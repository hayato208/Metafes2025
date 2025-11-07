using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class StartPoint : UdonSharpBehaviour
{
    public GameManager gameManager;
    public GateController gateController;        // 中間ポイントやゴール地点など

    // 銃を持った時に呼ばれる
    public void OnGunPickup()
    {
        gateController.Open();
    }

    // 銃を離した時に呼ばれる
    public void OnGunDrop()
    {
        gateController.Close();
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
