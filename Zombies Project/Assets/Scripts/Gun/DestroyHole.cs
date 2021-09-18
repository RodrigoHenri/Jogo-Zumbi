using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyHole : MonoBehaviour
{
    [SerializeField] private float timeToDestroy = 3f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

    
}
