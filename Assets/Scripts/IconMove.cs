using UnityEngine;

public class IconMove : MonoBehaviour
{
    public float moveAmount = 0.5f;
    public float turnAmount = 360f;
    public GameObject collider1;
    public GameObject collider2;

    void Start()
    {
        // Ensure default facing is right (0 degrees)
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public void MoveForward()
    {
        // Moves in whatever direction the icon is currently facing
        if (collider1.GetComponent<CollisionDetector>().collisionLayer != 6)
        {
            transform.position += transform.right * moveAmount;
        }
    }

    public void TurnLeft()
    {
        transform.Rotate(0f, 0f, turnAmount);
    }

    public void TurnRight()
    {
        transform.Rotate(0f, 0f, -turnAmount);
    }
}

