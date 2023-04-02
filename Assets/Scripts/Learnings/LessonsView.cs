using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LessonsView : MonoBehaviour
{
    [SerializeField] private List<LessonInfo> Lessons;
    [SerializeField] private GameObject lessonObjPref;
    [SerializeField] private Transform lessonsContainer;
    [SerializeField] private Activity questionsActivity;
    [SerializeField] private QuestionsGenerator questionsGenerator;
    //
    public Activity LessonsActivity;

    public void InitializeLessons()
    {
        /*while(lessonsContainer.childCount > 0)
        {
            Destroy(lessonsContainer.GetChild(0).gameObject);
        }*/
        var children = new List<GameObject>();
        foreach (Transform child in lessonsContainer) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
        foreach (var lesson in Lessons)
        {
            Lesson newLesson = Instantiate(lessonObjPref, lessonsContainer).gameObject.GetComponent<Lesson>();
            newLesson.InitializeLesson(lesson, this);
        }
    }

    public void StartLesson(LessonInfo lessonInfo, Lesson lesson)
    {
        //Debug.Log("lesson view");
        questionsGenerator.StartGenerating(lessonInfo, lesson);
        questionsActivity.Activate();
    }
}
