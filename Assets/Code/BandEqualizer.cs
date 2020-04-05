using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandEqualizer : MonoBehaviour
{
    [SerializeField]    AudioEqualizer equalizer;
    [SerializeField] float minScale = 1, maxScale = 2;

    void LateUpdate()
    {
        var t = transform;
        for (int i = 0; i < equalizer.bands.Length; i++)
        {
            var child = t.GetChild(i);

            var s = Mathf.LerpUnclamped(minScale, maxScale, equalizer.bufferedBands[i]);
            child.localScale = new Vector3(1, s, 1);
        }
    }
}
