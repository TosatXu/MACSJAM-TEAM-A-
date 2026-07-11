using UnityEngine;

public class MapMove : MonoBehaviour
{
    public float moveAmount = 0.5f;
    public GameObject rover;

    public void movemap ()
    {
        if (rover.GetComponent<IconMove>().timer < 0)
        {
            AudioManager.instance.PlayMove();
            // Moves in whatever direction the icon is currently facing
            if (rover.GetComponent<IconMove>().collider1.GetComponent<CollisionDetector>().collisionLayer != 6)
            {
                Invoke("movemapReal", 0.1f);
                rover.GetComponent<IconMove>().timer = 0.5f;
                rover.GetComponent<IconMove>().screenBlockManager.GetComponent<ScreenBlockManager>().block();
            }
        }
    }

    void movemapReal ()
    {
        if (rover.GetComponent<IconMove>().collider1.GetComponent<CollisionDetector>().collisionLayer != 6)
        {
            var pos = transform.position;
            if (rover.GetComponent<IconMove>().direction == 0)
            {
                pos += -transform.right * moveAmount;
            }
            if (rover.GetComponent<IconMove>().direction == 1)
            {
                pos += -transform.up * moveAmount;
            }
            if (rover.GetComponent<IconMove>().direction == 2)
            {
                pos += transform.right * moveAmount;
            }
            if (rover.GetComponent<IconMove>().direction == 3)
            {
                pos += transform.up * moveAmount;
            }


            transform.position = pos;
        }
    }
}
