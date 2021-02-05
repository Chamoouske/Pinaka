using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit3 : MonoBehaviour
{
    public static float speed=1F;
    public GameController controller;
    public static int destroyTime3=0;
    
    void Start(){
        controller = FindObjectOfType<GameController>();
    }

    void Update()
    {
         transform.position += Vector3.left * speed * Time.deltaTime; // Vector3.left faz o carro ir pra esquerda; Time.deltaTime deixa mais suave
        if((Fruit.destroyTime==1)||(Fruit2.destroyTime2==1)){
            Destroy(gameObject, 0.1f);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player"){  
            destroyTime3 = 1;
            Destroy(gameObject, 0.1f);
            if(RandomText3.number == Spawn_Fruit.result){ 
                controller.Score++;
                controller.scoreText.text = controller.Score.ToString();
                controller.testCorrect = 1;
            }else{
                controller.testIncorrect = 1;
            }
        }
    }
}

// VC TA PUXANDO A VARIAVEL DO SPAWN FRUIT E LANÇOU UM DEBUG.LOG PRA VER SE DETECTA A MAÇÃ COM A RESPOSTA CERTA