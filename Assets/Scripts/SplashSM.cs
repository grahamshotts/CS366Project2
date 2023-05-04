using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SplashSM : MonoBehaviour
{
    public TMP_Text start;
    public Button toMenuButton;
    public TMP_Text toMenu;
    private AudioSource relax;
    // Start is called before the first frame update
    void Start()
    {
        relax = GetComponent<AudioSource>();
        start.text = "Shroud of Anubis";
        toMenuButton.onClick.AddListener(OntoMenuButtonClick);
        toMenu.text = "Press to Start";
    }

    // Update is called once per frame
    void Update()
    {
        if (!relax.isPlaying)
            relax.PlayOneShot(relax.clip, 0.5f);
    }

    private void OntoMenuButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");
    }

}

