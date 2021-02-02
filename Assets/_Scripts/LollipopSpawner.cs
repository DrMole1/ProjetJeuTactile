using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LollipopSpawner : MonoBehaviour
{
    private const float XMIN = -1.7f;
    private const float XMAX = 1.7f;
    private const float DELAY = 0.5f;
    private const float DELAYMIN = 2.8f;
    private const float DELAYMAX = 4.8f;

    // ============== VARIABLES ==============

    public GameObject[] lollipops;
    public float delayToSpawn = 5f;
    public int nColor = 3;

    // =======================================



    private void Start()
    {
        StartCoroutine(Spawn());
    }


    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(delayToSpawn);

        delayToSpawn = UnityEngine.Random.Range(DELAYMIN, DELAYMAX);

        float x = UnityEngine.Random.Range(XMIN, XMAX);

        transform.position = new Vector3(x, transform.position.y, transform.position.z);

        int alea = UnityEngine.Random.Range(0, nColor);

        GameObject lollipop;
        lollipop = Instantiate(lollipops[alea], transform.position, Quaternion.identity);

        StartCoroutine(Spawn());
    }
}
