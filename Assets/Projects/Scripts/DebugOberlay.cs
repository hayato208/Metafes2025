using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DebugOverlay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI debugText;
    [SerializeField] private int maxLines = 15;
    [SerializeField] private string filterKeyword = "★DUI:"; // このキーワードを含むログのみ表示

    private Queue<string> logQueue = new Queue<string>();

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        // キーワードを含まないログは無視
        if (!logString.Contains(filterKeyword)) return;

        // ログをキューに追加
        logQueue.Enqueue(logString);

        // 行数制御
        while (logQueue.Count > maxLines)
        {
            logQueue.Dequeue();
        }

        // 表示を更新
        debugText.text = string.Join("\n", logQueue.ToArray());
    }
}
