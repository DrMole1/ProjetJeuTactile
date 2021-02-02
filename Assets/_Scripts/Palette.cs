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
            stop = true;
        }
    }

    IEnumerator GoNextStep()
    {
        print("next step");
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
}
