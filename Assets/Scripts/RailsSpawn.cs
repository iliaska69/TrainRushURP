using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailsSpawn : MonoBehaviour
{
    [SerializeField] private List<Rail> rails;
    [SerializeField] private GameObject spawnPoint;
    //[SerializeField] private GameObject her;

    public void Spawn()
    {

        var childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

        for (int i = 0; i < rails.Count; i++)
        {
            if (i != 0)
            {
                //her.transform.position = rails[i-1].secondPoint.transform.position;
                Debug.Log(rails[i - 1].secondPoint.transform.localPosition);
                var transformSecondPoint = transform.TransformPoint(rails[i - 1].secondPoint.transform.position);
                var a = Instantiate(rails[i], transformSecondPoint, Quaternion.identity, transform);
                Debug.Log(a.transform.localPosition);
                a.transform.localPosition = rails[i - 1].secondPoint.transform.position;
                Debug.Log(a.transform.position);
            }
            else
            {

                Instantiate(rails[i], transform.position, Quaternion.identity, transform);
            }
        }
    }
}
