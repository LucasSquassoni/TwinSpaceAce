using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ShipMover : MonoBehaviour
{
    public float moveSpeed;
    public float moveTilt;
    public float tiltSpeed;
    public Action onShipDeath;
    public ParticleSystem particles;

    Vector3 moveVec1;
    Vector3 moveVec2;
    private int numberOfControllers;
    private Rigidbody shipRigidbody;
    [SerializeField]private int shipLives = 3;

    public int NumberOfControllers { get => numberOfControllers; set => numberOfControllers = value; }
    public Vector3 MoveVec1 { get => moveVec1; set => moveVec1 = value; }
    public Vector3 MoveVec2 { get => moveVec2; set => moveVec2 = value; }
    public int ShipLives { get => shipLives; set => shipLives = value; }

    private void Start()
    {
        shipRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        shipRigidbody.velocity = Vector3.zero;
        shipRigidbody.angularVelocity = Vector3.zero;
        if (numberOfControllers == 1)
        {
            moveSpeed = 2.5f;
        }
        else if(numberOfControllers == 2)
        {
            moveSpeed = 5;
        }

        shipRigidbody.position += (moveVec1 + moveVec2) * moveSpeed * Time.deltaTime;
        particles.transform.position = shipRigidbody.position;
        transform.GetChild(0).rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, (moveVec1 + moveVec2).x * -moveTilt), tiltSpeed * Time.deltaTime);

        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, minScreenBounds.x, maxScreenBounds.x), /*Mathf.Clamp(transform.position.y, minScreenBounds.y + 1, maxScreenBounds.y - 3)*/transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Projectile")
        {
            shipLives--;
            if (shipLives > 0)
            {
                if (onShipDeath != null)
                {
                    onShipDeath();
                }
            }
            else
            {
                ScoreManager.Instance.SaveHighScore();
                SceneManager.LoadScene("Main Menu");
            }
            gameObject.SetActive(false);
            AudioManager.Instance.PlayExplosionSound();
            particles.Play();
            Destroy(other.gameObject);
        }
    }
}
