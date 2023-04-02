using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lesson : MonoBehaviour
{
    [HideInInspector] public LessonInfo LessonInfo;
    [HideInInspector] public LessonsView LessonsView;
    [SerializeField] private Text lessonNameText;
    [SerializeField] private Slider lessonRepeatsBar;

    public void InitializeLesson(LessonInfo lessonInfo, LessonsView lessonsView)
    {
        LessonInfo = lessonInfo;
        LessonsView = lessonsView;
        Debug.Log(PlayerPrefs.GetInt(LessonInfo.LessonName, 0));
        lessonRepeatsBar.maxValue = lessonInfo.NumberOfRepeats;
        lessonRepeatsBar.value = PlayerPrefs.GetInt(LessonInfo.LessonName, 0);
        lessonNameText.text = LessonInfo.LessonName;
    }

    public void Success()
    {
        Variables.Data.LessonsDone++;
        Variables.Data.Cash += 50;
        Variables.SaveData();
        if(lessonRepeatsBar.value != lessonRepeatsBar.maxValue)
        {
            Debug.Log("more");
            lessonRepeatsBar.value++;
            Debug.Log(lessonRepeatsBar.value);
            PlayerPrefs.SetInt(LessonInfo.LessonName, (int)lessonRepeatsBar.value);
            Debug.Log(PlayerPrefs.GetInt(LessonInfo.LessonName, 0));
        }
    }

    public void CloseLesson()
    {
        LessonsView.LessonsActivity.Activate();
    }

    public void StartLesson()
    {
        Debug.Log("start lesson");
        if(Variables.Data.HP > 0)
        {
            LessonsView.StartLesson(LessonInfo, this);
        }
    }
}