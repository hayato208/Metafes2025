using UdonSharp;
using UnityEngine;

public class ProgressGate : UdonSharpBehaviour
{
    [SerializeField] private GameObject gateObject; // 開閉対象のオブジェクト

    public void Open()
    {
        if (gateObject != null)
        {
            gateObject.SetActive(false); // 例: 非表示で「通れる」
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
