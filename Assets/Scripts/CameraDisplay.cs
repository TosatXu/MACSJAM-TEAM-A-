using UnityEngine;

public class CameraDisplay : MonoBehaviour
{
    public Sprite Screen;
    public Sprite Image;
    float timer;

    public void ShowImage()
    {
        timer = 0;
        this.GetComponent<SpriteRenderer>().sprite = Image;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 2)
        {
            this.GetComponent<SpriteRenderer>().sprite = Screen;
        }
    }
}
