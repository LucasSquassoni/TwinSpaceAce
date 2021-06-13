using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileMover : MonoBehaviour
{
    public float projectileSpeed;
    public float lifeTime;
    public bool active;
    public int player;

    private void OnEnable()
    {
        active = true;
    }

    private void OnDisable()
    {
        active = false;
        CancelInvoke();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
            transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);

        if(transform.position.z > 53)
        {
            Deactivator();
        }
    }

    public void Deactivator()
    {
        gameObject.SetActive(false);
    }
}
