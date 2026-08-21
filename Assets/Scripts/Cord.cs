using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Cord : MonoBehaviour
{
    public GameObject cameraScreen;
    
    [SerializeField]
    private Animator cordAnimator;

    [SerializeField]
    private AudioClip triggerSound;

    [Header("Cord Sound")]
    [SerializeField] 
    private AudioSource cordAudioSource;
    
    [SerializeField] 
    private float soundStopDelay = 0.18f;


    private Slider slider;
    private bool pressed;
    // Allows only one photo per press
    private bool photoTakenThisPull;
    private float previousCordValue;
    private float lastMovementTime;
   

    private void Awake()
    {
        slider = GetComponent<Slider>();

        if (cordAudioSource != null)
        {
            cordAudioSource.playOnAwake = false;
            cordAudioSource.loop = true;
            cordAudioSource.spatialBlend = 0f;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider.value = 0.5f;
        previousCordValue = slider.value;
    }

    // Update is called once per frame
    void Update()
    {
        float previousValue = slider.value;
    
        if (!pressed)
        {
            slider.value -= 0.01f + ((slider.value - 0.4f) / 10f);
        }

        slider.value = Mathf.Clamp(slider.value, 0.5f, 1f);

        // Play bounce after returning
        if (!pressed && previousValue > 0.5f && slider.value <= 0.5f && cordAnimator != null)
        {
            cordAnimator.ResetTrigger("Bounce");
            cordAnimator.SetTrigger("Bounce");
        }

        float currentValue = slider.value;
        bool cordMoved = !Mathf.Approximately(currentValue, previousCordValue);

        if (cordMoved)
        {
            lastMovementTime = Time.unscaledTime;

            if (cordAudioSource != null && cordAudioSource.clip != null && !cordAudioSource.isPlaying)
            {
                // Play while the cord moves.
                cordAudioSource.Play();
            }
        }

        previousCordValue = currentValue;

        if (cordAudioSource != null && cordAudioSource.isPlaying && Time.unscaledTime - lastMovementTime > soundStopDelay)
        {
            // Stop after the cord stops.
            cordAudioSource.Stop();
        }
    }

    public void activateCamera ()
    {
        if (!pressed || photoTakenThisPull)
        return;

        if (slider.value >= 0.9f)
        {
            photoTakenThisPull = true;

            if (AudioManager.instance != null && AudioManager.instance.sfxSource != null && triggerSound != null)
            {
                AudioManager.instance.sfxSource.PlayOneShot(triggerSound);
            }

            cameraScreen.GetComponent<CameraDisplay>().takePicture();
        }
    }

    public void isPressed ()
    {
        pressed = true;
        // Start a new pull
        photoTakenThisPull = false;
        previousCordValue = slider.value;

        if (cordAnimator != null)
        {
            cordAnimator.ResetTrigger("Bounce");
            cordAnimator.Play("Idle", 0, 0f);
            cordAnimator.Update(0f);
        }
    }

    public void isNotPressed ()
    {
        pressed = false;

        // Start a new pull
        photoTakenThisPull = false;

        // Stop the previous bounce
        if (cordAnimator != null)
        {
            cordAnimator.ResetTrigger("Bounce");
            cordAnimator.Play("Idle", 0, 0f);
            cordAnimator.Update(0f);
        }
    }

    private void OnDisable()
    {
        pressed = false;

        if (cordAudioSource != null)
        {
            cordAudioSource.Stop();
        }
    }
}
