using UnityEngine;

public class death : MonoBehaviour
{
    public GameObject deathScreen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            deathScreen.SetActive(true);
        }
    }
}
