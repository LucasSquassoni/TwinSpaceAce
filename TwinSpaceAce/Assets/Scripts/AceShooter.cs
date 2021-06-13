using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AceShooter : MonoBehaviour
{
    public static AceShooter Instance;

    public GameObject pelletPrefab;
    public Transform firePoint;

    public List<GameObject> pooledObjects;
    public int amountToPool;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        var dynamic = GameObject.Find("_Dynamic").transform;
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            var obj = Instantiate(pelletPrefab, dynamic);
            obj.SetActive(false);
            obj.name = "Pellet " + i;
            pooledObjects.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var bullet = GetPooledObject();
            if(bullet != null)
            {
                bullet.transform.position = firePoint.position;
                bullet.transform.rotation = transform.rotation;
                bullet.SetActive(true);
            }
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeSelf)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
