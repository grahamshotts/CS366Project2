using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet;
    public GameObject shootPoint;

    private bool inCoolDown = false;

    private Vector3 screenPosition;
    private Vector3 worldPosition;

    public GameObject player;
    public FPMove manaControl;

    /*private AudioSource PewPew;
    public AudioSource manaAttackSFX;
    public AudioSource manaPickupSFX;
    public AudioSource manaRechargeSFX;*/

    // Start is called before the first frame update
    void Start()
    {
        //PewPew = GetComponent<AudioSource>();

        manaControl = player.GetComponent<FPMove>();
        manaControl.shooter = this;
    }

    // Update is called once per frame
    void Update()
    {
        screenPosition = Input.mousePosition;
        screenPosition.z = Camera.main.nearClipPlane + 1;

        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        if (Input.GetKey(KeyCode.Space) && !inCoolDown && manaControl.currentMana >= 10)
        {
            /*if (!PewPew.isPlaying)
                PewPew.PlayOneShot(PewPew.clip, 0.6f);*/
            inCoolDown = true;
            GameObject go = Instantiate(bullet);
            go.transform.position = shootPoint.transform.position;
            go.transform.rotation = Quaternion.Euler(0, 0, 0);
            BulletMove b = go.GetComponent<BulletMove>();
            b.speed = 5f;
            b.transform.rotation = transform.rotation;
            Debug.Log("Rotation is: " + b.transform.rotation);
            b.direction = new Vector3(transform.rotation.y, 0, 1);
            manaControl.currentMana -= 10;
            manaControl.manaRemainingText.text = manaControl.currentMana.ToString("F1") + "/" + manaControl.manaMax + " Mana";
            Debug.Log("Current Mana is: " + manaControl.currentMana);

            Debug.Log(b.direction);
            StartCoroutine(CoolDown());
        }


    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        inCoolDown = false;
    }
}
