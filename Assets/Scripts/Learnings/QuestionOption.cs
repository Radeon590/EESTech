using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionOption : MonoBehaviour
{
    public Text QuestionText;
    public bool IsCorrect;
    public QuestionsGenerator QuestionsGenerator;

    public void Chosen()
    {
        if (IsCorrect)
        {
            QuestionsGenerator.CorrectPanel.SetActive(true);
            Variables.Data.QuestionsBank.Add(QuestionText.text);
        }
        else
        {
            Variables.Data.HP--;
            QuestionsGenerator.IncorrectPanel.SetActive(true);
        }
    }
}
