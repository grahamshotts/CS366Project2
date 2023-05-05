using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    //Public Variables:
    public GameObject bullet;
    public GameObject shootPoint;
    public GameObject player;
    public FPMove manaControl;
    public AudioSource manaAttackSFX;
    public AudioSource outOfManaSFX;
    public float coolDownTime = 0.5f;
    public float bulletSpeed = 5f;
    public int enemiesHitGroupOne = 0;
    public int enemiesHitGroupTwo = 0;
    public int enemiesHitGroupThree = 0;
    public int enemiesHitGroupFour = 0;

    //Private Variables:
    private bool inCoolDown = false;

    // Start is called before the first frame update
    void Start()
    {
        //For controlling variables in FPMove:
        manaControl = player.GetComponent<FPMove>();
        manaControl.shooter = this;
    }

    // Update is called once per frame
    void Update()
    {
        //See if player is trying to shoot:
        if (Input.GetKey(KeyCode.Mouse0) && !inCoolDown)
        {
            if (manaControl.currentMana >= 10)
            {
                //Manage cool down with coroutine:
                inCoolDown = true;
                StartCoroutine(CoolDown());

                //Spawn magic bullet:
                GameObject go = Instantiate(bullet);
                go.transform.position = shootPoint.transform.position;
                go.transform.rotation = shootPoint.transform.rotation;
                BulletMove b = go.GetComponent<BulletMove>();
                b.speed = bulletSpeed + ((manaControl.actualSpeedForward) / Time.deltaTime);
                b.playerShoot = this;
                b.transform.rotation = shootPoint.transform.rotation;
                b.direction = shootPoint.transform.rotation.eulerAngles;

                //Manage mana:
                if (!manaControl.mainManager.birminghamMode)
                {
                    manaControl.mainManager.manaUsed += 10;
                    manaControl.currentMana -= 10;
                    manaControl.manaRemainingText.text = manaControl.currentMana.ToString("F1") + "/" + manaControl.manaMax + " Mana";
                }

                //Play audio:
                manaAttackSFX.PlayOneShot(manaAttackSFX.clip, 0.6f);
            }
            else
            {
                //Manage cool down with coroutine:
                inCoolDown = true;
                StartCoroutine(CoolDown());

                //Play audio:
                outOfManaSFX.PlayOneShot(outOfManaSFX.clip, 0.6f);
            }
        }
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(coolDownTime);
        inCoolDown = false;
    }
}
