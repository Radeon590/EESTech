using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lesson : MonoBehaviour
{
    [SerializeField] private LessonInfo lesson;
    [SerializeField] private Activity lessonActivity;
    [SerializeField] private QuestionsGenerator questionsGenerator;
    [SerializeField] private Slider lessonRepeatsBar;

    private void Awake()
    {
        lessonRepeatsBar.value = PlayerPrefs.GetInt(lesson.LessonName, 0);
    }

    public void Success()
    {
        Variables.Data.LessonsDone++;
        Variables.Data.Cash += 50;
        if(lessonRepeatsBar.value != lessonRepeatsBar.maxValue)
        {
            lessonRepeatsBar.value++;
            PlayerPrefs.SetInt(lesson.LessonName, (int)lessonRepeatsBar.value);
        }
    }

    public void StartLesson()
    {
        if(Variables.Data.HP > 0)
        {
            questionsGenerator.StartGenerating(lesson, this);
            lessonActivity.Activate();
        }
    }
}