using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class StartPoint : UdonSharpBehaviour
{
    public GameManager gameManager;
    public GateController[] gateControllers;        // 中間ポイントやゴール地点など

    // 銃を持った時に呼ばれる
    public void OnGunPickup()
    {
        foreach (var gate in gateControllers)
        {
            gate.Open();
        }
    }

    // 銃を離した時に呼ばれる
    public void OnGunDrop()
    {
        foreach (var gate in gateControllers)
        {
            gate.Close();
        }
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
