using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class AudioTrigger : UdonSharpBehaviour
{
    public AudioSource audioSource; // 再生するAudioSource
    public bool isInAreaBGM = false;   // エリア内のみBGM流すか
                                       // private bool hasPlayed = false;

    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        if (!player.isLocal) return; // ローカルプレイヤーのみ反応

        // すでに再生中なら何もしない
        if (audioSource.isPlaying) return;

        audioSource.Play();
    }

    public override void OnPlayerTriggerExit(VRCPlayerApi player)
    {
        if (!player.isLocal) return;

        if (isInAreaBGM)
        {
            audioSource.Stop();
        }
    }
}