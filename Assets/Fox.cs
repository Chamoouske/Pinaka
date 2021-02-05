using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{

    float input_y = 0; // input vertical
    public float speed = 2.5f; // criada publica pra poder alterar o valor na unity
    bool isMove = false; // variável que muda a animação de movimentação/parado

    // Start is called before the first frame update
    void Start()
    {
        isMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        input_y = Input.GetAxisRaw("Vertical");
        isMove = (input_y != 0); // pra saber se o personagem ta se movimentando ou não

        if(isMove){
            var move = new Vector3(0, input_y, 0).normalized;
            transform.position += move * speed * Time.deltaTime; // movimenta o personagem
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Enemy"){   
            if(RandomText.number != Spawn_Fruit.result){
                Destroy(gameObject, 0.1f);
            }    
        }
    }
}
