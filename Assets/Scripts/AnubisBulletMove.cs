using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

public class AnubisBulletMove : MonoBehaviour
{
    //Public Variables:
    public Vector3 direction;
    public float speed = 0.1f;
    public GameObject explosionSys;
    public AnubisMove anubisMove;
    public int damageAmmount;
    FPMove fpMove;

    // Start is called before the first frame update
    void Start()
    {
        fpMove = anubisMove.playerObject.GetComponent<FPMove>();
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

        if (gameObject.tag == "Player")
        {
            //Play audio:
            fpMove.damagePlayer(damageAmmount);
            GameObject explosion = Instantiate(explosionSys);
            explosion.transform.position = this.transform.position;
            Destroy(gameObject);
        }
    }
}
