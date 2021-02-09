using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuSelectionScript : MonoBehaviour
{
    private const float VALUESCALE = 0.002f;


    public GameObject menu;

    public GameObject btnReturn;
    public GameObject btnSound;
    public GameObject btnMusic;
    public GameObject buttons;

    public Sprite successedLevel;
    public Sprite actualLevel;

    public bool menuIsOpen = false;

    public GameObject ptcActualLevelPrefab;
    public Transform map;

    private bool menuIsOnAnim = false;
    private RectTransform btnActualLevel;
    private bool isActualButtonCanAnim = false;
    private bool iconeReduceHeight = true;

    public void Start()
    {
        int page = (int)Mathf.Floor(PlayerPrefs.GetInt("MaxLevel", 0) / 10);
        PlayerPrefs.SetInt("ActualPage", page);

        for (int i = 0; i <= page; i++)
        {
            if(i != page)
            {
                for(int j = 0; j < 10; j++)
                {
                    MakeSuccessedLevel(buttons.transform.GetChild(i).GetChild(j).gameObject);
                }
            }
            else
            {
                int level = PlayerPrefs.GetInt("MaxLevel", 0) - page * 10;

                for (int k = 0; k < level; k++)
                {
                    MakeSuccessedLevel(buttons.transform.GetChild(i).GetChild(k).gameObject);
                }

                MakeActualLevel(buttons.transform.GetChild(i).GetChild(level).gameObject);
            }
        }

        StartCoroutine(ActiveAllButtons());
    }

    public void Return()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void ShowMenu()
    {
        if (menuIsOnAnim)
        {
            return;
        }

        if (menuIsOpen)
        {
            StartCoroutine(UpMenu());
        }
        else
        {
            menuIsOpen = true;

            StartCoroutine(DownMenu());
        }
    }


    IEnumerator DownMenu()
    {
        RectTransform pannel = menu.transform.GetChild(0).GetComponent<RectTransform>();
        menuIsOnAnim = true;

        btnReturn.SetActive(false);
        btnMusic.SetActive(false);
        btnSound.SetActive(false);
        buttons.SetActive(false);

        while (pannel.localPosition.y < -17f)
        {
            yield return new WaitForSeconds(0.01f);

            pannel.localPosition = new Vector3(pannel.localPosition.x, pannel.localPosition.y + 5f, 0);
        }

        while (pannel.localPosition.y > -20f)
        {
            yield return new WaitForSeconds(0.01f);

            pannel.localPosition = new Vector3(pannel.localPosition.x, pannel.localPosition.y - 2f, 0);
        }

        menuIsOnAnim = false;
    }

    IEnumerator UpMenu()
    {
        RectTransform pannel = menu.transform.GetChild(0).GetComponent<RectTransform>();
        menuIsOnAnim = true;

        while (pannel.localPosition.y < -17f)
        {
            yield return new WaitForSeconds(0.01f);

            pannel.localPosition = new Vector3(pannel.localPosition.x, pannel.localPosition.y + 2f, 0);
        }

        while (pannel.localPosition.y > -58.5f)
        {
            yield return new WaitForSeconds(0.01f);

            pannel.localPosition = new Vector3(pannel.localPosition.x, pannel.localPosition.y - 5f, 0);
        }

        menuIsOnAnim = false;

        menuIsOpen = false;

        btnReturn.SetActive(true);
        btnSound.SetActive(true);
        btnMusic.SetActive(true);
        buttons.SetActive(true);
    }

    public void SelectLevel(int _level)
    {
        if(PlayerPrefs.GetInt("MaxLevel", 0) < _level)
        {
            return;
        }

        PlayerPrefs.SetInt("ActualLevel", _level);
        SceneManager.LoadScene("Game0", LoadSceneMode.Single);
    }

    public void MakeSuccessedLevel(GameObject _button)
    {
        _button.GetComponent<Image>().overrideSprite = successedLevel;
        _button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.black;
        _button.GetComponent<RectTransform>().localScale = new Vector2(1f, 0.8f);
    }

    public void MakeActualLevel(GameObject _button)
    {
        _button.GetComponent<Image>().overrideSprite = actualLevel;
        _button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;
        _button.GetComponent<RectTransform>().localScale = new Vector2(0.95f, 1f);

        btnActualLevel = _button.GetComponent<RectTransform>();
    }

    IEnumerator ActiveAllButtons()
    {
        for(int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.2f);

            buttons.transform.GetChild(PlayerPrefs.GetInt("ActualPage", 0)).GetChild(i).gameObject.SetActive(true);
            StartCoroutine(GrowAnimButtons(buttons.transform.GetChild(PlayerPrefs.GetInt("ActualPage", 0)).GetChild(i).gameObject.GetComponent<RectTransform>()));
        }

        yield return new WaitForSeconds(0.6f);

        isActualButtonCanAnim = true;

        GameObject ptcActualLevel;
        ptcActualLevel = Instantiate(ptcActualLevelPrefab, map.GetChild(PlayerPrefs.GetInt("MaxLevel", 0)).position, Quaternion.identity);
    }

    IEnumerator GrowAnimButtons(RectTransform _button)
    {
        float initScaleX;
        initScaleX = _button.localScale.x;
        //float initScaleY;
        //initScaleY = _button.localScale.y;

        while (_button.localScale.x < initScaleX + 0.5f)
        {
            yield return new WaitForSeconds(0.01f);

            _button.localScale = new Vector3(_button.localScale.x + 0.07f, _button.localScale.y + 0.07f, 1);
        }

        while (_button.localScale.x > initScaleX)
        {
            yield return new WaitForSeconds(0.01f);

            _button.localScale = new Vector3(_button.localScale.x - 0.04f, _button.localScale.y - 0.04f, 1);
        }
    }

    private void Update()
    {
        if(isActualButtonCanAnim == false)
        {
            return;
        }

        if (iconeReduceHeight)
        {
            btnActualLevel.localScale = new Vector2(btnActualLevel.localScale.x + VALUESCALE, btnActualLevel.localScale.y - VALUESCALE);

            if (btnActualLevel.localScale.x >= 1f)
            {
                iconeReduceHeight = !iconeReduceHeight;
            }
        }
        else
        {
            btnActualLevel.localScale = new Vector2(btnActualLevel.localScale.x - VALUESCALE, btnActualLevel.localScale.y + VALUESCALE);

            if (btnActualLevel.localScale.y >= 1f)
            {
                iconeReduceHeight = !iconeReduceHeight;
            }
        }
    }
}