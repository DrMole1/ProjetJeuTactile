using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AugmentedLollipop : MonoBehaviour
{
    private const float delay = 1.82f;

    // ============== VARIABLES ==============

    public GameObject zoneLaserPrefab;

    // =======================================



    public void StartToExplode()
    {
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(delay);

        GameObject zoneLaser;
        zoneLaser = Instantiate(zoneLaserPrefab, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(0.01f);

        Destroy(gameObject);
    }
}
