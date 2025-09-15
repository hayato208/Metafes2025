using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DebugOverlay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI debugText;
    [SerializeField] private int maxLines = 15;
    [SerializeField] private string filterKeyword = "��DUI:"; // ���̃L�[���[�h���܂ރ��O�̂ݕ\��

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
        // �L�[���[�h���܂܂Ȃ����O�͖���
        if (!logString.Contains(filterKeyword)) return;

        // ���O���L���[�ɒǉ�
        logQueue.Enqueue(logString);

        // �s������
        while (logQueue.Count > maxLines)
        {
            logQueue.Dequeue();
        }

        // �\�����X�V
        debugText.text = string.Join("\n", logQueue.ToArray());
    }
}
