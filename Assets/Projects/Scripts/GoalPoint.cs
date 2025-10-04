using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

public class GoalPoint : UdonSharpBehaviour
{
    public GameManager gameManager;

    private Collider col;
    private Renderer rend;

    void Start()
    {
        //★これいらなくない？？
        col = GetComponent<BoxCollider>();
        rend = GetComponent<Renderer>();
    }

    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        if (!player.isLocal) return;

        gameManager.OnGoalZoneEntered(player);
    }
}
