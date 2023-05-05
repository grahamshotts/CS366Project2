using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using UnityEngine.UI;
using TMPro;

public class GameOver : MonoBehaviour
{
    //public Manager manager;
    public TMP_Text endGame;

    public TMP_Text toMenu;
    public TMP_Text toTitle;
    public TMP_Text toGame;

    public TMP_Text manaUsedText;
    public TMP_Text timeElapsedText;
    public TMP_Text scoreText;

    public float timeElapsed = 0f;
    public float manaUsed = 0f;
    public float calculatedScore = 0f;
    private const string manaUsedKey = "ManaUsed";
    private const string timeElapsedKey = "TimeElapsed";

    //public ButtonEditor button;
    private AudioSource easy;
    public Button toMenuButton;
    public Button toTitleButton;
    public Button toGameButton;
    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = PlayerPrefs.GetFloat(timeElapsedKey);
        manaUsed = PlayerPrefs.GetFloat(manaUsedKey);
        calculatedScore = (manaUsed * (timeElapsed / 60f));

        manaUsedText.text = ((int) manaUsed).ToString();
        timeElapsedText.text = ((int) timeElapsed).ToString();
        scoreText.text = ((int) calculatedScore).ToString();

        easy = GetComponent<AudioSource>();
        endGame.text = "Game Over";
        //toMenuButton.onClick.AddListener(OntoMenuButtonClick);
        toTitleButton.onClick.AddListener(OntoTitleButtonClick);
        toGameButton.onClick.AddListener(OntoGameButtonClick);
        //toMenu.text = "Back to Menu";
        toTitle.text = "Title Screen";
        toGame.text = "Play Again";
        Cursor.lockState = CursorLockMode.None;
        //PlayerPrefs();
    }

    // Update is called once per frame
    void Update()
    {
        if (!easy.isPlaying)
            easy.PlayOneShot(easy.clip, 0.5f);
        if (Input.GetKey(KeyCode.Space))
            UnityEngine.SceneManagement.SceneManager.LoadScene("SplashScene");
}

    private void OntoMenuButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");
    }

    private void OntoTitleButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SplashScene");
    }

    private void OntoGameButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
}

