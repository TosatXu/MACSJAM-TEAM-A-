using UnityEngine;

public class Popup : MonoBehaviour
{
    public Vector3 speed = new Vector3(0f, 2f);

    private void Start()
    {
        this.GetComponent<Rigidbody2D>().linearVelocity = speed;
    }

    private void Update()
    {
        if (transform.position.y > 10)
        {
            this.gameObject.SetActive(false);
        }
    }
}
