using UnityEngine;

public class IconMove : MonoBehaviour
{
    public float moveAmount = 0.5f;
    public float turnAmount = 360f;

    public void MoveForward()
    {
        transform.position += transform.right * moveAmount;
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

