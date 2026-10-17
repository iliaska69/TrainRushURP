using UnityEngine;

public class BonusBox : MonoBehaviour, IActivatable
{
    [SerializeField] private Bonus bonus;

    private Transform _transform;

    void Start()
    {
        _transform = transform;
    }

    public void ActivateBonus()
    {
        _transform.position = new Vector3(1110f, 1110f, 1110f);
        bonus.Activate();
    }

}
