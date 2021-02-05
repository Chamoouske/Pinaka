using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RandomText3 : MonoBehaviour
{
    public Text randomTxt;
    public static int number;

    void Start()
    {
        number = Random.Range(0, 99);

        if(Spawn_Fruit.turnCorrect == 3){
            number = Spawn_Fruit.result;
        }

        randomTxt.text = number.ToString();
        
    }

}
