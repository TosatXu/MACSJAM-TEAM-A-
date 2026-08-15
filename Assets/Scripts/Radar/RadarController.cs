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

    [Header("Position Radar References")]

    // Prefab used for each item position dot
    [SerializeField]
    private RadarBlip itemBlipPrefab;

    // Parent object that holds all generated item dots
    [SerializeField]
    private Transform blipContainer;

    // Radar circles shown during close-range detection
    [SerializeField]
    private GameObject radarCircles;

    [Header("Position Radar Settings")]

    // Four grid cells: 4 x 0.48 = 1.92 world units
    [SerializeField, Min(0.01f)]
    private float positionDetectionRadius = 1.92f;

    // Maximum distance from the radar centre to its edge
    [SerializeField, Min(0.01f)]
    private float radarVisualRadius = 1.15f;

    [Header("Detection Settings")]
    // Maximum detection range, currently about ten grid cells
    [SerializeField, Min(0.01f)]
    private float detectionRadius = 4.8f;

    // Used to check whether the player and item share a grid cell
    [SerializeField, Min(0f)]
    private float sameCellHalfSize = 0.24f;

    private RadarTarget[] targets;

    private RadarBlip[] itemBlips;
    
    private void Awake()
    {
        // Find both active and temporarily inactive targets
        targets = FindObjectsByType<RadarTarget>(
            FindObjectsInactive.Include
        );

        // Prepare one position dot for every target
        itemBlips = new RadarBlip[targets.Length];

        if (itemBlipPrefab != null && blipContainer != null)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i] == null)
                    continue;

                RadarBlip newBlip = Instantiate(itemBlipPrefab, blipContainer);

                // Give each generated dot an easy-to-read name
                newBlip.name = "ItemBlip_" + targets[i].gameObject.name;

                newBlip.gameObject.SetActive(false);
                itemBlips[i] = newBlip;
            }
        }

        // Start with both radar displays hidden
        SetLightActive(false);

        if (radarCircles != null)
        {
            radarCircles.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        if (player == null || detectorLight == null)
        {
            SetLightActive(false);
            return;
        }

        Vector2 playerPosition = player.position;

        UpdatePositionRadar(playerPosition);

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
                nearestDistanceSquared = distanceSquared;
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
        
        // Use position dots instead of the detector light, when the nearest item is within four grid cells
        if (distance <= positionDetectionRadius)
        {
            SetLightActive(false);
            return;
        }

        float closeness = 1f - Mathf.Clamp01(distance / detectionRadius);

        // The light becomes brighter and pulses faster when closer
        detectorLight.SetState(
            closeness,
            nearestIsSameCell
        );

        SetLightActive(true);
    }

    private void UpdatePositionRadar(Vector2 playerPosition)
    {
        bool hasVisibleBlip = false;

        if (targets == null || itemBlips == null)
        {
            if (radarCircles != null)
            {
                radarCircles.SetActive(false);
            }

            return;
        }

        float positionRadiusSquared = positionDetectionRadius * positionDetectionRadius;

        int targetCount = Mathf.Min(targets.Length, itemBlips.Length);

        for (int i = 0; i < targetCount; i++)
        {
            RadarTarget target = targets[i];
            RadarBlip blip = itemBlips[i];

            if (blip == null)
                continue;

            // Hide dots for missing, collected or revealed items
            if (target == null || !target.IsAvailable)
            {
                SetItemBlipActive(blip, false);
                continue;
            }

            Vector2 difference = target.WorldPosition - playerPosition;

            float distanceSquared = difference.sqrMagnitude;

            // Do not reveal the position outside four grid cells
            if (distanceSquared > positionRadiusSquared)
            {
                SetItemBlipActive(blip, false);
                continue;
            }

            hasVisibleBlip = true;

            float distance = Mathf.Sqrt(distanceSquared);

            float closeness =
                1f - Mathf.Clamp01(
                    distance / positionDetectionRadius
                );

            bool sameCell = Mathf.Abs(difference.x) <= sameCellHalfSize && Mathf.Abs(difference.y) <= sameCellHalfSize;

            // Convert the world direction into radar local position
            Vector2 radarPosition = difference / positionDetectionRadius * radarVisualRadius;

            // Keep the dot exactly in the centre on the same cell
            if (sameCell)
            {
                radarPosition = Vector2.zero;
            }

            blip.transform.localPosition = new Vector3(radarPosition.x, radarPosition.y, 0f);

            blip.SetState(closeness, sameCell);

            SetItemBlipActive(blip, true);
        }

        // Show the radar circles only when at least one dot is visible
        if (radarCircles != null && radarCircles.activeSelf != hasVisibleBlip)
        {
            radarCircles.SetActive(hasVisibleBlip);
        }
    }

    private void SetItemBlipActive(RadarBlip blip, bool active)
    {
        if (blip == null)
            return;

        if (blip.gameObject.activeSelf != active)
        {
            blip.gameObject.SetActive(active);
        }
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
