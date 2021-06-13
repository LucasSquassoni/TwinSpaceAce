using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public float minShootSec = 5;
    public float maxShootSec = 30;
    public int points = 0;

    public GameObject projectile;
    
    public EnemySpawner enemySpawner;
    public int wave, wavePos;

    private List<Transform> firePoints = new List<Transform>();

    private bool isSpawning;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform childTransform in transform)
        {
            if (childTransform == transform)
                continue;

            if (childTransform.name.Contains("FirePoint"))
            {
                firePoints.Add(childTransform);
            }
        }

        isSpawning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawning)
        {
            float timer = Random.Range(minShootSec, maxShootSec);
            Invoke("SpawnProjectile", timer);
            isSpawning = true;
        }
    }

    void SpawnProjectile()
    {
        for (int i = 0; i < firePoints.Count; i++)
        {
            Instantiate(projectile, firePoints[i].position, transform.rotation);
        }
        isSpawning = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        enemySpawner.SetEnemyDeath(wave, wavePos);
        enemySpawner.PlayDeathParticle(transform.position);
        Destroy(gameObject);
        AudioManager.Instance.PlayEnemyDeathSound();
        ScoreManager.Instance.levelScore += points;
        if(other.tag == "Projectile")
        {
            other.gameObject.SetActive(false);
        }
    }
}
