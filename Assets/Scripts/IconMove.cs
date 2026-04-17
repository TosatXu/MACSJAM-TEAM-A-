using UnityEngine;

public class IconMove : MonoBehaviour
{
    public float moveAmount = 0.5f;

    public void MoveRight()
    {
        transform.position += new Vector3(moveAmount, 0f, 0f);
    }
}

