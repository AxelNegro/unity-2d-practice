using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answersButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly;

    [Header("Buttons")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
    }

    private void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if(!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
        }
    }

    private void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if(questions.Contains(currentQuestion))
            questions.Remove(currentQuestion);
    }

    private void DisplayQuestion()
    {
        questionText.text = currentQuestion.getQuestion();

        for (int i = 0; i < answersButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answersButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.getAnswer(i);
        }
    }

    private void SetDefaultButtonSprites()
    {
        for (int i = 0; i < answersButtons.Length; i++)
        {
            SetSpriteOfButton(i, defaultAnswerSprite);
        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
    }

    private void DisplayAnswer(int index)
    {
        correctAnswerIndex = currentQuestion.getCorrectAnswerIndex();
        if (currentQuestion.getCorrectAnswerIndex() == index)
            questionText.text = "Correct!";
        else
            questionText.text = "Incorrect! The answer was\n" + answersButtons[correctAnswerIndex].GetComponentInChildren<TextMeshProUGUI>().text;
        SetSpriteOfButton(correctAnswerIndex, correctAnswerSprite);
    }

    private void SetSpriteOfButton(int index, Sprite sprite)
    {
        Image buttonImage = answersButtons[index].GetComponent<Image>();
        buttonImage.sprite = sprite;
    }

    void SetButtonState(bool state)
    {
        for(int i = 0; i < answersButtons.Length; i++)
        {
            Button button = answersButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }
}
