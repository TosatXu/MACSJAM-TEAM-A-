using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public int collisionLayer;

    void OnTriggerStay2D(Collider2D collision)
    {
        collisionLayer = collision.gameObject.layer;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collisionLayer = 0;
    }
}
