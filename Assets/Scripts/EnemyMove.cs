using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    //Public Variables:
    public float enemySpeed = 2f;
    public GameObject playerObject;
    public MainManager mainManager;
    public GameObject mainManagerObject;
    public FPMove player;
    public Vector3 homePosition;
    //public GameObject checkpointOne;
    //public GameObject checkpointTwo;
    //public GameObject checkpointThree;
    //public GameObject checkpointFour;
    public float homeOffset = 3f;
    public float zOffset = 1.08f;
    public float tooClose = 1f;
    public float collisionMargin = 0.2f;
    public float yRotateOffset = 90f;
    public enum enemyPositionEnum { one, two, three, four };
    public enemyPositionEnum enemyPos = enemyPositionEnum.one;
    //public ParticleSystem line;

    //Private Variables:
    private bool trackingPlayer = false;
    private Vector3 positionOne;
    private Vector3 positionTwo;
    private Vector3 positionThree;
    private Vector3 positionFour;
    private bool inPause = true;
    private bool isAttacking = false;
    private bool playerSet = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(endPause());

        mainManagerObject = GameObject.FindGameObjectWithTag("MainManager");
        mainManager = mainManagerObject.GetComponent<MainManager>();

        tooClose += UnityEngine.Random.Range(-100f, 100f) / 50f;

        positionOne = new Vector3(homePosition.x + homeOffset, zOffset, homePosition.z + homeOffset);
        positionTwo = new Vector3(homePosition.x - homeOffset, zOffset, homePosition.z + homeOffset);
        positionThree = new Vector3(homePosition.x - homeOffset, zOffset, homePosition.z - homeOffset);
        positionFour = new Vector3(homePosition.x + homeOffset, zOffset, homePosition.z - homeOffset);

        switch (enemyPos)
        {
            case enemyPositionEnum.one:
                this.transform.position = positionOne;
                break;
            case enemyPositionEnum.two:
                this.transform.position = positionTwo;
                break;
            case enemyPositionEnum.three:
                this.transform.position = positionThree;
                break;
            case enemyPositionEnum.four:
                this.transform.position = positionFour;
                break;
            default:
                UnityEngine.Debug.Log("EnemyMove: Undefined initial enemy position");
                break;
        }

        //line = Instantiate(line);
    }

    // Update is called once per frame
    void Update()
    {
        if (inPause)
        {
            return;
        }
        else
        {
            if (!playerSet)
            {
                playerSet = true;
                playerObject = mainManager.playerInstance;
                player = playerObject.GetComponent<FPMove>();
            }
        }

        if (Vector3.Distance(this.transform.position, playerObject.transform.position) < tooClose)
        {
            trackingPlayer = true;
            this.transform.LookAt(playerObject.transform.position);
            this.transform.Rotate(0, yRotateOffset, 0);
            this.transform.position += enemySpeed * this.transform.forward * Time.deltaTime;

            //Attack Mechanics
            if (!isAttacking && Vector3.Distance(this.transform.position, playerObject.transform.position) < 1f)
                StartCoroutine(attackPlayer());
            

            return;
        } else
        {
            trackingPlayer = false;
        }
        //line.Play();

        //StartCoroutine(lineParticle());
        switch (enemyPos)
        {
            case enemyPositionEnum.one:
                this.transform.LookAt(positionTwo);
                this.transform.position += enemySpeed * this.transform.forward * Time.deltaTime;
                this.transform.Rotate(0, yRotateOffset, 0);
                if (Vector3.Distance(this.transform.position, positionTwo) < collisionMargin)
                    enemyPos = enemyPositionEnum.two;
                break;
            case enemyPositionEnum.two:
                this.transform.LookAt(positionThree);
                this.transform.position += enemySpeed * this.transform.forward * Time.deltaTime;
                this.transform.Rotate(0, yRotateOffset, 0);
                if (Vector3.Distance(this.transform.position, positionThree) < collisionMargin)
                    enemyPos = enemyPositionEnum.three;
                break;
            case enemyPositionEnum.three:
                this.transform.LookAt(positionFour);
                this.transform.position += enemySpeed * this.transform.forward * Time.deltaTime;
                this.transform.Rotate(0, yRotateOffset, 0);
                if (Vector3.Distance(this.transform.position, positionFour) < collisionMargin)
                    enemyPos = enemyPositionEnum.four;
                break;
            case enemyPositionEnum.four:
                this.transform.LookAt(positionOne);
                this.transform.position += enemySpeed * this.transform.forward * Time.deltaTime;
                this.transform.Rotate(0, yRotateOffset, 0);
                if (Vector3.Distance(this.transform.position, positionOne) < collisionMargin)
                    enemyPos = enemyPositionEnum.one;
                break;
            default:
                UnityEngine.Debug.Log("EnemyMove: Undefined enemyPos enum");
                break;
        }
    }

    private IEnumerator endPause()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0f, 100f) / 100f);
        inPause = false;
    }

    private IEnumerator attackPlayer()
    {
        isAttacking = true;
        player.damagePlayer(1);
        yield return new WaitForSeconds(1);
        isAttacking = false;
    }

    //private IEnumerator lineParticle()
    //{
    //    line = Instantiate(line);
    //    line.transform.position = transform.position;
    //    line.Play();
    //    //transform.position = new Vector3(0, -500, 500);
    //    yield return new WaitForSeconds(1f);
    //    //ParticleSystemStopAction particleSystemStopAction = ParticleSystemStopAction.Destroy;
    //    line.Stop();
    //    //Destroy(line);
    //    //Destroy(this.gameObject);
    //}

    //private void OnTriggerEnter(Collider collision)
    //{
    //    GameObject gameObject = collision.gameObject;
    //    switch (enemyPos)
    //    {
    //        case enemyPositionEnum.one:
    //            if (gameObject.tag == checkpointTwo.tag)
    //            {
    //                enemyPos = enemyPositionEnum.two;
    //            }
    //            break;
    //        case enemyPositionEnum.two:
    //            if (gameObject.tag == checkpointThree.tag)
    //            {
    //                enemyPos = enemyPositionEnum.three;
    //            }
    //            break;
    //        case enemyPositionEnum.three:
    //            if (gameObject.tag == checkpointFour.tag)
    //            {
    //                enemyPos = enemyPositionEnum.four;
    //            }
    //            break;
    //        case enemyPositionEnum.four:
    //            if (gameObject.tag == checkpointOne.tag)
    //            {
    //                enemyPos = enemyPositionEnum.one;
    //            }
    //            break;
    //        default:
    //            break;
    //    }
    //}
}
