using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "View", menuName = "ScriptableObjects/View", order = 1)]
public class View : ScriptableObject
{
    public bool isHorizontal;
    [SerializeField] private List<Slice> verticalSlices;
    [SerializeField] private List<Slice> horizontalSlices;
    
    public List<Slice> GetAllSlices()
    {
        if (isHorizontal)
            return horizontalSlices;
        else
            return verticalSlices;
    }
}
