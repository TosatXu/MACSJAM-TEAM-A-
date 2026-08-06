using UnityEngine;

public class Device : MonoBehaviour
{
    int counter;
    public GameObject endText;
    bool overlap;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            overlap = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
            overlap = false;
    }

    void Awake()
    {
        counter = 10;
        Invoke("fly", 0.1f);
    }

    void fly ()
    {
        if (counter > 0)
        {
            transform.position += Vector3.down * 0.96f;
            Invoke("fly", 0.2f);
            counter--;
        }
    }

    public void trigger()
    {
        if (overlap)
        {
            endText.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
