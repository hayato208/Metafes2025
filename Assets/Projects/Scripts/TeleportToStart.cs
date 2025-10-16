
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class TeleportToStart : UdonSharpBehaviour
{
    public Transform startPoint; // スタート地点（Unity上でアサイン）

    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        if (!player.isLocal) return;

        // ワープ処理
        player.TeleportTo(startPoint.position, startPoint.rotation);
    }
}