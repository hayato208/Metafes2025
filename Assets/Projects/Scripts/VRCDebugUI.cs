using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using VRC.SDKBase;

public class VRCDebugUI : UdonSharpBehaviour
{
    [SerializeField] private TextMeshProUGUI debugText; // 画面左上にアタッチ（World Space Canvas 内の Text）
    [SerializeField] private int maxLines = 10;         // 表示する最大行数
    [SerializeField] private Vector3 offset = new Vector3(0, 0, 0.5f); // 頭の前に出すオフセット

    private string[] logBuffer;
    private int logIndex = 0;

    void Start()
    {
        logBuffer = new string[maxLines];
        for (int i = 0; i < maxLines; i++)
        {
            logBuffer[i] = "";
        }
    }

    void Update()
    {
        // 自分の頭の位置を取得
        VRCPlayerApi localPlayer = Networking.LocalPlayer;
        if (localPlayer == null) return;

        VRCPlayerApi.TrackingData head = localPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head);

        // デバッグUIを頭の前に追従させる
        transform.position = head.position + head.rotation * offset;
        transform.rotation = head.rotation;
    }

    /// <summary>
    /// デバッグメッセージをUIに出す
    /// </summary>
    public void Print(string message)
    {
        // ログをリングバッファに格納
        logBuffer[logIndex] = message;
        logIndex = (logIndex + 1) % maxLines;

        // UIに反映
        string result = "";
        for (int i = 0; i < maxLines; i++)
        {
            int idx = (logIndex + i) % maxLines;
            if (!string.IsNullOrEmpty(logBuffer[idx]))
            {
                result += logBuffer[idx] + "\n";
            }
        }
        if (debugText != null)
        {
            debugText.text = result;
        }
    }
}
