using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

public class GoalZone : UdonSharpBehaviour
{
    public GameManager gameManager;

    private Collider col;
    private Renderer rend;

    void Start()
    {
        col = GetComponent<BoxCollider>();
        rend = GetComponent<Renderer>();
    }

    public void Goal()
    {
        if (col != null) col.isTrigger = true;
        if (rend != null) rend.enabled = false;
    }
    public void ResetGoal()
    {
        if (col != null) col.isTrigger = false;
        if (rend != null) rend.enabled = true;
    }

    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        gameManager.OnGoalZoneEntered(player);
    }
}
