using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipShooter : MonoBehaviour
{
    public GameObject projectile;
    public Transform firePointLeft, firePointRight;

    public List<GameObject> pooledObjects;
    public int amountToPool;

    private Transform firePoint;
    private GameObject bullet;
    private void Start()
    {
        InitialisePooledObjects();
    }

    private void InitialisePooledObjects()
    {
        if (SceneManager.GetActiveScene().name.Contains("Level"))
        {
            var dynamic = GameObject.Find("_Dynamic").transform;
            pooledObjects = new List<GameObject>();
            for (int i = 0; i < amountToPool; i++)
            {
                var obj = Instantiate(projectile, dynamic);
                obj.SetActive(false);
                obj.name = "Pellet " + i;
                pooledObjects.Add(obj);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bullet != null)
        {
            AudioManager.Instance.PlayFireSound();
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
            bullet = null;
        }
    }

    public GameObject GetPooledObject()
    {
        if (!gameObject.activeSelf)
        {
            return null;
        }
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeSelf)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    public void Fire1()
    {
        firePoint = firePointLeft;
        bullet = GetPooledObject();
    }

    public void Fire2()
    {
        firePoint = firePointRight;
        bullet = GetPooledObject();
    }
}
