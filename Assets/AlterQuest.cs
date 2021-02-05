using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlterQuest : MonoBehaviour
{
    private int n1;
    private int n2 = -1;
    private int result;
    public Text Question;
    string count;
    // Start is called before the first frame update

    public static void AlterQuestion(int n1, int n2, int result, Text Question, string count)
    {
        count = n1.ToString() + " x " + n2.ToString() + " = ?";
        Question.text = count;

    }
}


