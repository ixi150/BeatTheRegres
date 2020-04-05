using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullSpectrumRing : MonoBehaviour
{
    [SerializeField] AudioEqualizer equalizer;
    [SerializeField] GameObject blockPrefab;
    [SerializeField] float minScale = 1, maxScale = 2, radius = 100;

    private void Awake()
    {
        for (int i = 0; i < AudioEqualizer.sampleCount; i++)
        {
            Instantiate(blockPrefab, transform);
        }
    }

    private void LateUpdate()
    {
        var t = transform;
        for (int i = 0; i < t.childCount; i++)
        {
            var child = t.GetChild(i);
            child.eulerAngles = new Vector3(0, i * 360f / AudioEqualizer.sampleCount, 0);
            child.localPosition = child.forward * radius;

            var s = Mathf.LerpUnclamped(minScale, maxScale, equalizer.samples[i]);
            child.localScale = new Vector3(1, s, 1);
        }
    }
}
