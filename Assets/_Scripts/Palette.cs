using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palette : MonoBehaviour
{
    private const int MAXDELAY = 50;

    // ============== VARIABLES ==============

    public int maxSteps = 0;
    public int actualStep = 0;

    [System.Serializable]
    public struct CandyPalette
    {
        public int step;
        public int place;
        public GameObject candy;
    }

    public CandyPalette[] candyPalette = new CandyPalette[255];

    public float speed = 0.7f;

    private int delay = 0;
    private bool stop = false;

    // =======================================


    private void Awake()
    {
        int maxPlaces = 7;
        int nCandy = 0;

        for(int i = 0; i < maxSteps; i++)
        {
            if(i != 0)
            {
                maxPlaces = CalculateMaxPlaces(i);
            }

            for(int j = 0; j < maxPlaces; j++)
            {
                candyPalette[nCandy].step = i;
                candyPalette[nCandy].place = j;
                candyPalette[nCandy].candy = transform.GetChild(nCandy).gameObject;
                nCandy++;
            }
        }

        for(int k = 0; k < transform.childCount; k++)
        {
            transform.GetChild(k).gameObject.SetActive(false);
            transform.GetChild(k).localScale = new Vector3(0.07f, 0.07f, 1f);
        }
    }

    private void Start()
    {
        StartCoroutine(ShowAllCandies());
    }

    public int CalculateMaxPlaces(int _step)
    {
        if (_step % 2 == 0)
        {
            return 7;
        }
        else
        {
            return 6;
        }
    }

    public void Check()
    {
        int maxPlaces = 7;
        int startPlace = 0;

        if (actualStep != 0)
        {
            maxPlaces = CalculateMaxPlaces(actualStep);
            startPlace = (int)Mathf.Ceil(actualStep / 2) * 7 + (int)Mathf.Floor(actualStep / 2) * 6;
        }

        bool stepIsEmpty = true;

        for(int i = startPlace; i < startPlace + maxPlaces; i++)
        {
            if(candyPalette[i].candy != null)
            {
                stepIsEmpty = false;
            }
        }

        if(stepIsEmpty && stop == false)
        {
            StartCoroutine(GoNextStep());
            print(actualStep);
            stop = true;
        }
    }

    IEnumerator GoNextStep()
    {
        delay = 0;

        while (delay < MAXDELAY)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y - 0.75f), speed * Time.deltaTime);
            delay++;

            yield return null;
        }

        actualStep++;
        stop = false;

        //Check();
    }

    IEnumerator ShowAllCandies()
    {
        for (int k = 0; k < transform.childCount; k++)
        {
            transform.GetChild(k).gameObject.SetActive(true);
            StartCoroutine(GrowCandy(transform.GetChild(k)));

            yield return new WaitForSeconds(0.02f);
        }
    }

    IEnumerator GrowCandy(Transform tr)
    {
        while(tr.localScale.x < 0.1f)
        {
            tr.localScale = new Vector3(tr.localScale.x + 0.0045f, tr.localScale.y + 0.0045f, 1f);

            yield return new WaitForSeconds(0.1f);
        }

        while (tr.localScale.x > 0.092f)
        {
            tr.localScale = new Vector3(tr.localScale.x - 0.0045f, tr.localScale.y - 0.0045f, 1f);

            yield return new WaitForSeconds(0.1f);
        }
    }
}
