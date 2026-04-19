using UnityEngine;

public class Popup : MonoBehaviour
{
    public Vector3 speed = new Vector3(0f, 2f);

    private void Update()
    {
        transform.position += speed * Time.deltaTime;
        if (transform.position.y < -50)
        {
            Destroy(this);
        }
    }
}
