using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public static float speed=1F;
    public GameController controller;
    public static int destroyTime=0;

    void Start(){
        controller = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
         transform.position += Vector3.left * speed * Time.deltaTime; // Vector3.left faz a fruta ir pra esquerda; Time.deltaTime deixa mais suave
        if((Fruit2.destroyTime2==1)||(Fruit3.destroyTime3==1)){
            Destroy(gameObject, 0.1f);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player"){  
            destroyTime = 1;
            Destroy(gameObject, 0.1f);
            if(RandomText.number == Spawn_Fruit.result){ 
                controller.Score++;
                controller.scoreText.text = controller.Score.ToString();
                controller.testCorrect = 1;
            }else{
                controller.testIncorrect = 1;
            }
        }
    }
}
