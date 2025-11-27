using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BombExploder : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private LayerMask _impactRaycastLayer;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Explode()
    {
        List<Rigidbody> rigidbodies = GetRigidbodies();

        for (int i = 0; i < rigidbodies.Count; i++)
            rigidbodies[i].AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
    }

    private List<Rigidbody> GetRigidbodies()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _explosionRadius, _impactRaycastLayer);
        List<Rigidbody> rigidbodies = new List<Rigidbody>();

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out Rigidbody rigidbody))
            {
                if (rigidbody == _rigidbody)
                    continue;
                else
                    rigidbodies.Add(rigidbody);
            }
        }

        return rigidbodies;
    }
}
