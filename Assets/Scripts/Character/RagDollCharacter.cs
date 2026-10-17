using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class RagDollCharacter : MonoBehaviour
{
    public Animator animator;
    public Rigidbody[] ragdollBodies;
    public Collider[] ragdollColliders;
    public float initialImpulse;
    private List<TransformData> _initialTransforms = new();

    void Start()
    {
        SaveInitialTransforms();
        ToggleRagdoll(false);
    }

    public void ActivateRagdoll(Vector3 forceImpulse)
    {
        if (animator != null)
            animator.enabled = false;

        ToggleRagdoll(true);

        foreach (var body in ragdollBodies)
        {
            body.AddForce(forceImpulse * initialImpulse, ForceMode.Impulse);
        }
    }

    public void ToggleRagdoll(bool state)
    {
        for (var i = 0; i < ragdollBodies.Length; i++)
        {
            ragdollBodies[i].isKinematic = !state;
            ragdollBodies[i].useGravity = state;
            ragdollColliders[i].enabled = state;
        }
    }

    public void ResetToInitialState()
    {
        ToggleRagdoll(false);

        for (int i = 0; i < _initialTransforms.Count; i++)
        {
            var bone = _initialTransforms[i].bone;
            bone.localPosition = _initialTransforms[i].localPosition;
            bone.localRotation = _initialTransforms[i].localRotation;
        }

        if (animator != null)
            animator.enabled = true;
    }

    [Button("Save RagDoll transform data")] // use this method in inspector
    public void SaveInitialTransforms()
    {
        _initialTransforms.Clear();

        foreach (var body in ragdollBodies)
        {
            var bone = body.transform;
            _initialTransforms.Add(new TransformData
            {
                bone = bone,
                localPosition = bone.localPosition,
                localRotation = bone.localRotation
            });
        }
    }

    private struct TransformData
    {
        public Transform bone;
        public Vector3 localPosition;
        public Quaternion localRotation;
    }
}