using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    //Public Variables:
    public Vector3 direction;
    public float speed = 0.1f;
    public PlayerShoot playerShoot;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 newPosition = speed * transform.forward * Time.deltaTime;

        this.transform.position += newPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject gameObject = other.gameObject;
        
        if (gameObject.tag == "EnemyGroupOne")
        {
            playerShoot.enemiesHitGroupOne++;
            Destroy(this.gameObject);
            Destroy(gameObject);
        }

        if (gameObject.tag == "EnemyGroupTwo")
        {
            playerShoot.enemiesHitGroupTwo++;
            Destroy(this.gameObject);
            Destroy(gameObject);
        }

        if (gameObject.tag == "EnemyGroupThree")
        {
            playerShoot.enemiesHitGroupThree++;
            Destroy(this.gameObject);
            Destroy(gameObject);
        }

        if (gameObject.tag == "EnemyGroupFour")
        {
            playerShoot.enemiesHitGroupFour++;
            Destroy(this.gameObject);
            Destroy(gameObject);
        }
    }
}
