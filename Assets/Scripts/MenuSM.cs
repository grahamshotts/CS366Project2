using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuSM : MonoBehaviour
{
    public Button startGameButton;
    public Button controlsButton;
    public Button backButton;
    public TMP_Text instructions;
    public TMP_Text controls;
    private AudioSource relax;
    // Start is called before the first frame update
    void Start()
    {
        relax = GetComponent<AudioSource>();
        startGameButton.onClick.AddListener(OnstartGameButtonClick);
        controlsButton.onClick.AddListener(OncontrolButtonClick);
        backButton.onClick.AddListener(OnbackButtonClick);
        backButton.gameObject.SetActive(false);
        controls.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!relax.isPlaying)
            relax.PlayOneShot(relax.clip, 0.5f);
    }

    private void OnstartGameButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
    private void OncontrolButtonClick()
    {
        controls.gameObject.SetActive(true);
        instructions.gameObject.SetActive(false);
        backButton.gameObject.SetActive(true);
        startGameButton.gameObject.SetActive(false);
        controlsButton.gameObject.SetActive(false);
    }

    private void OnbackButtonClick()
    {
        controls.gameObject.SetActive(false);
        instructions.gameObject.SetActive(true);
        backButton.gameObject.SetActive(false);
        startGameButton.gameObject.SetActive(true);
        controlsButton.gameObject.SetActive(true);
    }
}
