using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ShovelLeverController : MonoBehaviour,
    IPointerDownHandler,
    IPointerUpHandler
{
    [SerializeField] private Button shovelButton;
    [SerializeField] private float triggerValue = 0.9f;
    [SerializeField] private float returnSpeed = 2.5f;

    [Header("Lever Sound")]
    [SerializeField] private AudioSource leverAudioSource;
    [SerializeField] private float soundStopDelay = 0.12f;

    private Slider lever;
    private bool isPressed;
    private bool digTriggeredThisPull;
    private float previousLeverValue;
    private float lastMovementTime;

    private void Awake()
    {
        lever = GetComponent<Slider>();
        lever.value = lever.minValue;
        previousLeverValue = lever.value;

        lever.onValueChanged.AddListener(CheckForDig);

        if (leverAudioSource != null)
        {
            leverAudioSource.playOnAwake = false;
            leverAudioSource.loop = true;
            leverAudioSource.spatialBlend = 0f;
        }
    }

    private void Update()
    {
        if (!isPressed)
        {
            // Return to the top.
            lever.value = Mathf.MoveTowards(lever.value, lever.minValue, returnSpeed * Time.deltaTime);
        }

        float currentValue = lever.value;
        bool leverMoved = !Mathf.Approximately(currentValue, previousLeverValue);

        if (leverMoved)
        {
            lastMovementTime = Time.unscaledTime;

            if (leverAudioSource != null && leverAudioSource.clip != null && !leverAudioSource.isPlaying)
            {
                // Play while the lever moves.
                leverAudioSource.Play();
            }
        }

        previousLeverValue = currentValue;

        if (leverAudioSource != null && leverAudioSource.isPlaying && Time.unscaledTime - lastMovementTime > soundStopDelay)
        {
            // Stop after the lever stops.
            leverAudioSource.Stop();
        }
    }

    private void CheckForDig(float value)
    {
        if (!isPressed || digTriggeredThisPull || value < triggerValue)
            return;

        // Trigger only once per pull.
        digTriggeredThisPull = true;

        // Use the old button actions.
        if (shovelButton != null)
        {
            shovelButton.onClick.Invoke();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        digTriggeredThisPull = false;
        previousLeverValue = lever.value;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }

    private void OnDisable()
    {
        isPressed = false;

        if (leverAudioSource != null)
        {
            leverAudioSource.Stop();
        }
    }

    private void OnDestroy()
    {
        if (lever != null)
        {
            lever.onValueChanged.RemoveListener(CheckForDig);
        }
    }
}