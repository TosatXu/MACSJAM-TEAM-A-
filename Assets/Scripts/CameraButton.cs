using UnityEngine;
using UnityEngine.UIElements;


public class button : MonoBehaviour
{
    public GameObject screen;

    public void CameraButton_clicked()
    {
        AudioManager.instance.PlayCamera();
        screen.GetComponent<CameraDisplay>().takePicture();
    }
}
