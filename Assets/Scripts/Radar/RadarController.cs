using UnityEngine;

public class RadarController : MonoBehaviour
{
    [Header("References")]

    // Player position
    [SerializeField]
    private Transform player;

    // Detector light on the bottom-right screen
    [SerializeField]
    private RadarBlip detectorLight;

    [Header("Detection Settings")]
    // Maximum detection range, currently about ten grid cells
    [SerializeField, Min(0.01f)]
    private float detectionRadius = 4.8f;

    // Used to check whether the player and item share a grid cell
    [SerializeField, Min(0f)]
    private float sameCellHalfSize = 0.24f;

    private RadarTarget[] targets;

    private void Awake()
    {
        // Find both active and temporarily inactive targets
        targets = FindObjectsByType<RadarTarget>( FindObjectsInactive.Include );

        SetLightActive(false);
    }

    private void LateUpdate()
    {
        if (player == null || detectorLight == null)
        {
            SetLightActive(false);
            return;
        }

        Vector2 playerPosition = player.position;

        float detectionRadiusSquared = detectionRadius * detectionRadius;

        RadarTarget nearestTarget = null;
        float nearestDistanceSquared = float.PositiveInfinity;

        bool nearestIsSameCell = false;

        foreach (RadarTarget target in targets)
        {
            // Skip targets that are unavailable or already collected
            if (target == null || !target.IsAvailable)
            {
                continue;
            }
            
            Vector2 difference = target.WorldPosition - playerPosition;

            float distanceSquared = difference.sqrMagnitude;

            if (distanceSquared > detectionRadiusSquared)
            {
                continue;
            }

            bool sameCell = Mathf.Abs(difference.x) <= sameCellHalfSize && Mathf.Abs(difference.y) <= sameCellHalfSize;

            if (distanceSquared < nearestDistanceSquared)
            {
                // Use the nearest target within the 360-degree range
                nearestTarget = target;
                nearestDistanceSquared =
                    distanceSquared;
                nearestIsSameCell = sameCell;
            }
        }

        if (nearestTarget == null)
        {
            // Turn off the light when no target is within range
            SetLightActive(false);
            return;
        }

        float distance = Mathf.Sqrt(nearestDistanceSquared);

        float closeness = 1f - Mathf.Clamp01(distance / detectionRadius);

        // The light becomes brighter and pulses faster when closer
        detectorLight.SetState(
            closeness,
            nearestIsSameCell
        );

        SetLightActive(true);
    }

    private void OnDisable()
    {
        SetLightActive(false);
    }

    private void SetLightActive(bool active)
    {
        if (detectorLight == null)
            return;

        if (detectorLight.gameObject.activeSelf != active)
        {
            detectorLight.gameObject.SetActive(active);
        }
    }
}
