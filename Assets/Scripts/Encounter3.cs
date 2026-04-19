using UnityEngine;

public class Encounter3 : MonoBehaviour
{
    int moveCounter = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            moveCounter++;
            if (moveCounter > 4)
            {
                Destroy(this.gameObject);
            }
            
            transform.position += transform.up * -0.96f;
        }
    }
}
