using NUnit.Framework.Constraints;
using UnityEngine;

public class CameraDisplay : MonoBehaviour
{
    public Sprite Screen;
    public Sprite expanse1;
    public Sprite cliff;
    public Sprite wall;
    public Sprite item1;
    public Sprite sarcophagus;
    public Sprite mushroom;
    public Sprite dust;

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
        else if (collider1.GetComponent<CollisionDetector>().collisionLayer == 7)
        {
            this.GetComponent<SpriteRenderer>().sprite = item1;
        }
        else if (collider1.GetComponent<CollisionDetector>().collisionLayer == 8)
        {
            this.GetComponent<SpriteRenderer>().sprite = mushroom;
        }
        else if (collider1.GetComponent<CollisionDetector>().collisionLayer == 9)
        {
            this.GetComponent<SpriteRenderer>().sprite = sarcophagus;
        }
        else if (collider1.transform.position.x > 0 || collider1.transform.position.x < -7.5 || collider1.transform.position.y > 4.5 || collider1.transform.position.y < -3.25)
        {
            this.GetComponent<SpriteRenderer>().sprite = dust;
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
