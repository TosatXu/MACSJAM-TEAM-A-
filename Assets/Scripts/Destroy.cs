using System;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public Boolean overlap;
    public GameObject text;
    public GameObject monster;
    public GameObject map;
    public Vector3 spawnPoint;

    // Keeps this object visible after its event is triggered
    [SerializeField]
    private bool keepVisibleAfterTrigger;

    private bool hasTriggered;

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

    public void destroySelf ()
    {
        if (overlap)
        {
            TriggerItem();
            // if (monster != null)
            // {
            //     Instantiate(monster, this.transform.position + spawnPoint, this.transform.rotation, map.transform);
            // }
            // if (text != null)
            // {
            //     Instantiate(text);
            // }
            // transform.DetachChildren();
            // this.gameObject.SetActive(false);
        }
    }

    public void TriggerFromPhoto()
    {
        TriggerItem();
    }

    private void TriggerItem()
    {
        if (hasTriggered)
            return;

        hasTriggered = true;

        if (monster != null)
        {
            /*Transform parent = map != null ? map.transform : null;

            Instantiate(
                monster,
                transform.position + spawnPoint,
                transform.rotation,
                parent
            );*/
            monster.SetActive(true);
        }

        if (text != null)
            text.SetActive(true);

        // Hide the object unless it should remain visible
        if (!keepVisibleAfterTrigger)
        {
            transform.DetachChildren();
            gameObject.SetActive(false);
        }
    }
}
