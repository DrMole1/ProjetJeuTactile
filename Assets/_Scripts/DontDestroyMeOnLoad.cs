using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Dont destroy objetcs when this script is attached to
public class DontDestroyMeOnLoad : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
