using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class GameManager : UdonSharpBehaviour
{
    public float startTime;
    public float endTime;
    public bool isRunning = false;

    void Update()
    {
        if (isRunning)
        {
            float elapsed = Time.time - startTime;
            // デバッグ用に表示（VRChatではUI Textで表示できる）
            Debug.Log("Elapsed Time: " + elapsed.ToString("F2"));
        }
    }

    public void StartTimer()
    {
        if (!isRunning)
        {
            startTime = Time.time;
            isRunning = true;
            Debug.Log("Timer Started!");
        }
    }

    public void StopTimer()
    {
        if (isRunning)
        {
            endTime = Time.time;
            isRunning = false;
            Debug.Log("Timer Stopped! Total: " + (endTime - startTime).ToString("F2"));
        }
    }
}
