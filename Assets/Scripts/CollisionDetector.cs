using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public int collisionLayer;
    public GameObject collisionObject;

    // Keeps track of everything inside this detector
    private readonly HashSet<Collider2D> overlappingColliders =
        new HashSet<Collider2D>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        overlappingColliders.Add(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        overlappingColliders.Add(collision);

        // Keep the original detection behaviour
        collisionLayer = collision.gameObject.layer;
        collisionObject = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        overlappingColliders.Remove(collision);

        if (collision.gameObject == collisionObject)
        {
            collisionLayer = 0;
            collisionObject = null;
        }
    }

    // Finds the nearest radar item inside this detector
    public RadarTarget GetNearestRadarTarget()
    {
        RadarTarget nearestTarget = null;
        float nearestDistanceSquared = float.PositiveInfinity;

        foreach (Collider2D detectedCollider in overlappingColliders)
        {
            if (detectedCollider == null)
                continue;

            RadarTarget target = detectedCollider.GetComponentInParent<RadarTarget>();

            if (target == null || !target.IsAvailable)
                continue;

            float distanceSquared =
                ((Vector2)target.transform.position -
                 (Vector2)transform.position).sqrMagnitude;

            if (distanceSquared < nearestDistanceSquared)
            {
                nearestTarget = target;
                nearestDistanceSquared = distanceSquared;
            }
        }

        return nearestTarget;
    }

    private void OnDisable()
    {
        overlappingColliders.Clear();
        collisionLayer = 0;
        collisionObject = null;
    }
}