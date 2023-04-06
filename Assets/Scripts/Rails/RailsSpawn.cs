using System.Collections.Generic;
using UnityEngine;

public class RailsSpawn : MonoBehaviour
{
    [SerializeField] private List<Rail> railPrefabs;
    [SerializeField] private GameObject spawnPoint;

    private List<Rail> rails;


    public void Spawn()
    {
        Rail spawnObject;
        rails = new List<Rail>();

        var childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            if (transform.GetChild(0).gameObject is not null)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
        }

        for (int i = 0; i < railPrefabs.Count; i++)
        {
            if (i != 0)
            {
                spawnObject = Instantiate(railPrefabs[i], transform.position, Quaternion.identity, transform);
                rails.Add(spawnObject);
                rails[i].transform.position = rails[i - 1].GetSpawnPostionForNextRail();

                rails[i].transform.rotation = Quaternion.LookRotation(rails[i - 1].GetDirection());
            }
            else
            {
                spawnObject = Instantiate(railPrefabs[i], transform.position, Quaternion.identity, transform);
                rails.Add(spawnObject);
            }
        }
        rails.Clear();
    }
}
