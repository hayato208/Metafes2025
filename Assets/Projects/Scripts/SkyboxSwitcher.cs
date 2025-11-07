using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class SkyboxSwitcher : UdonSharpBehaviour
{
    [Header("切り替え先のSkyboxマテリアル")]
    public Material newSkybox;

    private Material originalSkybox;
    private bool isChanged = false;

    void Start()
    {
        // 元のSkyboxを保存
        originalSkybox = RenderSettings.skybox;
    }

    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        // ローカルプレイヤーのみ反応
        if (!player.isLocal) return;

        if (!isChanged && newSkybox != null)
        {
            RenderSettings.skybox = newSkybox;
            isChanged = true;
        }
    }

    public override void OnPlayerTriggerExit(VRCPlayerApi player)
    {
        // ローカルプレイヤーのみ反応
        if (!player.isLocal) return;

        if (isChanged && originalSkybox != null)
        {
            RenderSettings.skybox = originalSkybox;
            isChanged = false;
        }
    }
}
