using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionParameters : MonoBehaviour
{
    private const float VALUESCALE = 0.002f;

    // ============== VARIABLES ==============

    [Header("Parameters")]
    public static bool isMusic = true;
    public static bool isSound = true;

    [Header("Objects to Drop")]
    public GameObject menu;
    public AudioSource audioSource;
    public Image soundImage;
    public Image musicImage;
    public RectTransform btnQuit;
    public RectTransform btnSound;
    public RectTransform btnMusic;
    public RectTransform btnQuitLevel;

    [Header("Sprites")]
    public Sprite soundOn;
    public Sprite soundOff;
    public Sprite musicOn;
    public Sprite musicOff;

    public bool menuIsOpen = false;
    private bool menuIsOnAnim = false;
    private bool iconeReduceHeight = true;

    // =======================================

    private void Start()
    {
        if(PlayerPrefs.GetInt("Sound", 1) == 0)
        {
            isSound = false;
            audioSource.volume = 0;
            soundImage.overrideSprite = soundOff;
        }
    }

    public void ShowMenu()
    {
        if(menuIsOnAnim)
        {
            return;
        }

        if (menuIsOpen)
        {
            return;
        }

        menu.SetActive(true);
        menuIsOpen = true;

        StartCoroutine(DownMenu());
    }

    public void HideMenu()
    {
        if (menuIsOnAnim)
        {
            return;
        }

        if (!menuIsOpen)
        {
            return;
        }

        StartCoroutine(UpMenu());
    }

    public void UpdateSound()
    {
        if(isSound)
        {
            isSound = false;
            audioSource.volume = 0;
            soundImage.overrideSprite = soundOff;
            PlayerPrefs.SetInt("Sound", 0);
        }
        else
        {
            isSound = true;
            audioSource.volume = 1;
            soundImage.overrideSprite = soundOn;
            PlayerPrefs.SetInt("Sound", 1);
        }
    }

    IEnumerator DownMenu()
    {
        Transform pannel = menu.transform.GetChild(0);
        menuIsOnAnim = true;

        while (pannel.localPosition.y > -8f)
        {
            yield return new WaitForSeconds(0.01f);

            pannel.localPosition = new Vector3(0, pannel.localPosition.y - 3, 0);
        }

        while (pannel.localPosition.y < 0f)
        {
            yield return new WaitForSeconds(0.01f);

            pannel.localPosition = new Vector3(0, pannel.localPosition.y + 1.5f, 0);
        }

        menuIsOnAnim = false;

        Time.timeScale = 0f;
    }

    IEnumerator UpMenu()
    {
        Time.timeScale = 1f;

        Transform pannel = menu.transform.GetChild(0);
        menuIsOnAnim = true;

        while (pannel.localPosition.y > -8f)
        {
            yield return new WaitForSeconds(0.01f);

            pannel.localPosition = new Vector3(0, pannel.localPosition.y - 1.5f, 0);
        }

        while (pannel.localPosition.y < 63f)
        {
            yield return new WaitForSeconds(0.01f);

            pannel.localPosition = new Vector3(0, pannel.localPosition.y + 3f, 0);
        }

        menuIsOnAnim = false;

        menu.SetActive(false);
        menuIsOpen = false;
    }

    private void Update()
    {
        if (menuIsOpen)
        {
            if(iconeReduceHeight)
            {
                btnQuit.localScale = new Vector2(btnQuit.localScale.x + VALUESCALE, btnQuit.localScale.y - VALUESCALE);
                btnSound.localScale = new Vector2(btnSound.localScale.x + VALUESCALE, btnSound.localScale.y - VALUESCALE);
                btnMusic.localScale = new Vector2(btnMusic.localScale.x + VALUESCALE, btnMusic.localScale.y - VALUESCALE);
                btnQuitLevel.localScale = new Vector2(btnQuitLevel.localScale.x + VALUESCALE, btnQuitLevel.localScale.y - VALUESCALE);

                if(btnQuit.localScale.x >= 1f)
                {
                    iconeReduceHeight = !iconeReduceHeight;
                }
            }
            else
            {
                btnQuit.localScale = new Vector2(btnQuit.localScale.x - VALUESCALE, btnQuit.localScale.y + VALUESCALE);
                btnSound.localScale = new Vector2(btnSound.localScale.x - VALUESCALE, btnSound.localScale.y + VALUESCALE);
                btnMusic.localScale = new Vector2(btnMusic.localScale.x - VALUESCALE, btnMusic.localScale.y + VALUESCALE);
                btnQuitLevel.localScale = new Vector2(btnQuitLevel.localScale.x - VALUESCALE, btnQuitLevel.localScale.y + VALUESCALE);

                if (btnQuit.localScale.y >= 1f)
                {
                    iconeReduceHeight = !iconeReduceHeight;
                }
            }
        }
    }

    public void Quitter()
    {
        Application.Quit();
    }

    public void Jouer()
    {
        SceneManager.LoadScene("SelectionLevel", LoadSceneMode.Single);
    }
}
