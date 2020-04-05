using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    [SerializeField] float delay = 1;
    void Start()
    {
        Destroy(gameObject, delay);
    }

}
