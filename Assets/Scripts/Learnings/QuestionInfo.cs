using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestion", menuName = "Lessons/Question")]
public class QuestionInfo : ScriptableObject
{
    [SerializeField] private string question;
    [SerializeField] private List<string> options;
    [SerializeField] private int correctOptionIndex;
    [SerializeField] private string faq;

    public string Question { get { return question; } }
    public List<string> Options { get { return options; } }
    public int CorrectOptionIndex { get { return correctOptionIndex; } }
    public string Faq { get { return faq;} }
}

public enum QuestionTypes
{
    test,
    comunication
}
