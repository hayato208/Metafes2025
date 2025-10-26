using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[RequireComponent(typeof(MeshRenderer))]
public class TextureScaler : UdonSharpBehaviour
{
    void Start()
    {
        MeshRenderer mr = (MeshRenderer)GetComponent(typeof(MeshRenderer));
        if (mr == null) return;

        Material mat = mr.material;
        if (mat == null) return;

        Texture tex = mat.mainTexture;
        if (tex == null) return;

        // テクスチャのアスペクト比（横 / 縦）
        float aspect = (float)tex.width / tex.height;

        // 現在のスケールを取得
        Vector3 currentScale = transform.localScale;

        // 高さ（Y）を基準に横幅（X）を比率に合わせて調整
        float newX = currentScale.y * aspect;
        transform.localScale = new Vector3(newX, currentScale.y, currentScale.z);
    }
}
