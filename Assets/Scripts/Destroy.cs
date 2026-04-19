using System;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public Boolean overlap;
    public GameObject text;
    public GameObject monster;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            overlap = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        overlap = false;
    }

    public void destroySelf ()
    {
        if (overlap)
        {
            Instantiate(monster);
            Instantiate(text);
            this.gameObject.SetActive(false);
        }
    }
}
