using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public int collisionLayer;
    public GameObject collisionObject;

    private void OnTriggerStay2D(Collider2D collision)
    {
        collisionLayer = collision.gameObject.layer;
        collisionObject = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == collisionObject)
        {
            collisionLayer = 0;
            collisionObject = null;
        }
    }
}