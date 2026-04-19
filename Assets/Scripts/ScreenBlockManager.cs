using UnityEngine;

public class ScreenBlockManager : MonoBehaviour
{
    public GameObject blocker;

    public void block ()
    {
        Instantiate(blocker);
    }
}
