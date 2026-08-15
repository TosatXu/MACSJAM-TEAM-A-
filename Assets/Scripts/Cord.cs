using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Cord : MonoBehaviour
{
    public GameObject cameraScreen;
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
        if (!pressed)
        {
            gameObject.GetComponent<Slider>().value -= 0.01f + ((gameObject.GetComponent<Slider>().value - 0.5f) / 10f);
        }
        gameObject.GetComponent<Slider>().value = Mathf.Clamp(gameObject.GetComponent<Slider>().value, 0.5f, 1f);
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
    }

    public void isNotPressed ()
    {
        pressed = false;
    }
}
