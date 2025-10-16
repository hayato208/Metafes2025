using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class GunPickupHandler : UdonSharpBehaviour
{
    public StartPoint startZone;

    public override void OnPickup()
    {
        if (startZone != null)
        {
            startZone.OnGunPickup();
        }
    }

    public override void OnDrop()
    {
        if (startZone != null)
        {
            startZone.OnGunDrop();
        }
    }
}
