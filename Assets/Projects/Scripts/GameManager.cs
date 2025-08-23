using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using TMPro; // TextMeshPro 用

public class GameManager : UdonSharpBehaviour
{
    private float startTime;
    private bool isRunning;

    public EnemyManager enemyManager;
    public GameObject startWall;
    public GameObject goalWall;

    // ワールド上の看板テキストに割り当て
    public TextMeshPro timeText; 

    public void StartTimer()
    {
        startTime = Time.time;
        isRunning = true;

        if (timeText != null)
        {
            timeText.text = "計測中...";
        }
    }

    public void OnStartZoneEntered(VRCPlayerApi player)
    {
        startWall.SetActive(false);
        goalWall.SetActive(true);
        enemyManager.ResetEnemies();

        startTime = Time.time;
        isRunning = true;
    }

    public void OnGoalZoneEntered(VRCPlayerApi player)
    {
        if (!isRunning) return;

        float totalTime = Time.time - startTime;

        isRunning = false;
        goalWall.SetActive(false);
        startWall.SetActive(true);

        if (timeText != null)
        {
            timeText.text = $"クリアタイム: {totalTime:F2} 秒";
        }
    }
}
