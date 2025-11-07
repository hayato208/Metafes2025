using UdonSharp;
using UnityEngine;

public class GateController : UdonSharpBehaviour
{
    [SerializeField] private GameObject gateObject; // 開閉対象のオブジェクト
    [SerializeField] private SoundEffectPlayer soundEffectPlayer; // サウンド

    public bool oneTimeOnly = false; // Inspectorで設定可能
    private bool hasOpened = false; // このオブジェクトで一度でも開いたかどうか

    public void Open()
    {
        if (gateObject != null)
        {
            gateObject.SetActive(false); // 例: 非表示で「通れる」

            // ワールド通じて1回のみ&未開放
            // 銃をピックアップした1回目のみどうさせる
            if (oneTimeOnly && hasOpened) return;

            // 扉を開ける音
            soundEffectPlayer.SendCustomEvent("PlaySE");
            hasOpened = true; // 開いた状態を記録
        }
    }

    public void Close()
    {
        if (gateObject != null)
        {
            gateObject.SetActive(true); // 例: 有効化で「塞ぐ」
        }
    }
}
