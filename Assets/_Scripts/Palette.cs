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
        float initPosY = transform.position.y;
        float nextPosY = initPosY - 0.75f;

        while (transform.position.y > nextPosY)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);

            yield return new WaitForSeconds(0.01f);
        }

        actualStep++;
        //stop = false;

        yield return new WaitForSeconds(1f);

        stop = false;

        Check();
    }

    IEnumerator ShowAllCandies()
    {
        for (int k = 0; k < transform.childCount; k++)
        {
            transform.GetChild(k).gameObject.SetActive(true);

            if(transform.GetChild(k).gameObject.layer == 7 || transform.GetChild(k).gameObject.layer == 14)
                StartCoroutine(GrowCandy(transform.GetChild(k)));
            if (transform.GetChild(k).gameObject.layer == 13)
                StartCoroutine(GrowCandyExplosive(transform.GetChild(k)));

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

    IEnumerator GrowCandyExplosive(Transform tr)
    {
        while (tr.localScale.x < 0.45f)
        {
            tr.localScale = new Vector3(tr.localScale.x + 0.085f, tr.localScale.y + 0.085f, 1f);

            yield return new WaitForSeconds(0.1f);
        }

        while (tr.localScale.x > 0.35f)
        {
            tr.localScale = new Vector3(tr.localScale.x - 0.085f, tr.localScale.y - 0.085f, 1f);

            yield return new WaitForSeconds(0.1f);
        }
    }
}
