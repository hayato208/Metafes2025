using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

public class ScoreManager : UdonSharpBehaviour
{
    [SerializeField]
    private float[] bestTimes; // プレイヤーIDごとのベストタイム（シンプルに配列で管理）
    private int maxPlayers = 80; // VRChatの上限人数

    [Header("プレイヤーごとのハイスコア表示")]
    public TextMeshPro playerScoreText;

    void Start()
    {
        bestTimes = new float[maxPlayers];
        for (int i = 0; i < maxPlayers; i++)
        {
            bestTimes[i] = Mathf.Infinity;
        }
    }

    public void RecordTime(VRCPlayerApi player, float time)
    {
        int id = player.playerId;
        if (time < bestTimes[id])
        {
            bestTimes[id] = time;
            UpdateDisplay(player, bestTimes[id]);
        }
    }

    private void UpdateDisplay(VRCPlayerApi player, float time)
    {
        if (playerScoreText != null)
        {
            playerScoreText.text = $"{player.displayName} : {time:F2} 秒";
        }
    }
}
