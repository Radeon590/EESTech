using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionsGenerator : MonoBehaviour
{
    [SerializeField] private GameObject helpButton;
    [SerializeField] private GameObject helpPanel;
    [SerializeField] private Text helpText;
    [SerializeField] private GameObject questionOptionPref;
    [SerializeField] private Transform questionOptionsContainer;
    [SerializeField] private GameObject correctPanel;
    [SerializeField] private GameObject incorrectPanel;

    public GameObject CorrectPanel
    {
        get { return correctPanel; }
    }

    public GameObject IncorrectPanel
    {
        get { return incorrectPanel; }
    }

    private Lesson _lesson;

    [HideInInspector] public LessonInfo CurrentLesson;

    private int currentQuestion = -1;
    public int TrueAnswers = 0;

    public void StartGenerating(LessonInfo currentLesson, Lesson lesson)
    {
        CurrentLesson = currentLesson;
        _lesson = lesson;
        currentQuestion = -1;
        TrueAnswers = 0;
        NextQuestion();
    }

    public void NextButtonEvent()
    {
        NextQuestion();
    }

    public void NextQuestion()
    {
        if(currentQuestion >= CurrentLesson.QuestionsBank.Count - 1)
        {
            GetComponent<Activity>().Deactivate();
            if(TrueAnswers > currentQuestion / 2)
            {
                _lesson.Success();
            }
            _lesson.CloseLesson();
            return;
        }
        //
        /*while(questionOptionsContainer.childCount > 0)
        {
            Destroy(questionOptionsContainer.GetChild(0).gameObject);
        }*/
        var children = new List<GameObject>();
        foreach (Transform child in questionOptionsContainer) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
        CorrectPanel.SetActive(false);
        IncorrectPanel.SetActive(false);
        currentQuestion++;
        //
        QuestionInfo questionInfo = CurrentLesson.QuestionsBank[currentQuestion];
        if (!Variables.Data.QuestionsBank.Contains(questionInfo.Question))
        {
            helpText.text = questionInfo.Faq;
            helpButton.SetActive(true);
        }
        else
        {
            helpButton.SetActive(false);
        }
        foreach(var option in questionInfo.Options) 
        {
            QuestionOption newOption = Instantiate(questionOptionPref, questionOptionsContainer).GetComponent<QuestionOption>();
            newOption.QuestionText.text = option;
            newOption.IsCorrect = (option == questionInfo.Options[questionInfo.CorrectOptionIndex]);
            newOption.QuestionsGenerator = this;
        }
    }

    public void OpenHelp()
    {
        helpPanel.SetActive(true);
    }

    public void CloseHelp()
    {
        helpPanel.SetActive(false);
    }
}
