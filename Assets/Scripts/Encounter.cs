using UnityEngine;
using UnityEngine.EventSystems;

public class Encounter3 : MonoBehaviour
{
    int moveCounter = 0;
    public Vector3 direction;
    public int moveTotal;
    public float moveTimeIncrement;

    private void Start()
    {
        AudioManager.instance.PlayMonsterRoar();
        Invoke("move", moveTimeIncrement);
    }

    private void move ()
    {
        AudioManager.instance.PlayMonsterRoar();
        moveCounter++;
        if (moveCounter > moveTotal)
        {
            Destroy(this.gameObject);
        }
            
        transform.position += direction * 0.48f;

        Invoke("move", moveTimeIncrement);
    }
}
