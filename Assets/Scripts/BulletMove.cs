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
    public GameObject explosionSys;

    private bool alreadyHit = false;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5);
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
            GameObject explosion = Instantiate(explosionSys);
            explosion.transform.position = this.transform.position;
            Destroy(gameObject);
        }

        if (gameObject.tag == "EnemyGroupTwo")
        {
            //Play audio:
            enemyHitSFX.PlayOneShot(enemyHitSFX.clip, 1f);
            playerShoot.enemiesHitGroupTwo++;
            GameObject explosion = Instantiate(explosionSys);
            explosion.transform.position = this.transform.position;
            Destroy(gameObject);
        }

        if (gameObject.tag == "EnemyGroupThree")
        {
            //Play audio:
            enemyHitSFX.PlayOneShot(enemyHitSFX.clip, 1f);
            playerShoot.enemiesHitGroupThree++;
            GameObject explosion = Instantiate(explosionSys);
            explosion.transform.position = this.transform.position;
            Destroy(gameObject);
        }

        if (gameObject.tag == "EnemyGroupFour")
        {
            //Play audio:
            enemyHitSFX.PlayOneShot(enemyHitSFX.clip, 1f);
            playerShoot.enemiesHitGroupFour++;
            GameObject explosion = Instantiate(explosionSys);
            explosion.transform.position = this.transform.position;
            Destroy(gameObject);
        }

        if (gameObject.tag == "Anubis")
        {
            if (alreadyHit)
                return;
            alreadyHit = true;
            if (playerShoot.enemiesHitGroupOne != 4 || playerShoot.enemiesHitGroupTwo != 4 || playerShoot.enemiesHitGroupThree != 4 || playerShoot.enemiesHitGroupFour != 4)
                return;
            //Play audio:
            enemyHitSFX.PlayOneShot(enemyHitSFX.clip, 1f);
            AnubisMove anubisMove = gameObject.GetComponent<AnubisMove>();
            anubisMove.hurtAnubis();
            GameObject explosion = Instantiate(explosionSys);
            explosion.transform.position = this.transform.position;
        }

        //if (gameObject.tag == "Obstacle")
        //{
        //    Destroy(this.gameObject);
        //}
    }
}
