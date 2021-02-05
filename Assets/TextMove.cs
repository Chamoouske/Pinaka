using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMove : MonoBehaviour
{
    public static float speed=90F;
    public GameController controller;
    public static int destroyTime=0;

    void Start(){
        controller = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
         transform.position += Vector3.left * speed * Time.deltaTime; // Vector3.left faz o carro ir pra esquerda; Time.deltaTime deixa mais suave
    }

}
