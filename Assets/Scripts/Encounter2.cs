using UnityEngine;

public class Encounter2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    int moveCounter = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioManager.instance.PlayMonsterRoar2();
        if (collision.gameObject.layer == 11)
        {
            moveCounter++;
            if (moveCounter > 4)
            {
                Destroy(this.gameObject);
            }
            if (moveCounter > 2)
            {
                transform.position += transform.right * -0.48f;
            }
        }
    }
}
