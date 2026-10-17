using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

public class CrashTrailerAnim : MonoBehaviour
{
    [SerializeField] private RagDollCharacter ragDollCharacter;
    [SerializeField] private float torqueForce;
    [SerializeField] private Vector3 gravity = new(0, -30f, 0);
    [SerializeField] private ParticleSystem soulParticleSystem;
    [SerializeField] private GameObject catchObject;
    [SerializeField] private Collider collider1;
    [SerializeField] private Collider collider2;

    private Rigidbody _rb;
    private ParticleSystemRenderer _particleRenderer;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _particleRenderer = soulParticleSystem.GetComponent<ParticleSystemRenderer>();
    }

    public void StartFalling(Transform obstacleTransform)
    {
        EnableColliders(false);

        _rb.useGravity = true;
        _rb.isKinematic = false;
        Physics.gravity = gravity;
        var direction = (transform.position - obstacleTransform.position).normalized;
        
        var forceImpulse = direction * 12;
        forceImpulse.y = 22;
        
        _rb.AddForce(forceImpulse, ForceMode.Impulse);
        _rb.AddTorque(Random.onUnitSphere * torqueForce, ForceMode.Impulse);
        
        ragDollCharacter.ActivateRagdoll(forceImpulse);
        _particleRenderer.material = null;
        soulParticleSystem.Emit(1);
        
        Invoke(nameof(DeactivateObject), 5f);
    }

    private void DeactivateObject()
    {
        _rb.useGravity = false;
        _rb.isKinematic = true;
        ragDollCharacter.ToggleRagdoll(false); 
        //gameObject.SetActive(false);
    }

    public void EnableColliders(bool value)
    {
        collider1.enabled = value;
        collider2.enabled = value;
        catchObject.SetActive(value);
    }

    public void ResetToInitialState()
    {
        _rb.useGravity = false;
        _rb.isKinematic = true;
        gameObject.SetActive(true);
        ragDollCharacter.ResetToInitialState();
    }
}