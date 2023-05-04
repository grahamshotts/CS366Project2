using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainManager : MonoBehaviour
{
    //Public Variables:
    //Torch
    public int burningTorchCount = 0;
    public bool torchOneBurning = false;
    public bool torchTwoBurning = false;
    public bool torchThreeBurning = false;
    public bool torchFourBurning = false;
    public ParticleSystem torchOnePS;
    public ParticleSystem torchTwoPS;
    public ParticleSystem torchThreePS;
    public ParticleSystem torchFourPS;
    public GameObject torchOneLight;
    public GameObject torchTwoLight;
    public GameObject torchThreeLight;
    public GameObject torchFourLight;
    //Sound
    public AudioSource igniteSFX;
    public AudioSource lockClickSFX;
    public AudioSource gateOpenSFX;
    public AudioSource templeofAmunRaMusic;
    public AudioSource flameOfAtenMusic;
    public AudioSource birminghamMusic;
    //Mana Spawns
    public Vector3 manaOneSpawnOne;
    public Vector3 manaOneSpawnTwo;
    public Vector3 manaOneSpawnThree;
    public Vector3 manaTwoSpawnOne;
    public Vector3 manaTwoSpawnTwo;
    public Vector3 manaTwoSpawnThree;
    public Vector3 manaThreeSpawnOne;
    public Vector3 manaThreeSpawnTwo;
    public Vector3 manaThreeSpawnThree;
    public Vector3 manaFourSpawnOne;
    public Vector3 manaFourSpawnTwo;
    public Vector3 manaFourSpawnThree;
    //Game objects
    public GameObject manaPrefab;
    public GameObject gates;
    //Text
    public TMP_Text manaCollectedText;
    public TMP_Text manaRemainingText;
    public TMP_Text healthText;
    public TMP_Text igniteText;

    //Player
    public Vector3 playerSpawnPoint;
    public GameObject playerPrefab;
    public GameObject playerInstance;
    public float gameMoveSpeed = 1.2f;

    public bool birminghamMode = false;

    //Private Variables:
    private GameObject tempManaObject;
    private bool gatesMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        playerInstance = Instantiate(playerPrefab);
        FPMove fpMove = playerInstance.GetComponent<FPMove>();
        fpMove.mainManager = this;
        fpMove.manaCollectedText = manaCollectedText;
        fpMove.manaRemainingText = manaRemainingText;
        fpMove.healthText = healthText;
        fpMove.igniteText = igniteText;
        playerInstance.transform.position = playerSpawnPoint;

        templeofAmunRaMusic.PlayOneShot(templeofAmunRaMusic.clip, 1f);

        torchOnePS.Stop();
        torchTwoPS.Stop();
        torchThreePS.Stop();
        torchFourPS.Stop();

        int randomNum = UnityEngine.Random.Range(1, 4);
        tempManaObject = Instantiate(manaPrefab);
        switch (randomNum)
        {
            case 1:
                tempManaObject.transform.position = manaOneSpawnOne;
                break;
            case 2:
                tempManaObject.transform.position = manaOneSpawnTwo;
                break;
            case 3:
                tempManaObject.transform.position = manaOneSpawnThree;
                break;
            default:
                UnityEngine.Debug.Log("MainManager: Invalid spawn number...");
                break;
        }

        randomNum = UnityEngine.Random.Range(1, 4);
        tempManaObject = Instantiate(manaPrefab);
        switch (randomNum)
        {
            case 1:
                tempManaObject.transform.position = manaTwoSpawnOne;
                break;
            case 2:
                tempManaObject.transform.position = manaTwoSpawnTwo;
                break;
            case 3:
                tempManaObject.transform.position = manaTwoSpawnThree;
                break;
            default:
                UnityEngine.Debug.Log("MainManager: Invalid spawn number...");
                break;
        }

        randomNum = UnityEngine.Random.Range(1, 4);
        tempManaObject = Instantiate(manaPrefab);
        switch (randomNum)
        {
            case 1:
                tempManaObject.transform.position = manaThreeSpawnOne;
                break;
            case 2:
                tempManaObject.transform.position = manaThreeSpawnTwo;
                break;
            case 3:
                tempManaObject.transform.position = manaThreeSpawnThree;
                break;
            default:
                UnityEngine.Debug.Log("MainManager: Invalid spawn number...");
                break;
        }

        randomNum = UnityEngine.Random.Range(1, 4);
        tempManaObject = Instantiate(manaPrefab);
        switch (randomNum)
        {
            case 1:
                tempManaObject.transform.position = manaFourSpawnOne;
                break;
            case 2:
                tempManaObject.transform.position = manaFourSpawnTwo;
                break;
            case 3:
                tempManaObject.transform.position = manaFourSpawnThree;
                break;
            default:
                UnityEngine.Debug.Log("MainManager: Invalid spawn number...");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gatesMoving)
        {
            gates.transform.position = new Vector3(gates.transform.position.x, gates.transform.position.y - gameMoveSpeed * Time.deltaTime, gates.transform.position.z);
            if (gates.transform.position.y < -6f)
            {
                gatesMoving = false;
                gates.transform.position = new Vector3(gates.transform.position.x, -6f, gates.transform.position.z);
            }
        }
        if (Input.GetKeyDown(KeyCode.BackQuote) && !birminghamMode)
        {
            birminghamMode = true;
            healthText.text = "Yes/10 Health";
            manaRemainingText.text = "Yes/100 Mana";
            manaCollectedText.text = "Yes Mana Recharges";
            templeofAmunRaMusic.Stop();
            birminghamMusic.PlayOneShot(birminghamMusic.clip);
        }
    }

    public void igniteTorch(string torchTag)
    {
        switch(torchTag)
        {
            case "TorchOne":
                if (torchOneBurning)
                    break;
                StartCoroutine(igniteTorch());
                burningTorchCount++;
                torchOneBurning = true;
                torchOnePS.Play();
                torchOneLight.SetActive(true);
                break;
            case "TorchTwo":
                if (torchTwoBurning)
                    break;
                StartCoroutine(igniteTorch());
                burningTorchCount++;
                torchTwoBurning = true;
                torchTwoPS.Play();
                torchTwoLight.SetActive(true);
                break;
            case "TorchThree":
                if (torchThreeBurning)
                    break;
                StartCoroutine(igniteTorch());
                burningTorchCount++;
                torchThreeBurning = true;
                torchThreePS.Play();
                torchThreeLight.SetActive(true);
                break;
            case "TorchFour":
                if (torchFourBurning)
                    break;
                igniteSFX.PlayOneShot(igniteSFX.clip, 1f);
                burningTorchCount++;
                torchFourBurning = true;
                torchFourPS.Play();
                torchFourLight.SetActive(true);
                break;
            default:
                UnityEngine.Debug.Log("MainManager: Unknown Object Attempted To Be Ignited...");
                break;
        }

        if (burningTorchCount == 4)
        {
            StartCoroutine(gateOpen());
        }
    }

    private IEnumerator igniteTorch()
    {
        igniteSFX.PlayOneShot(igniteSFX.clip, 1f);
        yield return new WaitForSeconds(2f);
        lockClickSFX.PlayOneShot(lockClickSFX.clip, 1f);
    }

    private IEnumerator gateOpen()
    {
        //Do something with gate, boss, shader, etc...
        yield return new WaitForSeconds(3f);
        templeofAmunRaMusic.Pause();
        gateOpenSFX.PlayOneShot(gateOpenSFX.clip, 1f);
        UnityEngine.Debug.Log("MainManager: Gate opened...");
        yield return new WaitForSeconds(1.2f);
        gatesMoving = true;
        yield return new WaitForSeconds(3.8f);
        if(!birminghamMode)
            flameOfAtenMusic.PlayOneShot(flameOfAtenMusic.clip, 1f);
    }

}
