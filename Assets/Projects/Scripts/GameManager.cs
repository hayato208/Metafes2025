using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using TMPro;

public class GameManager : UdonSharpBehaviour
{
    [SerializeField]
    private float[] playerStartTimes; // プレイヤーごとの開始時間
   // private bool[] isRunning;         // 計測中プレイヤーかどうか
    private int maxPlayers = 80;      // VRChatの上限人数

    public EnemyManager enemyManager;
    public GameObject startWall;
    public GameObject goalWall;

    public TextMeshPro timeText;
    public ScoreManager scoreManager; // ハイスコア管理用

    void Start()
    {
        playerStartTimes = new float[maxPlayers];
       // isRunning = new bool[maxPlayers];
        for (int i = 0; i < maxPlayers; i++)
        {
            playerStartTimes[i] = -1f; // 未計測を -1 とする
           // isRunning[i] = false;
        }
    }

    public void OnStartZoneEntered(VRCPlayerApi player)
    {
        int id = player.playerId;
        if (id < 0 || id >= maxPlayers) return;

        playerStartTimes[id] = Time.time;
       // isRunning[id] = true;

        if (timeText != null)
        {
            timeText.text = $"{player.displayName} 計測開始！";
        }

        startWall.SetActive(false);
        goalWall.SetActive(true);
        enemyManager.ResetEnemies();
    }

    public void OnGoalZoneEntered(VRCPlayerApi player)
    {
        int id = player.playerId;
        if (id < 0 || id >= maxPlayers) return;
       // if (!isRunning[id]) return; // 計測中でなければ無視

        float totalTime = Time.time - playerStartTimes[id];
       // isRunning[id] = false;

        if (timeText != null)
        {
            timeText.text = $"{player.displayName} のクリアタイム: {totalTime:F2} 秒";
        }

        startWall.SetActive(true);
        goalWall.SetActive(false);

        // ハイスコア更新
        scoreManager.RecordTime(player, totalTime);
    }
}
