using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class SoundEffectPlayer : UdonSharpBehaviour
{
    [Header("再生する効果音（AudioSourceを指定）")]
    public AudioSource audioSource;
    // private bool hasPlayed = false;

    // 外部から呼び出す用のメソッド
    public void PlaySE()
    {
        if (audioSource == null) return;

        audioSource.Play();
      //  hasPlayed = true;
    }
}
