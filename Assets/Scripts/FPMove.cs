using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Threading;
using UnityEngine;
using TMPro;

public class FPMove : MonoBehaviour
{
    //Public Variables:
    public float speed;
    public float sprintSpeed;
    public float camMoveXSpeed = 80f;
    public float camMoveYSpeed = 80f;
    public float movementLerpConstantAccel = 0.2f;
    public float movementLerpConstantDecel = 0.5f;
    public float sprintBurstTimeMax = 5f;
    public GameObject cameraObject;
    public GameObject hitGameObject;
    public GameObject tempHitGameObject;
    public MainManager mainManager;
    public TMP_Text igniteText;
    public CharacterController characterController;
    public Light itemLight;
    public AudioSource footstepOneSFX;
    public AudioSource footstepTwoSFX;
    public AudioSource footstepThreeSFX; 
    public AudioSource footstepFourSFX;
    public float walkSFXDelay = 0.5f;

    //Private Variables:
    private float actualSpeed;
    private float sprintBurstTime = 0f;
    private float xRotPos;
    private float yRotPos;
    private float actualSpeedForward = 0;
    private float actualSpeedRight = 0;
    private bool movingForward = false;
    private bool movingBackward = false;
    private bool movingLeft = false;
    private bool movingRight = false;
    private bool footstepClipPlaying = false;
    private Vector3 direction;
    private GameObject lightSourceGameObject; //Null reference workaround for keeping itemLight "filled" with a light

    // Start is called before the first frame update
    void Start()
    {
        //For movement:
        sprintBurstTime = sprintBurstTimeMax;
        actualSpeed = speed;
        direction = Vector3.zero;
        Cursor.lockState = CursorLockMode.Locked; //Lock cursor in middle of screen for traditional 1st person movement
    }

    // Update is called once per frame
    void Update()
    {
        PuzzleMechanics();

        //Get input and move every frame:
        GetInputAndMove();
    }

    private void GetInputAndMove()
    {
        //This are the intended movement values, so we reset them to 0 every call:
        float inputSpeedForward = 0;
        float inputSpeedRight = 0;
        movingForward = false;
        movingBackward = false;
        movingRight = false;
        movingLeft = false;

        //Get cardinal movement input:
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if (!footstepClipPlaying)
                StartCoroutine(playFootstepClip());
            movingForward = true;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if (!footstepClipPlaying)
                StartCoroutine(playFootstepClip());
            movingBackward = true;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (!footstepClipPlaying)
                StartCoroutine(playFootstepClip());
            movingRight = true;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (!footstepClipPlaying)
                StartCoroutine(playFootstepClip());
            movingLeft = true;
        }

        //Get sprint input:
        if (Input.GetKey(KeyCode.LeftShift) && (movingForward || movingLeft || movingRight))
        {
            if (sprintBurstTime > 0)
            {
                walkSFXDelay = 0.38f;
                actualSpeed = sprintSpeed;
                sprintBurstTime -= Time.deltaTime;
            }
            else
            {
                walkSFXDelay = 0.5f;
                actualSpeed = speed;
            }
        }
        else
        {
            walkSFXDelay = 0.5f;
            actualSpeed = speed;
            if (sprintBurstTime < sprintBurstTimeMax)
                sprintBurstTime += Time.deltaTime;
        }

        //Get cardinal movement input:
        if (movingForward)
        {
            inputSpeedForward = actualSpeed * Time.deltaTime;
        }
        if (movingBackward)
        {
            inputSpeedForward = -actualSpeed * Time.deltaTime;
        }
        if (movingRight)
        {
            inputSpeedRight = actualSpeed * Time.deltaTime;
        }
        if (movingLeft)
        {
            inputSpeedRight = -actualSpeed * Time.deltaTime;
        }

        //Normalize:
        if (inputSpeedForward != 0 && inputSpeedRight != 0)
        {
            inputSpeedForward *= 0.7071f;
            inputSpeedRight *= 0.7071f;
        }

        //Lerp movement with different constants for acceleration & deceleration:
        if (inputSpeedForward == 0)
            actualSpeedForward = Mathf.Lerp(actualSpeedForward, inputSpeedForward, movementLerpConstantDecel);
        else
            actualSpeedForward = Mathf.Lerp(actualSpeedForward, inputSpeedForward, movementLerpConstantAccel);
        if (inputSpeedRight == 0)
            actualSpeedRight = Mathf.Lerp(actualSpeedRight, inputSpeedRight, movementLerpConstantDecel);
        else
            actualSpeedRight = Mathf.Lerp(actualSpeedRight, inputSpeedRight, movementLerpConstantAccel);

        //Get mouse look input:
        xRotPos += Input.GetAxis("Mouse X") * camMoveXSpeed * Time.deltaTime;
        yRotPos += -Input.GetAxis("Mouse Y") * camMoveYSpeed * Time.deltaTime;
        yRotPos = Mathf.Clamp(yRotPos, -90f, 90f); //Clamp vertical movement

        //Call function which does actual movement:
        Move(xRotPos, yRotPos, actualSpeedForward, actualSpeedRight);
    }
    private void Move(float rotX, float rotY, float speedForward, float speedRight)
    {
        //Move player:
        Vector3 moveDirectionForward = transform.TransformDirection(Vector3.forward) * speedForward;
        Vector3 moveDirectionRight = transform.TransformDirection(Vector3.right) * speedRight;
        characterController.Move(moveDirectionForward + moveDirectionRight);

        //Rotate player & camera (seperately):
        Vector3 playerRotation = new Vector3(0, rotX, 0); /*If the player capsule recieved the y rotation, the player
                                                            would be able to float off into space...*/
        Vector3 cameraRotation = new Vector3(rotY, rotX, 0);
        this.transform.eulerAngles = playerRotation;
        cameraObject.transform.eulerAngles = cameraRotation;
    }

    private void Illuminate()
    {   
        GameObject lightedGameObject;
        RaycastHit lightingHit = new RaycastHit();
        Ray lightingRay = new Ray(cameraObject.transform.position, cameraObject.transform.forward);
        if (Physics.Raycast(lightingRay, out lightingHit, 3f))
        {
            lightedGameObject = lightingHit.transform.gameObject;
            //UnityEngine.Debug.Log("Hit Tag: " + lightedGameObject.tag);
            switch(lightedGameObject.tag)
            {
                case "TorchOne":
                    if (mainManager.torchOneBurning)
                    {
                        igniteText.enabled = false;
                        itemLight.enabled = false;
                        break;
                    }
                    igniteText.enabled = true;
                    itemLight = lightedGameObject.GetComponent<Light>();
                    itemLight.enabled = true;
                    break;
                case "TorchTwo":
                    if (mainManager.torchTwoBurning)
                    {
                        igniteText.enabled = false;
                        itemLight.enabled = false;
                        break;
                    }
                    igniteText.enabled = true;
                    itemLight = lightedGameObject.GetComponent<Light>();
                    itemLight.enabled = true;
                    break;
                case "TorchThree":
                    if (mainManager.torchThreeBurning)
                    {
                        igniteText.enabled = false;
                        itemLight.enabled = false;
                        break;
                    }
                    igniteText.enabled = true;
                    itemLight = lightedGameObject.GetComponent<Light>();
                    itemLight.enabled = true;
                    break;
                case "TorchFour":
                    if (mainManager.torchFourBurning)
                    {
                        igniteText.enabled = false;
                        itemLight.enabled = false;
                        break;
                    }
                    igniteText.enabled = true;
                    itemLight = lightedGameObject.GetComponent<Light>();
                    itemLight.enabled = true;
                    break;
                default:
                    igniteText.enabled = false;
                    itemLight.enabled = false;
                    break;
            }    
        } else
        {
            igniteText.enabled = false;
            itemLight.enabled = false;
        }
    }

    private void PuzzleMechanics()
    {
        Illuminate();
        //Is the player trying to collect an object?
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Attempt to light torch:
            RaycastHit hit = new RaycastHit();
            Ray ray = new Ray(cameraObject.transform.position, cameraObject.transform.forward);

            if (Physics.Raycast(ray, out hit, 3f))
            {
                hitGameObject = hit.transform.gameObject;
                if (hitGameObject.tag == "TorchOne" || hitGameObject.tag == "TorchTwo" ||
                    hitGameObject.tag == "TorchThree" || hitGameObject.tag == "TorchFour") //Only can ignite object if it is a torch
                {
                    mainManager.igniteTorch(hitGameObject.tag);
                    //itemLight = lightSourceGameObject.GetComponent<Light>();
                }
            }
        }
    }

    private IEnumerator playFootstepClip()
    {
        footstepClipPlaying = true;
        int footstepClipChoice = UnityEngine.Random.Range(1, 5);
        switch (footstepClipChoice)
        {
            case 1:
                footstepOneSFX.PlayOneShot(footstepOneSFX.clip, 1f);
                break;
            case 2:
                footstepTwoSFX.PlayOneShot(footstepTwoSFX.clip, 1f);
                break;
            case 3:
                footstepThreeSFX.PlayOneShot(footstepThreeSFX.clip, 1f);
                break;
            case 4:
                footstepFourSFX.PlayOneShot(footstepFourSFX.clip, 1f);
                break;
            default:
                break;
        }
        yield return new WaitForSeconds(walkSFXDelay);
        footstepClipPlaying = false;
    }
}