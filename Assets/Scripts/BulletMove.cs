using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 0.1f;

    private Vector3 screenPosition;
    private Vector3 worldPosition;
    // Start is called before the first frame update
    void Start()
    {
        screenPosition = Input.mousePosition;
        screenPosition.z = Camera.main.nearClipPlane + 1;

        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        //direction = new Vector2(1, 0);
        Destroy(this.gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {

        Move();

    }

    private void Move()
    {
        //Vector3 newPosition = new Vector3(speed * transform.position.x * Time.deltaTime, speed * transform.position.y * Time.deltaTime, 0);
        //Vector3 newPosition = new Vector3(direction.x, direction.y, speed * transform.position.z * Time.deltaTime);
        

        Vector3 newPosition = new Vector3(speed * transform.forward.x * Time.deltaTime, 0, speed * transform.forward.z * Time.deltaTime);

        this.transform.position += newPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject gameObject = other.gameObject;
        if (gameObject.tag == "Player")
        {
            //Destroy(gameObject);
            Destroy(this.gameObject);
        }
        if (gameObject.tag == "Enemy")
        {
            //Destroy(gameObject);
            Destroy(this.gameObject);
        }
    }
}
