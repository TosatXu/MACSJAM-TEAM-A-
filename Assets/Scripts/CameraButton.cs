using UnityEngine;
using UnityEngine.UIElements;


public class button : MonoBehaviour
{
    public GameObject screen;

    public void CameraButton_clicked()
    {
        screen.GetComponent<CameraDisplay>().ShowImage();
    }
}
