using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawn_Fruit : MonoBehaviour
{
    public GameObject Coin;
    public GameObject Coin2;
    public GameObject Coin3;
    public float height;
    public float maxTime;
    public static float timer =0f;

    public static int n1=0;
    public static int n2 = 0;
    public static int result;
    public Text Question;
    string count;
    public static int turnCorrect;
    public static int maxTabuada = 0;

    public static bool tryGameplay =true;

    public GameObject Gameplay;
    public GameObject TelaScore;
    public Text textScore;
    public GameController controller;
    public GameObject Fox;

    public Text txtQuest;
    public static int blockUp=0; // pra impedir o update de chamar a função de exibir score várias vezes

    void Start()
    {
        controller = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(n1 == 0){
            tryGameplay = false;
        }

        if(GameController.sinal == true){
            tryGameplay = true;
        }

        if(timer> maxTime){
            if(maxTabuada<10){
                // to mudando o n1 através do gamecontroller.cs
                n2 = n2+1;
                result = n1*n2;
                Fruit.destroyTime = 0;
                Fruit2.destroyTime2 = 0;
                Fruit3.destroyTime3 = 0;

                turnCorrect = Random.Range(1, 4); // decide qual fruta vai ter a resposta correta

                AlterQuest.AlterQuestion(n1, n2, result, Question, count);

                GameObject newCoin = Instantiate(Coin); // cria uma nova instância do objeto Coins
                newCoin.transform.position = transform.position + new Vector3(2, 5, 0);  //  sorteia o valor do y (altura)
                Destroy(newCoin, 15f);
                timer = 0;  

                GameObject newCoin2 = Instantiate(Coin2); // cria uma nova instância do objeto Coins
                newCoin2.transform.position = transform.position + new Vector3(2, 2, 0);  //  sorteia o valor do y (altura)
                Destroy(newCoin2, 15f);
                timer = 0;  


                GameObject newCoin3 = Instantiate(Coin3); // cria uma nova instância do objeto Coins
                newCoin3.transform.position = transform.position + new Vector3(2, -1, 0);  //  sorteia o valor do y (altura)
                Destroy(newCoin3, 15f);
                timer = 0;  

                maxTabuada = maxTabuada+1;
            }else{
                if(blockUp==0){
                    ScoreFox();
                    blockUp = 1;
                }
            }
        }
        timer += Time.deltaTime;
    }

    public void ScoreFox(){
        controller.finalGame = 1;
        textScore.text = "Parabéns por concluir a tabuada do " + n1 + ". Você fez " + controller.Score + " pontos!";
        Gameplay.SetActive(false);
        Fox.SetActive(false);
        Time.timeScale = 0;
        TelaScore.SetActive(true);
    }

    public void reiniciaGame(){
        // reiniciando as variáveis pra reiniciar o jogo
        txtQuest.text = "";
        blockUp = 0;
        timer = 0;
        n2 = 0;
        maxTabuada = 0;
        controller.Score = 0;
        controller.scoreText.text = controller.Score.ToString();
        Time.timeScale = 1;
        Fox.SetActive(true);
        Gameplay.SetActive(true);
        TelaScore.SetActive(false);
    }

}
