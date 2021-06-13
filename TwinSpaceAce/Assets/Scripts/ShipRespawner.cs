using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipRespawner : MonoBehaviour
{
    public ShipMover shipReference;
    public Text respawnValue;

    // Start is called before the first frame update
    void Start()
    {
        shipReference.onShipDeath += RunRespawn;
        respawnValue.text = shipReference.ShipLives.ToString();
    }

    private void OnDisable()
    {
        shipReference.onShipDeath -= RunRespawn;
    }

    void RunRespawn()
    {
        respawnValue.text = shipReference.ShipLives.ToString();
        foreach(var obj in GameObject.FindGameObjectsWithTag("Projectile"))
        {
            if(obj.name.Contains("Enemy"))
                Destroy(obj);
        }
        StartCoroutine(RespawnPlayer());
    }

    private IEnumerator RespawnPlayer()
    {
        float normalizedTime = 0;
        float respawnTime = 1;
        while (normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / respawnTime;
            yield return null;
        }
        shipReference.transform.position = new Vector3(0, 0, 0);
        shipReference.gameObject.SetActive(true);
    }
}
