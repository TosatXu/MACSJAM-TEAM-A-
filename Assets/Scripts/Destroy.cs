using System;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public Boolean overlap;

    void OnTriggerStay2D(Collider2D collision)
    {
        overlap = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        overlap = false;
    }

    public void destroySelf ()
    {
        if (overlap)
        {
            this.gameObject.SetActive(false);
        }
    }
}
