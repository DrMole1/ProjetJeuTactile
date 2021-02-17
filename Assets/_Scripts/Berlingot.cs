using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berlingot : MonoBehaviour
{
    // ============== VARIABLES ==============

    public GameObject ptcExplosionPrefab;
    public EatLollipop eatLollipop;

    // =======================================



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8 && other.gameObject.tag == gameObject.tag)
        {
            GameObject.Find("Palette").GetComponent<Palette>().Check();

            GameObject ptcExplosion;
            ptcExplosion = Instantiate(ptcExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(ptcExplosion, 4f);

            eatLollipop = GameObject.Find("Gummy").GetComponent<EatLollipop>();
            eatLollipop.TouchBerlingotExplosif(transform.position);

            Destroy(gameObject);
        }

        if (other.gameObject.layer == 12 && other.gameObject.tag == gameObject.tag)
        {
            GameObject.Find("Palette").GetComponent<Palette>().Check();

            GameObject ptcExplosion;
            ptcExplosion = Instantiate(ptcExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(ptcExplosion, 4f);

            eatLollipop = GameObject.Find("Gummy").GetComponent<EatLollipop>();
            eatLollipop.TouchBerlingotExplosif(transform.position);

            Destroy(gameObject);
        }

        if (other.gameObject.layer == 11 && other.gameObject.tag == "Supra")
        {
            GameObject.Find("Palette").GetComponent<Palette>().Check();

            GameObject ptcExplosion;
            ptcExplosion = Instantiate(ptcExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(ptcExplosion, 4f);

            eatLollipop = GameObject.Find("Gummy").GetComponent<EatLollipop>();
            eatLollipop.TouchBerlingotExplosif(transform.position);

            Destroy(gameObject);
        }
    }
}
