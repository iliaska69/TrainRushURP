using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "View", menuName = "ScriptableObjects/View", order = 1)]
public class View : ScriptableObject
{
    [SerializeField] private List<Slice> slices;
    
    public List<Slice> GetAllSlices()
    {
        return slices;
    }
}
