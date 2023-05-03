using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    //Public Variables:
    public AudioSource enemyHitSFX;
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
            //Play audio:
            enemyHitSFX.PlayOneShot(enemyHitSFX.clip, 1f);
            playerShoot.enemiesHitGroupOne++;
            Destroy(gameObject);
        }

        if (gameObject.tag == "EnemyGroupTwo")
        {
            //Play audio:
            enemyHitSFX.PlayOneShot(enemyHitSFX.clip, 1f);
            playerShoot.enemiesHitGroupTwo++;
            Destroy(gameObject);
        }

        if (gameObject.tag == "EnemyGroupThree")
        {
            //Play audio:
            enemyHitSFX.PlayOneShot(enemyHitSFX.clip, 1f);
            playerShoot.enemiesHitGroupThree++;
            Destroy(gameObject);
        }

        if (gameObject.tag == "EnemyGroupFour")
        {
            //Play audio:
            enemyHitSFX.PlayOneShot(enemyHitSFX.clip, 1f);
            playerShoot.enemiesHitGroupFour++;
            Destroy(gameObject);
        }
    }
}
