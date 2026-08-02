using UnityEngine;

public class Device : MonoBehaviour
{
    int counter;

    void Start()
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
}
