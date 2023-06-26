using DG.Tweening;
using UnityEngine;

public class BranchingRail : Rail
{
    [SerializeField] private Rail rightWayRail;
    [SerializeField] private Rail leftWayRail;

    public void AllowRightWay()
    {
        rightWayRail.SetLicenseToMove(true);
        leftWayRail.SetLicenseToMove(false);
    }

    public void AllowLeftWay()
    {
        rightWayRail.SetLicenseToMove(false);
        leftWayRail.SetLicenseToMove(true);
    }
}
