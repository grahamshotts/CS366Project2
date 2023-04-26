using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    //Public Variables:
    public int burningTorchCount = 0;
    public bool torchOneBurning = false;
    public bool torchTwoBurning = false;
    public bool torchThreeBurning = false;
    public bool torchFourBurning = false;
    public AudioSource igniteSFX; 
    public AudioSource gateOpenSFX;
    public AudioSource misirlouMusic;
    public AudioSource flameOfAtenMusic;
    public ParticleSystem torchOnePS;
    public ParticleSystem torchTwoPS;
    public ParticleSystem torchThreePS;
    public ParticleSystem torchFourPS;
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
    public GameObject manaPrefab;

    //Private Variables:
    private GameObject tempManaObject;

    // Start is called before the first frame update
    void Start()
    {
        misirlouMusic.PlayOneShot(misirlouMusic.clip, 1f);

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
        
    }

    public void igniteTorch(string torchTag)
    {
        switch(torchTag)
        {
            case "TorchOne":
                if (torchOneBurning)
                    break;
                igniteSFX.PlayOneShot(igniteSFX.clip, 1f);
                burningTorchCount++;
                torchOneBurning = true;
                torchOnePS.Play();
                break;
            case "TorchTwo":
                if (torchTwoBurning)
                    break;
                igniteSFX.PlayOneShot(igniteSFX.clip, 1f);
                burningTorchCount++;
                torchTwoBurning = true;
                torchTwoPS.Play();
                break;
            case "TorchThree":
                if (torchThreeBurning)
                    break;
                igniteSFX.PlayOneShot(igniteSFX.clip, 1f);
                burningTorchCount++;
                torchThreeBurning = true;
                torchThreePS.Play();
                break;
            case "TorchFour":
                if (torchFourBurning)
                    break;
                igniteSFX.PlayOneShot(igniteSFX.clip, 1f);
                burningTorchCount++;
                torchFourBurning = true;
                torchFourPS.Play();
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

    private IEnumerator gateOpen()
    {
        //Do something with gate, boss, shader, etc...
        yield return new WaitForSeconds(3f);
        misirlouMusic.Pause();
        gateOpenSFX.PlayOneShot(gateOpenSFX.clip, 1f);
        UnityEngine.Debug.Log("MainManager: Gate opened...");
        yield return new WaitForSeconds(5f);
        flameOfAtenMusic.PlayOneShot(flameOfAtenMusic.clip, 1f);
    }
}
