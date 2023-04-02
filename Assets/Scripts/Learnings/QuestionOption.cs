using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionOption : MonoBehaviour
{
    public Text QuestionText;
    [HideInInspector] public bool IsCorrect;
    [HideInInspector] public QuestionsGenerator QuestionsGenerator;

    public void Chosen()
    {
        if (IsCorrect)
        {
            QuestionsGenerator.TrueAnswers++;
            QuestionsGenerator.CorrectPanel.SetActive(true);
            if (!Variables.Data.QuestionsBank.Contains(QuestionText.text))
            {
                Variables.Data.QuestionsBank.Add(QuestionText.text);
            }
        }
        else
        {
            Variables.Data.HP--;
            QuestionsGenerator.IncorrectPanel.SetActive(true);
        }
    }
}
