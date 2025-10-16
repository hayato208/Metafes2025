using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ScoreManager : UdonSharpBehaviour
{
    private const int maxPlayers = 80;

    [Header("ランキング表示")]
    public TextMeshProUGUI playerNamesText; // 左列（プレイヤー名）
    public TextMeshProUGUI scoresText;      // 右列（スコア）

    [UdonSynced] private float[] bestTimes = new float[maxPlayers];
    [UdonSynced] private string[] playerNames = new string[maxPlayers];

    void Start()
    {
        bestTimes = new float[maxPlayers];
        playerNames = new string[maxPlayers];
        for (int i = 0; i < maxPlayers; i++)
        {
            bestTimes[i] = Mathf.Infinity;
            playerNames[i] = "";
        }
    }

    public void RecordTime(VRCPlayerApi player, float time)
    {
        int id = player.playerId;

        // オーナーをこのプレイヤーに切り替える
        Networking.SetOwner(player, gameObject);

        if (time < bestTimes[id])
        {
            bestTimes[id] = time;
            playerNames[id] = player.displayName;

            // 全員に同期
            RequestSerialization();

            // ローカル表示更新
            UpdateRanking();
        }
    }

    public override void OnDeserialization()
    {
        // 他プレイヤーから同期されたときもランキング更新
        UpdateRanking();
    }

    private void UpdateRanking()
    {
        // 配列コピー
        float[] timesCopy = (float[])bestTimes.Clone();
        string[] namesCopy = (string[])playerNames.Clone();

        // 昇順ソート（バブルソート）
        for (int i = 0; i < timesCopy.Length - 1; i++)
        {
            for (int j = i + 1; j < timesCopy.Length; j++)
            {
                if (timesCopy[j] < timesCopy[i])
                {
                    float tmpTime = timesCopy[i];
                    timesCopy[i] = timesCopy[j];
                    timesCopy[j] = tmpTime;

                    string tmpName = namesCopy[i];
                    namesCopy[i] = namesCopy[j];
                    namesCopy[j] = tmpName;
                }
            }
        }

        // テキスト作成
        string namesColumn = "";
        string scoresColumn = "";

        for (int i = 0; i < timesCopy.Length; i++)
        {
            if (timesCopy[i] == Mathf.Infinity) continue;

            namesColumn += $"{i + 1}. {namesCopy[i]}\n";
            scoresColumn += $"{timesCopy[i]:F2} 秒\n";
        }

        if (playerNamesText != null) playerNamesText.text = namesColumn;
        if (scoresText != null) scoresText.text = scoresColumn;
    }
}