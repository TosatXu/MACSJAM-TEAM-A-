using NUnit.Framework.Constraints;
using UnityEngine;

public class CameraDisplay : MonoBehaviour
{
    public Sprite Screen;
    public Sprite expanse1;
    public Sprite cliff;
    public Sprite wall;

    float timer;
    public GameObject collider1;
    public GameObject collider2;

    public void ShowImage()
    {
        timer = 0;

        if (collider1.GetComponent<CollisionDetector>().collisionLayer == 6)
        {
            this.GetComponent<SpriteRenderer>().sprite = wall;
        }
        else if (collider2.GetComponent<CollisionDetector>().collisionLayer == 6)
        {
            this.GetComponent<SpriteRenderer>().sprite = cliff;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = expanse1;
        }
        
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
