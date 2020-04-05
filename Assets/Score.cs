using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Score : MonoBehaviour
{
    [SerializeField] int plusValue = 1, minusValue = -2;
    [SerializeField] Color positiveColor, negativeColor;
    [SerializeField] int initScore = 60;

    Text text;
    int score = 0;

    public static event Action ScoreLowerThanZero =() => { };

    private void Awake()
    {
        text = GetComponent<Text>();
        ScoreLowerThanZero = null;
    }

    void Start()
    {
        AudioEqualizer.MusicFinished += OnMusicStop;
        Player.Plused += OnPlus;
        Player.Minused += OnMinus;
        score = initScore;
        StartCoroutine(CountingDown());
    }


    void OnMusicStop()
    {
        StopAllCoroutines();
    }


    IEnumerator CountingDown()
    {
        var yieldInstruction = new WaitForSeconds(1);
        while (true)
        {
            yield return yieldInstruction;
            score--;
        }
    }

    private void LateUpdate()
    {
        text.text = score >= 0 ? score.ToString() : "R";
        text.color = score >= 0 ? positiveColor : negativeColor;

        if (score < 0)
        {
            enabled = false;
            ScoreLowerThanZero();
        }
    }


    void OnMinus()
    {
        score += minusValue;
    }

    void OnPlus()
    {
        score += plusValue;
    }
}
