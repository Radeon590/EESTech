using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statisctics : MonoBehaviour
{
    [SerializeField] private Text lessonsDoneStatText;
    public void UpdateStatistics()
    {
        lessonsDoneStatText.text = $"уроков пройдено {Variables.Data.LessonsDone}";
    }
}
