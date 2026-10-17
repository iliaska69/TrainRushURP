using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTrailer : MonoBehaviour
{
    [Range(0,1)][SerializeField] private float startParameter;
    [SerializeField] private BezierCurve curve;
    [SerializeField] private bool isSpawnMainTrailer;
    [SerializeField] private Transform spawnPlace;
    [SerializeField] private Vector3 trailerPosition;
    [SerializeField] private Quaternion trailerRotation;
    [SerializeField] private Railway railway;
    [SerializeField] private GameObject spawnerModel;
    
    private Trailer _spawnedTrailer;


    private void Start()
    {
        var typeOfTrailer = Game.instance.GetTrailerSkin();
        if (_spawnedTrailer != null)
        {
            _spawnedTrailer.DestroyWithoutAnimation();
        }
        
        _spawnedTrailer = Instantiate(typeOfTrailer, trailerPosition, transform.rotation, spawnPlace);
        _spawnedTrailer.SetMainTrailer(isSpawnMainTrailer);

        railway.ImplementObjectToRailway(_spawnedTrailer.gameObject.GetInstanceID(), curve, startParameter);
        spawnerModel.SetActive(false);
    }

    [Button("Join")]
    public virtual void JoinToCurve()
    {
        var hitColliders = Physics.OverlapSphere(transform.position, 0.6f);
        railway = GetComponentInParent<Railway>();
        foreach (var hit in hitColliders)
        {
            var newCurve = hit.gameObject.GetComponent<BezierCurve>();
            if (newCurve == null) continue;
            curve = newCurve;
            var points = curve.GetAllPoints();
            trailerPosition = railway.GetPositionByT(curve, startParameter);
            transform.position = trailerPosition;

            trailerRotation = Quaternion.LookRotation(
                 BezierCubic.GetFirstDerivative(points[0], points[1], points[2], points[3], startParameter));
            transform.rotation = trailerRotation;
        }
    }
}
