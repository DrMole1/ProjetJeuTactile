using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatLollipop : MonoBehaviour
{
    // ============== VARIABLES ==============

    public SpriteRenderer tongue;
    public SpriteRenderer head;
    public Sprite headWithoutLollipop;
    public Sprite headWithLollipop;
    public GameObject[] lollipopsPrefab;
    public Transform tr;
    public float power = 1.0f;

    private bool hasEaten = false;
    private Vector3 fingerPos;
    private Vector3 realWorldPos;
    private int color;
    private float x = 0;
    private Rigidbody2D rb;
    private Vector3 dir;

    // =======================================



    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 6 && hasEaten == false)
        {
            hasEaten = true;

            switch (other.gameObject.tag)
            {
                case "Red":
                    color = 0;
                    break;
                case "Green":
                    color = 1;
                    break;
                case "Blue":
                    color = 2;
                    break;
                case "Pink":
                    color = 3;
                    break;
                case "Orange":
                    color = 4;
                    break;
                default:
                    print("Incorrect tag. Error.");
                    break;
            }

            Destroy(other.gameObject);

            tongue.enabled = false;
            head.sprite = headWithLollipop;
        }
    }

    private void Update()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            fingerPos = Input.GetTouch(0).position;
            fingerPos.z = 8;
            realWorldPos = Camera.main.ScreenToWorldPoint(fingerPos);

            if(realWorldPos.y > -2f)
            {
                Spit();
            }
        }
    }


    public void Spit()
    {
        if(hasEaten == false)
        {
            return;
        }

        tongue.enabled = true;
        head.sprite = headWithoutLollipop;

        ThrowLollipop();
    }


    public void ThrowLollipop()
    {
        x = tr.position.x;
        Vector3 pos = new Vector3(x, -4.2f, 0f);
        print(pos);
        GameObject spittedlollipop;
        spittedlollipop = Instantiate(lollipopsPrefab[color], pos, Quaternion.identity);
        rb = spittedlollipop.GetComponent<Rigidbody2D>();

        rb.gravityScale = 0f;
        dir = (realWorldPos - tr.position).normalized;
        rb.AddForce(dir * power);

        StartCoroutine(DelayToStartEat());
    }


    IEnumerator DelayToStartEat()
    {
        yield return new WaitForSeconds(1.5f);

        hasEaten = false;
    }
}
