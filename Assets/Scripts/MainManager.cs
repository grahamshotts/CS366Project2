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
    public ParticleSystem torchOnePS;
    public ParticleSystem torchTwoPS;
    public ParticleSystem torchThreePS;
    public ParticleSystem torchFourPS;

    //Private Variables:

    // Start is called before the first frame update
    void Start()
    {
        torchOnePS.Stop();
        torchTwoPS.Stop();
        torchThreePS.Stop();
        torchFourPS.Stop();
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
        gateOpenSFX.PlayOneShot(gateOpenSFX.clip, 1f);
        UnityEngine.Debug.Log("MainManager: Gate opened...");
    }
}
