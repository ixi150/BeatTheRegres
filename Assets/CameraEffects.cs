using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    [SerializeField] Color plusColor=Color.green;
    [SerializeField] Color minusColor=Color.red;
    [SerializeField] float colorSpeed = 1;

    Color initialColor;
    new Camera camera;
    private void Awake()
    {
        camera = GetComponent<Camera>();
        initialColor = camera.backgroundColor;
    }

    void Start()
    {
        Player.Plused += OnPlus;
        Player.Minused += OnMinus;
    }

    private void LateUpdate()
    {
        camera.backgroundColor = Vector4.MoveTowards(camera.backgroundColor, initialColor, Time.deltaTime * colorSpeed);
    }

    void OnMinus()
    {
        camera.backgroundColor = minusColor;
    }

    void OnPlus()
    {
        if (camera.backgroundColor == initialColor)
        {
            camera.backgroundColor = plusColor;
        }
    }
}
