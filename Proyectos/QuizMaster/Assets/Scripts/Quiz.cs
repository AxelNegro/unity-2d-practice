using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;
    [SerializeField] GameObject[] answersButtons;
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    int correctAnswerIndex;

    void Start()
    {
        GetNextQuestion();
    }

    void GetNextQuestion()
    {
        SetButtonState(true);
        SetDefaultButtonSprites();
        DisplayQuestion();
    }

    private void DisplayQuestion()
    {
        questionText.text = question.getQuestion();

        for (int i = 0; i < answersButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answersButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.getAnswer(i);
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
        correctAnswerIndex = question.getCorrectAnswerIndex();
        if (question.getCorrectAnswerIndex() == index)
            questionText.text = "Correct!";
        else
            questionText.text = "Incorrect! The answer was\n" + answersButtons[correctAnswerIndex].GetComponentInChildren<TextMeshProUGUI>().text;
        SetSpriteOfButton(correctAnswerIndex, correctAnswerSprite);
        SetButtonState(false);
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
