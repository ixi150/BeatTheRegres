using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class AudioEqualizer : MonoBehaviour
{
    [SerializeField] int[] bandConfig;
    [SerializeField] float initMaxBuffer = 10;
    [SerializeField] float dropBufferSpeed = 1;
    [SerializeField] float dropBandSpeed = 5;
    [SerializeField] float lowestMaxTreshold = .01f;

    public static event Action MusicFinished = () => { };

    public const int sampleCount = 512;

    public float[] samples = new float[sampleCount];
    public float[] bands = new float[0];
    public float[] bufferedBands = new float[0];

    float[] bufferMaxes = new float[sampleCount];

    AudioSource audioSource;

    [HideInInspector]
    public UnityEvent[] BandPeeked = new UnityEvent[sampleCount];

    void Awake()
    {
        MusicFinished = null;
        audioSource = GetComponent<AudioSource>();
        for (int i = 0; i < bufferMaxes.Length; i++)
        {
            bufferMaxes[i] = initMaxBuffer;
            BandPeeked[i] = new UnityEvent();
        }

    }

    private void Start()
    {
        Score.ScoreLowerThanZero += OnScoreLowerThanZero;
    }

    void OnScoreLowerThanZero()
    {
        MusicFinished = null;
        StartCoroutine(Ending());
    }

    IEnumerator Ending()
    {
        while (audioSource.pitch > 0)
        {
            audioSource.pitch -= Time.deltaTime / 4;
            yield return null;
        }
        audioSource.pitch = 0;
    }

    void Update()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);

        Array.Resize(ref bands, bandConfig.Length);
        Array.Resize(ref bufferedBands, bandConfig.Length);

        int sampleCount = 0;
        for (int i = 0; i < bandConfig.Length; i++)
        {
            var bandSum = 0f;
            for (int j = 0; j < bandConfig[i]; j++)
            {
                bandSum += samples[sampleCount++];
            }

            bands[i] = bandSum;// / bandConfig[i];

            bufferMaxes[i] = Mathf.Max(lowestMaxTreshold, bufferMaxes[i] - dropBufferSpeed * Time.deltaTime * Mathf.Abs(audioSource.pitch));
            bufferedBands[i] = Mathf.Max(0, bufferedBands[i] - dropBandSpeed * Time.deltaTime);

            if (bandSum > bufferMaxes[i])
            {
                bufferMaxes[i] = bandSum;
                bufferedBands[i] = 1;
                BandPeeked[i].Invoke();
            }

        }

    }

    private void LateUpdate()
    {
        if (audioSource.isPlaying)
        {
            return;
        }

        enabled = false;
        MusicFinished();
    }
}
