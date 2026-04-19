using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip backgroundMusic;
    public AudioClip moveSound;
    public AudioClip cameraSound;
    public AudioClip shovelSound;
    public AudioClip monsterRoar;
    public AudioClip monsterRoar2;

    void Awake()
    {
        // Singleton so other scripts can access it easily
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Play background music
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayMove()
    {
        sfxSource.PlayOneShot(moveSound);
    }

    public void PlayCamera()
    {
        sfxSource.PlayOneShot(cameraSound);
    }

    public void PlayShovel()
    {
        sfxSource.PlayOneShot(shovelSound);
    }
    public void PlayMonsterRoar()
    {
        sfxSource.PlayOneShot(monsterRoar);
    }

    public void PlayMonsterRoar2()
    {
        sfxSource.PlayOneShot(monsterRoar2);
    }
}