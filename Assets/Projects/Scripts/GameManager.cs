using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : UdonSharpBehaviour
{
    [SerializeField]
    private float[] playerStartTimes; // プレイヤーごとの開始時間
    private int maxPlayers = 80;      // VRChatの上限人数

    public EnemyManager[] enemyManagers;
    public GameObject startWall;
    public GameObject goalWall;

    public TextMeshPro timeText;
    public ScoreManager scoreManager; // ハイスコア管理用

    public VRCDebugUI debugUI;

    void Start()
    {
        playerStartTimes = new float[maxPlayers];
        for (int i = 0; i < maxPlayers; i++)
        {
            playerStartTimes[i] = -1f; // 未計測を -1 とする
        }
    }

    public void OnStartZoneEntered(VRCPlayerApi player)
    {
        int id = player.playerId;
        if (id < 0 || id >= maxPlayers) return;

        playerStartTimes[id] = Time.time;

        if (timeText != null)
        {
            debugUI.Print(id + "★DUI:プレイヤーID：" + id.ToString());
            debugUI.Print(id + "★DUI:開始タイム：" + playerStartTimes[id]);

            timeText.text = $"{player.displayName} 計測開始！";
        }

        startWall.SetActive(false);
        goalWall.SetActive(true);
        foreach (var manager in enemyManagers)
        {
            if (manager != null)
                manager.ResetEnemies();
        }
    }

    public void OnGoalZoneEntered(VRCPlayerApi player)
    {
        int id = player.playerId;
        if (id < 0 || id >= maxPlayers) return;

        float totalTime = Time.time - playerStartTimes[id];

        if (timeText != null)
        {
            timeText.text = $"{player.displayName} のクリアタイム: {totalTime:F2} 秒";
            debugUI.Print(id + "★DUI:Time.time：" + Time.time);
            debugUI.Print(id + "★DUI:クリアタイム：" + totalTime);
        }

        startWall.SetActive(true);
        goalWall.SetActive(false);

        // ハイスコア更新
        scoreManager.RecordTime(player, totalTime);
    }
}
