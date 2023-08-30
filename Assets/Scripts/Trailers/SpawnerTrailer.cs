using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTrailer : MonoBehaviour
{
    [Range(0,1)][SerializeField] private float startParameter;
    [SerializeField] private BezierCurve curve;
    [SerializeField] private Transform spawnPlace;
    [SerializeField] private bool isSpawnMainTrailer;

    [SerializeField] private Vector3 trailerPosition;
    [SerializeField] private Quaternion trailerRotation;

    private void Start()
    {
        var typeOfTrailer = Game.instance.GetDefaultTrailerSkin();
        var trailer = Instantiate(typeOfTrailer, trailerPosition, trailerRotation, spawnPlace);
        var trailerMovementComponent = trailer.GetComponent<TrailerMovement>();
        trailerMovementComponent.SetStartPosition(curve, startParameter);
        trailer.SetMainTrailer(isSpawnMainTrailer);
        Destroy(gameObject);
    }

    [Button("Join")]
    public virtual void JoinToCurve()
    {
        var hitColliders = Physics.OverlapSphere(transform.position, 0.6f);
        foreach (var hit in hitColliders)
        {
            var newCurve = hit.gameObject.GetComponent<BezierCurve>();
            if (newCurve == null) continue;
            curve = newCurve;
            var points = curve.GetAllPoints();

            trailerPosition = Bezier.GetPoint(points[0], points[1], points[2], points[3], startParameter);
            Debug.Log(trailerPosition);
            transform.position = trailerPosition;

            trailerRotation = Quaternion.LookRotation(
                Bezier.GetFirstDerivative(points[0], points[1], points[2], points[3], startParameter));
            transform.rotation = trailerRotation;
        }
    }
}
