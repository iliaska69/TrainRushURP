using System;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private bool _isAnimate;
    private bool _cover;

    private void Start()
    {
        animator.SetBool("IsWaving", false);
        animator.SetBool("IsRaising", false);
    }

    void Update()
    {
        if (_cover)
        {
            animator.SetBool("IsWaving", false);
            animator.SetBool("IsRaising", false);
            return;
        }
        
        if (_isAnimate)
        {
            animator.SetBool("IsRaising", true);
            animator.SetBool("IsWaving", true);
        }
        else
        {
            animator.SetBool("IsWaving", false);
            animator.SetBool("IsRaising", false);
        }
    }

    public void SetIsAnimate(bool value) =>_isAnimate = value;
    public void SetIsCover(bool value) =>_cover = value;
    
    public void PutHands(bool value)
    {
        _cover = value;
        if (_cover)
        {
            animator.SetBool("IsWaving", false);
            animator.SetBool("IsRaising", false);
        }
        else
        {
            animator.SetBool("IsWaving", true);
            animator.SetBool("IsRaising", true);
        }
    }
}
