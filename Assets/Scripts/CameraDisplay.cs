using NUnit.Framework.Constraints;
using UnityEngine;

public class CameraDisplay : MonoBehaviour
{
    public Sprite Screen;
    public Sprite expanse1;
    public Sprite expanse2;
    public Sprite expanse3;
    public Sprite cliff;
    public Sprite wall;
    public Sprite item1;
    public Sprite item2;
    public Sprite sarcophagus1;
    public Sprite sarcophagus2;
    public Sprite mushroom;
    public Sprite dust;

    float timer;
    public GameObject collider1;
    public GameObject collider2;
    public GameObject audioManager;
    public GameObject rover;

    public void takePicture()
    {
        if (timer < 0) {
            audioManager.GetComponent<AudioManager>().PlayCamera();
            Invoke("ShowImage", 0.3f);
        }
    }

    public void ShowImage()
    {
        timer = 2f;

        if (collider1.GetComponent<CollisionDetector>().collisionLayer == 6)
        {
            this.GetComponent<SpriteRenderer>().sprite = wall;
        }
        else if (collider2.GetComponent<CollisionDetector>().collisionLayer == 12 || collider1.GetComponent<CollisionDetector>().collisionLayer == 12)
        {
            this.GetComponent<SpriteRenderer>().sprite = dust;
        }
        else if (collider2.GetComponent<CollisionDetector>().collisionLayer == 7)
        {
            this.GetComponent<SpriteRenderer>().sprite = item1;
        }
        else if (collider1.GetComponent<CollisionDetector>().collisionLayer == 7)
        {
            this.GetComponent<SpriteRenderer>().sprite = item2;
        }
        else if (collider1.GetComponent<CollisionDetector>().collisionLayer == 8)
        {
            this.GetComponent<SpriteRenderer>().sprite = mushroom;
        }
        else if (collider1.GetComponent<CollisionDetector>().collisionLayer == 9)
        {
            this.GetComponent<SpriteRenderer>().sprite = sarcophagus1;
        }
        else if (collider2.GetComponent<CollisionDetector>().collisionLayer == 9)
        {
            this.GetComponent<SpriteRenderer>().sprite = Screen;
        }
        else if (collider2.GetComponent<CollisionDetector>().collisionLayer == 6)
        {
            this.GetComponent<SpriteRenderer>().sprite = cliff;
        }
        else if (collider1.transform.position.x > 0 || collider1.transform.position.x < -7.5 || collider1.transform.position.y > 4.5 || collider1.transform.position.y < -3.25)
        {
            this.GetComponent<SpriteRenderer>().sprite = dust;
        }
        else
        {
            if (collider1.transform.position.x < -4.5 && collider1.transform.position.y < -0.5)
            {
                this.GetComponent<SpriteRenderer>().sprite = expanse1;
            }
            else if (collider1.transform.position.y < 1)
            {
                this.GetComponent<SpriteRenderer>().sprite = expanse2;
            }
            else
            {
                this.GetComponent<SpriteRenderer>().sprite = expanse3;
            }
        }
        rover.GetComponent<IconMove>().placeMarker();
        
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0.5f)
        {
            this.GetComponent<SpriteRenderer>().sprite = Screen;
        }
    }
}
