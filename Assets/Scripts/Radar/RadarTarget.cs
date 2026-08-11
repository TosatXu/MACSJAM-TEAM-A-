using System;
using UnityEngine;

[DisallowMultipleComponent]
public class RadarTarget : MonoBehaviour
{
    // Hide the item world sprite while the game is running
    [SerializeField]
    private bool hideWorldSpriteAtRuntime = true;

    [SerializeField]
    private SpriteRenderer worldSpriteRenderer;

    // Records whether this item has been collected
    public bool IsCollected { get; private set; }
    // Records whether this item has been revealed
    public bool IsRevealed { get; private set; }

    // Only active and uncollected items can be detected
    public bool IsAvailable => isActiveAndEnabled && !IsCollected;

    // Current world position used by the detector
    public Vector2 WorldPosition => transform.position;

    // Notifies other systems when this item is collected
    public event Action<RadarTarget> Collected;

    private void Reset()
    {
        worldSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Awake()
    {
        if (worldSpriteRenderer == null)
        {
            // Find it automatically if the Inspector reference is empty
            worldSpriteRenderer =
                GetComponent<SpriteRenderer>();
        }
    }

    private void OnEnable()
    {
        if (hideWorldSpriteAtRuntime && !IsRevealed && worldSpriteRenderer != null)
        {
            // Hide only the sprite
            worldSpriteRenderer.enabled = false;
        }
    }

    public void MarkCollected()
    {
        // Prevent the same item from being collected more than once
        if (IsCollected)
            return;

        IsCollected = true;
        Collected?.Invoke(this);
    }

    public void Reveal()
    {
        // Show the item after it is photographed
        IsRevealed = true;

        if (worldSpriteRenderer != null)
        {
            worldSpriteRenderer.enabled = true;
        }
    }
}
