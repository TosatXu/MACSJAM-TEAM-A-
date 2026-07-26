using UnityEngine;

public class Encounter3 : MonoBehaviour
{
    int moveCounter = 0;

    private void Start()
    {
        moveDown();
    }

    private void moveDown ()
    {
        AudioManager.instance.PlayMonsterRoar();
        moveCounter++;
        if (moveCounter > 10)
        {
            Destroy(this.gameObject);
        }
            
        transform.position += transform.up * -0.48f;

        Invoke("moveDown", 0.3f);
    }
}
