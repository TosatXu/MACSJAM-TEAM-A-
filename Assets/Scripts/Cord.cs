using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Cord : MonoBehaviour
{
    public GameObject cameraScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.GetComponent<Slider>().value = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Slider>().value -= 0.01f + ((gameObject.GetComponent<Slider>().value-0.5f)/10f);
        gameObject.GetComponent<Slider>().value = Mathf.Clamp(gameObject.GetComponent<Slider>().value, 0.5f, 1f);
    }

    public void activateCamera ()
    {
        if (gameObject.GetComponent<Slider>().value >= 0.9f)
        {
            cameraScreen.GetComponent<CameraDisplay>().takePicture();
        }
    }
}
