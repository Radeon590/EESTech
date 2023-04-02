using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLesson", menuName = "Lessons/LessonInfo")]
public class LessonInfo : ScriptableObject
{
    [SerializeField] private string lessonName;
    [SerializeField] private List<QuestionInfo> questionsBank = new List<QuestionInfo>();
    [SerializeField] private int numberOfRepeats;

    public string LessonName { get { return lessonName; } }
    public List<QuestionInfo> QuestionsBank { get { return questionsBank; } }
    public int NumberOfRepeats { get { return numberOfRepeats; } }
}
