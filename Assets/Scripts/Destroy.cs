using System;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public Boolean overlap;
    public GameObject text;
    public GameObject monster;
    public GameObject map;
    public Vector3 spawnPoint;

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
            if (monster != null)
            {
                Instantiate(monster, this.transform.position + spawnPoint, this.transform.rotation, map.transform);
            }
            if (text != null)
            {
                Instantiate(text);
            }
            transform.DetachChildren();
            this.gameObject.SetActive(false);
        }
    }
}
