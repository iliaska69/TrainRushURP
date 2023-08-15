using UnityEngine;

[CreateAssetMenu(fileName = "Shop Item", menuName = "ScriptableObjects/Shop Item", order = 2)]
public class ShopItem : ScriptableObject
{
    [SerializeField] private int price;
    [SerializeField] private GameObject model;
    [SerializeField] private float speed;
}
