using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 30f;
    [SerializeField] float timeToShowCorrectAnswer = 10f;

    public bool loadNextQuestion;
    public bool isAnsweringQuestion;
    public float fillFraction;

    float timerValue;
    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        if(timerValue > 0)
        {
            if (isAnsweringQuestion)
                fillFraction = timerValue / timeToCompleteQuestion;
            else
                fillFraction = timerValue / timeToShowCorrectAnswer;
        }
        else
        {
            if (isAnsweringQuestion)
            {
                timerValue = timeToShowCorrectAnswer;
                isAnsweringQuestion = false;
            }
            else
            {
                loadNextQuestion = true;
                isAnsweringQuestion = true;
                timerValue = timeToCompleteQuestion;
            }

        }
    }
}
