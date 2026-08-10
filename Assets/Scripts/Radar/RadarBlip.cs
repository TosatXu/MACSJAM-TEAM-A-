using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RadarBlip : MonoBehaviour
{
    [Header("Colours")]
    [SerializeField]
    private Color farColor = new Color(0.05f, 0.35f, 0.12f, 1f);

    [SerializeField]
    private Color nearColor = new Color(0.43f, 1f, 0.33f, 1f);

    [Header("Pulse Speed")]
    [SerializeField]
    private float farPulsePeriod = 1.3f;

    [SerializeField]
    private float nearPulsePeriod = 0.45f;

    [Header("Minimum Brightness")]
    [SerializeField, Range(0f, 1f)]
    private float farMinimumAlpha = 0.15f;

    [SerializeField, Range(0f, 1f)]
    private float nearMinimumAlpha = 0.65f;

    private SpriteRenderer spriteRenderer;
    private float closeness;

    // check is the player in the same grid as the player
    private bool sameCell;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetState(
        float normalizedCloseness,
        bool isSameCell
    )
    {
        closeness = Mathf.Clamp01(normalizedCloseness);
        sameCell = isSameCell;
    }

    private void Update()
    {   
        // Ensure the value remains between 0 and 1
        Color currentColor = Color.Lerp(farColor, nearColor, closeness);

        // Stop blinking when in the same cell and stay brightest
        if (sameCell)
        {
            currentColor.a = 1f;
            spriteRenderer.color = currentColor;
            return;
        }

        // Flashing speed determined by the distance
        float pulsePeriod =
            Mathf.Lerp(
                farPulsePeriod,
                nearPulsePeriod,
                closeness
            );

        // Smooth flickering effect
        float pulse = (Mathf.Sin(Time.time * Mathf.PI * 2f / pulsePeriod) + 1f) * 0.5f;

        float minimumAlpha =
            Mathf.Lerp(
                farMinimumAlpha,
                nearMinimumAlpha,
                closeness
            );

        currentColor.a = Mathf.Lerp(minimumAlpha, 1f, pulse);

        spriteRenderer.color = currentColor;
    }
}