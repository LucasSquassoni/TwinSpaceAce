using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public bool canMove;
    public float moveSpeed;

    private EnemyBezier bezier;
    private int currentPoint;
    // Start is called before the first frame update
    void Start()
    {
        bezier = GetComponent<EnemyBezier>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if(transform.position != bezier.Positions[currentPoint])
            {
                GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(transform.position, bezier.Positions[currentPoint], moveSpeed * Time.deltaTime));
            }
            else
            {
                currentPoint = (currentPoint + 1) % bezier.Positions.Length;
            }
        }
    }
}
