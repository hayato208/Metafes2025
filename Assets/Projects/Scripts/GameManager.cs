using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using TMPro; // TextMeshPro 用

public class GameManager : UdonSharpBehaviour
{
    private float startTime;
    private bool isRunning;

    public TextMeshPro timeText; // ワールド上の看板テキストに割り当て

    public void StartTimer()
    {
        startTime = Time.time;
        isRunning = true;

        if (timeText != null)
        {
            timeText.text = "計測中...";
        }
    }

    public void StopTimer()
    {
        if (!isRunning) return;

        isRunning = false;
        float totalTime = Time.time - startTime;

        if (timeText != null)
        {
            timeText.text = $"クリアタイム: {totalTime:F2} 秒";
        }

        Debug.Log($"クリアタイム: {totalTime:F2} 秒");
    }
}
