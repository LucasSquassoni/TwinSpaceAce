using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;

    public AudioSource fireSound;
    public AudioSource explosionSound;
    public AudioSource enemyDeathSound;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlayFireSound()
    {
        fireSound.Play();
    }

    public void PlayExplosionSound()
    {
        explosionSound.Play();
    }

    public void PlayEnemyDeathSound()
    {
        enemyDeathSound.Play();
    }
}
