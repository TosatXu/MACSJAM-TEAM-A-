using UnityEngine;

public class Encounter1 : MonoBehaviour
{
    int moveCounter = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioManager.instance.PlayMonsterRoar2();
        if (collision.gameObject.layer == 11)
        {
            moveCounter++;
        }
    }

    private void Update()
    {
        if (moveCounter >= 2) {
            this.gameObject.SetActive(false);
        }
    }
}
