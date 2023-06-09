using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBox : MonoBehaviour, IActivatable
{
    [SerializeField] private Bonus bonus;
    [SerializeField] private float timer = 3f;

    private Transform _transform;
    private bool isTimerStart;

    void Start()
    {
        _transform = transform;
    }

    void Update()
    {
        if (isTimerStart && timer > 0f)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0f)
        {
            DeactivateBonus();
        }
    }

    public void ActivateBonus()
    {
        isTimerStart = true;
        _transform.position = new Vector3(1110f, 1110f, 1110f);
        bonus.Activate();
    }

    public void DeactivateBonus() 
    {
        bonus.Deactivate();
        Destroy(gameObject);
    }
}
