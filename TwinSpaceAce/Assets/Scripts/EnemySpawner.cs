using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Vector3> positions;
    public List<List<bool>> waves;
    public GameObject enemy1, enemy2, enemy3;
    public Transform wave1Parent, wave2Parent, wave3Parent;
    public float fadeTime;
    public int[] waveTransitionThreshold;
    public ParticleSystem[] particles;
    private int[] waveLevel = new int[3];

    private GameObject currentSpawnObject;
    private int[] waveIncrement = new int[3];
    // Start is called before the first frame update
    void Start()
    {
        currentSpawnObject = enemy1;
        waves = new List<List<bool>>();
        for (int i = 0; i < 3; i++)
        {
            var boolList = new List<bool>();
            waves.Add(boolList);
            SpawnWave(i);
            waveLevel[i] = 1;
        }
    }

    void SpawnWave(int waveNumber)
    {
        List<GameObject> fadeWave = new List<GameObject>();
        for (int i = 0; i < positions.Count; i++)
        {
            var enemy = Instantiate(getSpawnEnemy(waveNumber), getTargetParent(waveNumber));
            enemy.GetComponentInChildren<MeshRenderer>().material.color = new Color(1,1,1,0);
            enemy.GetComponentInChildren<BoxCollider>().enabled = false;
            var enemyShooter = enemy.GetComponent<EnemyShooter>();
            enemyShooter.enemySpawner = this;
            enemyShooter.wave = waveNumber;
            enemyShooter.wavePos = i;
            enemy.transform.localPosition = positions[i];
            if (waves[waveNumber].Count < 7)
                waves[waveNumber].Add(true);
            else
                waves[waveNumber][i] = true;
            fadeWave.Add(enemy);
        }
        StartCoroutine(FadeInWave(fadeWave));
    }

    private Transform getTargetParent(int waveNum)
    {
        switch (waveNum)
        {
            case 1:
                return wave2Parent;
            case 2:
                return wave3Parent;
            default:
                return wave1Parent;
        }
    }

    private GameObject getSpawnEnemy(int waveNum)
    {
        switch (waveLevel[waveNum])
        {
            case 2:
                return enemy2;
            case 3:
                return enemy3;
            default:
                return enemy1;
        }
    }

    public void SetEnemyDeath(int wave, int position)
    {
        waves[wave][position] = false;
        CheckWaveStatus(wave);
    }

    public void CheckWaveStatus(int waveNumber)
    {
        for (int i = 0; i < waves[waveNumber].Count; i++)
        {
            if (waves[waveNumber][i])
            {
                return;
            }
        }
        UpgradeWave(waveNumber);
        SpawnWave(waveNumber);
    }

    public void PlayDeathParticle(Vector3 deathPos)
    {
        for (int i = 0; i < particles.Length; i++)
        {
            if (!particles[i].isPlaying)
            {
                particles[i].transform.position = deathPos;
                particles[i].Play();
                return;
            }
            else
            {
                continue;
            }
        }
    }

    private void UpgradeWave(int waveNumber)
    {
        waveIncrement[waveNumber]++;
        if(waveTransitionThreshold[waveNumber] == waveIncrement[waveNumber])
        {
            if (waveLevel[waveNumber] < 3)
            {
                waveLevel[waveNumber]++;
                getTargetParent(waveNumber).GetComponent<Animator>().SetFloat("SpeedMultiplier", waveLevel[waveNumber]);
            }
            waveIncrement[waveNumber] = 0;
        }
    }

    private IEnumerator FadeInWave(List<GameObject> wave)
    {
        float normalizedTime = 0;
        while(normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / fadeTime;
            for (int i = 0; i < wave.Count; i++)
            {
                wave[i].GetComponentInChildren<MeshRenderer>().material.color = new Color(1, 1, 1, normalizedTime);
            }
            yield return null;
        }
        for (int i = 0; i < wave.Count; i++)
        {
            wave[i].GetComponentInChildren<BoxCollider>().enabled = true;
        }
    }
}
