using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ResetButton : UdonSharpBehaviour
{
    [Header("リセット対象オブジェクト")]
    public GameObject[] targetObjects;

    [Header("リセット位置・回転")]
    public Transform[] resetTransforms;

    public override void Interact()
    {
        if (targetObjects == null || resetTransforms == null) return;
        if (targetObjects.Length != resetTransforms.Length) return; // 配列の長さが一致している必要あり

        for (int i = 0; i < targetObjects.Length; i++)
        {
            if (targetObjects[i] == null || resetTransforms[i] == null) continue;

            // 所定の位置と回転にリセット
            targetObjects[i].transform.SetPositionAndRotation(
                resetTransforms[i].position,
                resetTransforms[i].rotation
            );
        }
    }
}
