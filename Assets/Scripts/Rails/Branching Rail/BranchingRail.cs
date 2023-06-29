using UnityEngine;

public class BranchingRail : Rail
{
    [SerializeField] private Rail rightWayRail;
    [SerializeField] private Rail leftWayRail;
    [SerializeField] private GameObject rightDirection;
    [SerializeField] private GameObject leftDirection;
    [SerializeField] private GameObject straightDirection;


    public void AllowRightWay()
    {
        rightWayRail.SetLicenseToMove(true);
        leftWayRail.SetLicenseToMove(false);
        leftDirection.SetActive(false);
        straightDirection.SetActive(false);
        rightDirection.SetActive(true);
    }

    public void AllowLeftWay()
    {
        rightWayRail.SetLicenseToMove(false);
        leftWayRail.SetLicenseToMove(true);
        rightDirection.SetActive(false);
        straightDirection.SetActive(false);
        leftDirection.SetActive(true);
    }
}
