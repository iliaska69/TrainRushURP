using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class Railway : MonoBehaviour
{
    //[SerializeField] private GameObject tailer;

    //private List<Rail> _rails = new List<Rail>();
    //private List<int> _forbiddenNumbersOfRailToSpawn = new List<int>();

    //private void Start()
    //{
    //    _forbiddenNumbersOfRailToSpawn.Add(1);
    //    Debug.Log(_forbiddenNumbersOfRailToSpawn.Count);
    //}

    //[Button("Update interior list of rails")]
    //public void UpdateRails()
    //{
    //    _rails.Clear();
    //    var childrenRail = gameObject.GetComponentsInChildren<Rail>();

    //    foreach (var rail in childrenRail)
    //    {
    //        _rails.Add(rail);
    //    }
    //}

    //[Button("Spawn trailer in random place")]
    //public void SpawnTrailerInRandomPlace()
    //{
    //    var newNumberRailToSpawn = Random.Range(1, (_rails.Count - 1));
    //    Debug.Log(newNumberRailToSpawn);
    //    Debug.Log(_forbiddenNumbersOfRailToSpawn.Count);
    //    foreach (var numberRail in _forbiddenNumbersOfRailToSpawn)
    //    {
    //        if (numberRail == newNumberRailToSpawn) continue;

    //        _forbiddenNumbersOfRailToSpawn.Add(newNumberRailToSpawn);
    //        var curveToSpawn = _rails[newNumberRailToSpawn].GetCurve();
    //        var points = curveToSpawn.GetAllPoints();

    //        var tParametr = Random.Range(0, 1);
    //        var postionToSpawn = Bezier.GetPoint(
    //            points[0],
    //            points[1],
    //            points[2],
    //            points[3],
    //            tParametr);

    //        var rotation = Bezier.GetFirstDerivative(
    //            points[0],
    //            points[1],
    //            points[2],
    //            points[3],
    //            tParametr);

    //        Instantiate(tailer, postionToSpawn, Quaternion.LookRotation(rotation), transform);
    //    }

    //    Debug.Log("All rail is forbidden");
    //    return;

    //}
}
