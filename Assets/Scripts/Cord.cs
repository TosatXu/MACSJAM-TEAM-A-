using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Cord : MonoBehaviour
{
    public GameObject cameraScreen;
    
    [SerializeField]
    private Animator cordAnimator;

    bool pressed;

    // Allows only one photo per press
    bool photoTakenThisPull;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.GetComponent<Slider>().value = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        Slider slider = GetComponent<Slider>();
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
    }

    public void activateCamera ()
    {
        if (!pressed || photoTakenThisPull)
        return;

        if (gameObject.GetComponent<Slider>().value >= 0.9f)
        {
            photoTakenThisPull = true;
            cameraScreen.GetComponent<CameraDisplay>().takePicture();
        }
    }

    public void isPressed ()
    {
        pressed = true;

        // Start a new pull
        photoTakenThisPull = false;

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
}
