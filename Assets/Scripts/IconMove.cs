using UnityEngine;

public class IconMove : MonoBehaviour
{
    public float moveAmount = 0.5f;
    public float turnAmount = 360f;
    public GameObject collider1;
    public GameObject collider2;
    public GameObject screenBlockManager;
    public float timer = 0;
    public int direction;
    public GameObject marker;
    public GameObject map;

    void Start()
    {
        // Ensure default facing is right (0 degrees)
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        direction = 0;
    }

    public void MoveForward()
    {
        if (timer < 0)
        {
            AudioManager.instance.PlayMove();
            // Moves in whatever direction the icon is currently facing
            if (collider1.GetComponent<CollisionDetector>().collisionLayer != 6)
            {
                Invoke("MoveForwardReal", 0.1f);
                timer = 0.5f;
                screenBlockManager.GetComponent<ScreenBlockManager>().block();
            }
        }
    }

    void MoveForwardReal ()
    {
        // Moves in whatever direction the icon is currently facing
        if (collider1.GetComponent<CollisionDetector>().collisionLayer != 6)
        {
            var pos = transform.position;
            pos += transform.right * moveAmount;

            pos.x = Mathf.Clamp(pos.x, -7.26f, 0f);
            pos.y = Mathf.Clamp(pos.y, -2.93f, 4.25f);

            transform.position = pos;
        }
    }

    public void TurnLeft()
    {
        if (timer < 0)
        {
            Invoke("TurnLeftReal", 0.1f);
            timer = 0.5f;
            screenBlockManager.GetComponent<ScreenBlockManager>().block();
        }
    }

    void TurnLeftReal ()
    {
        AudioManager.instance.PlayMove();
        transform.Rotate(0f, 0f, turnAmount);
        direction++;
        direction = direction % 4;
    }

    public void TurnRight()
    {
        if (timer < 0)
        {
            Invoke("TurnRightReal", 0.1f);
            timer = 0.5f;
            screenBlockManager.GetComponent<ScreenBlockManager>().block();
        }
    }

    void TurnRightReal ()
    {
        AudioManager.instance.PlayMove();
        transform.Rotate(0f, 0f, -turnAmount);
        if (direction == 0)
        {
            direction = 3;
        }
        else
        {
            direction--;
        }
        direction = direction % 4;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
    }

    public void placeMarker ()
    {
        if (collider1.GetComponent<CollisionDetector>().collisionLayer == 6)
        {
            Instantiate(marker, collider1.transform.position, collider1.transform.localRotation, map.transform);
        }
        else if (collider2.GetComponent<CollisionDetector>().collisionLayer == 6 && collider1.GetComponent<CollisionDetector>().collisionLayer != 8)
        {
            Instantiate(marker, collider2.transform.position, collider2.transform.localRotation, map.transform);
        }
    }
}

