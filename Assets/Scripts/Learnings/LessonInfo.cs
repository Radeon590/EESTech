using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLesson", menuName = "Lessons/Lesson")]
public class LessonInfo : ScriptableObject
{
    [SerializeField] private List<QuestionInfo> questionsBank = new List<QuestionInfo>();
    [SerializeField] private int numberOfRepeats;

    public List<QuestionInfo> QuestionsBank { get { return questionsBank; } }
    public int NumberOfRepeats { get { return numberOfRepeats; } }
}
