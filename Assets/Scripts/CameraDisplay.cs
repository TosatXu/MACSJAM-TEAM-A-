using NUnit.Framework.Constraints;
using UnityEngine;
using System.Collections;

public class CameraDisplay : MonoBehaviour
{
    public Sprite Screen;
    public Sprite expanse1;
    public Sprite expanse2;
    public Sprite expanse2figures;
    public Sprite expanse3figures;
    public Sprite expanse3;
    public Sprite expanseFinal;
    public Sprite cliff;
    public Sprite wall;
    public Sprite item1;
    public Sprite item2;
    public Sprite carcass;
    public Sprite sarcophagus1;
    public Sprite sarcophagus2;
    public Sprite desert;
    public Sprite dust;
    public Sprite canyon;
    public Sprite tunnel;
    public Sprite cave;
    public Sprite face;
    public Destroy sarcophagus1Target;

    float timer;
    public GameObject collider1;
    public GameObject collider2;
    public GameObject audioManager;
    public GameObject rover;

    [Header("Photo Flash")]
    [SerializeField] private SpriteRenderer flashOverlay;
    [SerializeField] private float flashHoldTime = 0.05f;
    [SerializeField] private float flashFadeTime = 0.15f;

    public void takePicture()
    {
        if (timer < 0) {
            timer = 2.2f;
            audioManager.GetComponent<AudioManager>().PlayCamera();
            // Invoke("ShowImage", 0.2f);
            StartCoroutine(PhotoSequence());
        }
    }

    private IEnumerator PhotoSequence()
    {
        if (flashOverlay == null)
        {
            yield return new WaitForSeconds(flashHoldTime + flashFadeTime);
            ShowImage();
            yield break;
        }

        flashOverlay.enabled = true;

        Color flashColor = flashOverlay.color;
        flashColor.a = 1f;
        flashOverlay.color = flashColor;

        // Keep the flash bright for a moment.
        yield return new WaitForSeconds(flashHoldTime);

        ShowImage();

        // Fade the flash out.
        float elapsed = 0f;
        

        while (elapsed < flashFadeTime)
        {
            elapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsed / flashFadeTime);

            flashColor.a = Mathf.Lerp(1f, 0f, progress);
            flashOverlay.color = flashColor;

            yield return null;
        }

        flashColor.a = 0f;
        flashOverlay.color = flashColor;
        flashOverlay.enabled = false;
    }

    public void ShowImage()
    {
        int randomNum = Random.Range(0, 4);
        Debug.Log(randomNum);

        CollisionDetector frontDetector1 =
        collider1.GetComponent<CollisionDetector>();
        CollisionDetector frontDetector2 =
        collider2.GetComponent<CollisionDetector>();

        // Find the nearest item captured by the camera
        RadarTarget photographedTarget = frontDetector1.GetNearestRadarTarget();

        if (photographedTarget == null)
        {
            photographedTarget = frontDetector2.GetNearestRadarTarget();
        }

        // Reveal the photographed item in the game world
        if (photographedTarget != null)
        {
            photographedTarget.Reveal();
        }

        Destroy photographedItem1 = null;
        Destroy photographedItem2 = null;

        if (frontDetector1.collisionObject != null)
        {
            photographedItem1 =
                frontDetector1.collisionObject.GetComponentInParent<Destroy>();
        }
        if (frontDetector2.collisionObject != null)
        {
            photographedItem2 =
                frontDetector2.collisionObject.GetComponentInParent<Destroy>();
        }

        if (sarcophagus1Target != null && photographedItem1 == sarcophagus1Target || photographedItem2 == sarcophagus1Target)
        {
            GetComponent<SpriteRenderer>().sprite = tunnel;

            sarcophagus1Target.TriggerFromPhoto();

            frontDetector1.collisionLayer = 0;
            frontDetector1.collisionObject = null;
            frontDetector2.collisionLayer = 0;
            frontDetector2.collisionObject = null;
        }
        else if (frontDetector1.collisionLayer == 6)
        {
            GetComponent<SpriteRenderer>().sprite = wall;
        }
        else if (collider1.GetComponent<CollisionDetector>().collisionLayer == 6)
        {
            this.GetComponent<SpriteRenderer>().sprite = wall;
        }
        else if (collider1.GetComponent<CollisionDetector>().collisionLayer == 12)
        {
            this.GetComponent<SpriteRenderer>().sprite = face;
        }
        else if (collider2.GetComponent<CollisionDetector>().collisionLayer == 7)
        {
            this.GetComponent<SpriteRenderer>().sprite = item1;
        }
        else if (collider1.GetComponent<CollisionDetector>().collisionLayer == 7)
        {
            this.GetComponent<SpriteRenderer>().sprite = item2;
        }
        else if (collider1.GetComponent<CollisionDetector>().collisionLayer == 18)
        {
            this.GetComponent<SpriteRenderer>().sprite = carcass;
        }
        else if (collider1.GetComponent<CollisionDetector>().collisionLayer == 8)
        {
            this.GetComponent<SpriteRenderer>().sprite = desert;
        }
        else if (frontDetector1.collisionLayer == 9)
        {
            GetComponent<SpriteRenderer>().sprite = sarcophagus2;
        }
        // else if (collider1.GetComponent<CollisionDetector>().collisionLayer == 9)
        // {
        //     this.GetComponent<SpriteRenderer>().sprite = sarcophagus1;
        // }
        //else if (collider2.GetComponent<CollisionDetector>().collisionLayer == 9)
        //{
        //    this.GetComponent<SpriteRenderer>().sprite = tunnel;
        //}
        else if (collider2.GetComponent<CollisionDetector>().collisionLayer == 6)
        {
            this.GetComponent<SpriteRenderer>().sprite = cliff;
        }
        else if (collider1.GetComponent<CollisionDetector>().collisionLayer == 16)
        {
            this.GetComponent<SpriteRenderer>().sprite = canyon;
        }
        else if (collider1.GetComponent<CollisionDetector>().collisionLayer == 17)
        {
            this.GetComponent<SpriteRenderer>().sprite = tunnel;
        }
        else if (collider2.GetComponent<CollisionDetector>().collisionLayer == 17)
        {
            this.GetComponent<SpriteRenderer>().sprite = cave;
        }
        else
        {
            if (collider1.GetComponent<CollisionDetector>().collisionLayer == 13 || collider2.GetComponent<CollisionDetector>().collisionLayer == 13)
            {

                if (randomNum == 2)
                {
                    this.GetComponent<SpriteRenderer>().sprite = expanse2figures;
                }
                else
                {
                    this.GetComponent<SpriteRenderer>().sprite = expanse1;
                }
            }
            else if (collider1.GetComponent<CollisionDetector>().collisionLayer == 14 || collider2.GetComponent<CollisionDetector>().collisionLayer == 14)
            {
                if (randomNum == 2)
                {
                    this.GetComponent<SpriteRenderer>().sprite = expanse2figures;
                }
                else
                {
                    this.GetComponent<SpriteRenderer>().sprite = expanse2;
                }
            }
            else if (collider1.GetComponent<CollisionDetector>().collisionLayer == 15 || collider2.GetComponent<CollisionDetector>().collisionLayer == 15)
            {
                if (randomNum == 2)
                {
                    this.GetComponent<SpriteRenderer>().sprite = expanse3figures;
                }
                else
                {
                    this.GetComponent<SpriteRenderer>().sprite = expanse3;
                }
            }
            else
            {
                this.GetComponent<SpriteRenderer>().sprite = expanseFinal;
            }
        }
        rover.GetComponent<IconMove>().placeMarker();
        
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0.3f)
        {
            this.GetComponent<SpriteRenderer>().sprite = Screen;
        }
    }
}
