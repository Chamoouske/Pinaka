using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MySql.Data.MySqlClient;

public class GameController : MonoBehaviour
{
    public int Score;
    public Text scoreText;
    public GameObject GamePaused;
    public GameObject Gameplay;
    public GameObject Menu;
    public GameObject Level;
    public GameObject Confirm;
    public GameObject Close;
    public GameObject Fox;
    public GameObject Login;
    public GameObject Quit;
    public GameObject Dificulty;
    public GameObject TelaScore;
    private int Mode;
    private int dialogNum=0;
    public Text txtDialog;
    public Text txtTabu;
    public Text txtQuestion; // alterar o txt da tabuada
    public static int testSucess; // variável auxiliar que testa se o usuário pegou a maçã correta
    public GameObject DialogFox;
    // -------- TELA DE RANKING
    public GameObject TelaRank;
    public GameObject OpenRank;
    public GameObject CloseRank;
    public Text User;
    public Text Rank;
    //-------------------------------
    //---------TELA DE USUARIO-------
    public GameObject TelaUser;
    public GameObject MudarNome;
    public GameObject MudarSenha;
    public GameObject ApagarUser;
    public GameObject FecharUser;
    public GameObject TelaClose;
    public InputField userPerfil;
    public Text txtUserPerfil;
    public InputField passwordPerfil;
    public Text txtPasswordPerfil;
    public Text msgErroPerfil;
    public Text txtScore;
    public GameObject btnPerfil;
    //-------------------------------

    public GameObject btnSoundOn;
    public GameObject btnSoundOff;
    

    public AudioSource SoundPoppyCorn;
    private int testaBotaoPause=0;

    public AudioSource SoundButton;

    public AudioSource btnFail;

    public Text txtErroSelect;
    public static bool sinal;

    public AudioSource Correct;
    public AudioSource Incorrect;

    public int testCorrect;
    public int testIncorrect;

    public AudioSource soundVictory;
    public int finalGame;

    //------------ LOGIN  -----------
    public Text msgErro;

    public static string user;
    public Text txtUser;

    public static string password;
    public Text txtPassword;

    //----------------------------
    //Classe de Acesso ao banco de dados
    static MySqlConnection conn = new MySqlConnection("Server = localhost; Database=pinaka;Uid=root;Pwd=;");
    // Start is called before the first frame update
    void Start()
    { 
        Time.timeScale = 0; // pausa a execução
        conn.Open(); //Abertura do banco de dados
    }

    // Update is called once per frame
    void Update()
    {
        if(testCorrect == 1){
            Correct.Play();
            testCorrect = 0;
        }
        if(testIncorrect == 1){
            Incorrect.Play();
            testIncorrect = 0;
        }

        if(finalGame == 1){
            soundVictory.Play();
            finalGame = 0;
        }
    }

    //  pausa o jogo
    public void pauseGame(){ // exibe tela de pause
        SoundButton.Play(); 
        Time.timeScale = 0; // pausa a execução
        
        Gameplay.SetActive(false);
        Confirm.SetActive(false);
        GamePaused.SetActive(true);
    }

    
    public void continueGame(){ // despausa o jogo através do menu
        SoundButton.Play();
        Gameplay.SetActive(true);
        GamePaused.SetActive(false);
        Time.timeScale = 1;
    }

    public void mainMenu(){ // exibe menu principal
        // zerando as variáveis da gameplay -----------
        Spawn_Fruit.n1 = 0;
        txtQuestion.text = "";
        Spawn_Fruit.blockUp = 0;
        Spawn_Fruit.timer = 0;
        Spawn_Fruit.n2 = 0;
        Spawn_Fruit.maxTabuada = 0;
        Score = 0;
        scoreText.text = Score.ToString();
        sinal = false;
        Time.timeScale = 0;
        SoundPoppyCorn.Stop();
        // --------------------------------------

        SoundButton.Play();
        Close.SetActive(false);
        Fox.SetActive(false);
        Login.SetActive(false);
        Gameplay.SetActive(false);
        GamePaused.SetActive(false);
        Confirm.SetActive(false);
        TelaScore.SetActive(false);
        Dificulty.SetActive(false);
        Level.SetActive(false);
        TelaUser.SetActive(false);
        Menu.SetActive(true);
    }

    public void returnLogin(){ // volta ao login
        Menu.SetActive(false);
        Close.SetActive(false);
        Login.SetActive(true);
        msgErro.text = "";
    }
    
    public void quitApp(){
        Login.SetActive(false);
        Quit.SetActive(true);
    }

    public void selectLevel(){ // exibe a seleção da tabuada
        SoundButton.Play();
        Menu.SetActive(false);
        Level.SetActive(true);
    }

    public void selectDificulty(){
        txtErroSelect.text = "Escolha a tabuada desejada";
        if(Spawn_Fruit.tryGameplay == true){
            SoundButton.Play();
            txtTabu.text = "Tabuada do número " + Spawn_Fruit.n1;
            Level.SetActive(false);
            Dificulty.SetActive(true);
        }
        else{
            btnFail.Play();
            txtErroSelect.text = "Não é possível jogar sem escolher uma tabuada!"; //  caso o jogador n escolha uma tabuada
        }
    }

    public void selectTabuada(int num){ // verifica se a tabuada foi escolhida e modifica a tabuada conforme a escolha
        SoundButton.Play();
        Spawn_Fruit.n1 = num;
        sinal = true;
    }

    public void startGame(int Mode){ // inicia o jogo
            SoundButton.Play();
            Menu.SetActive(false);
            Dificulty.SetActive(false);
            Gameplay.SetActive(true);
            Fox.SetActive(true);
            Level.SetActive(false);
            if(Mode==2 || Mode==3){
                Time.timeScale = 1;
            }
            SoundPoppyCorn.Play();

            if(Mode==1){
                DialogFox.SetActive(true);
            }
            if(Mode==3){
                Fruit.speed = 2F;
                Fruit2.speed = 2F;
                Fruit3.speed = 2F; 
                TextMove.speed = 182F; 
            }
    }

    public void dialogText(){
        SoundButton.Play();
        if(dialogNum<3){
            if(dialogNum==0){
                txtDialog.text = "Eu tô com fome, que tal me ajudar a comer algumas maçãs? Mova a raposa para cima e para baixo com o seu dedo.";
            }
            if(dialogNum==1){
                txtDialog.text = "Pegue as maçãs que possuem a resposta para a tabuada que vai aparecer em cima para fazer pontos!";
            }
            if(dialogNum==2){
                txtDialog.text = "Eu sei que você consegue, coleguinha. Vamos lá!";
            }
            dialogNum = dialogNum+1;
        }else{
            DialogFox.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void ConfirmToMenu(){ // exibe tela de confirmação para o menu
        SoundButton.Play();
        GamePaused.SetActive(false);
        Confirm.SetActive(true);
    }

    public void CloseMenu(){ // exibe tela de confirmação pra sair do jogo 
        SoundButton.Play();
        Menu.SetActive(false);
        Login.SetActive(false);
        Close.SetActive(true);
    }

    public void CloseAccount(){ // exibe tela de confirmação para excluir conta
        SoundButton.Play();
        Menu.SetActive(false);
        Login.SetActive(false);
        TelaUser.SetActive(false);
        TelaClose.SetActive(true);
    }

    public void quitGame(){ // sai do jogo
        SoundButton.Play();
        Application.Quit();
    }

    public void SoundOnOff(){ //  liga e desliga o som do jogo
        if(testaBotaoPause%2==0){
            SoundButton.Play();
            SoundPoppyCorn.Pause();
            btnSoundOn.SetActive(false);
            btnSoundOff.SetActive(true);
        }else{
            SoundButton.Play();
            SoundPoppyCorn.Play();
            btnSoundOff.SetActive(false);
            btnSoundOn.SetActive(true); 
        }
        testaBotaoPause = testaBotaoPause+1;
    }


    //Verificação se usuario existe para fazer Login
    public void validacao(){
        //Nao mexa, GAMBIARRA PESADA
        int veri = 2;
        string userbd;
        string passwordbd;
        MySqlCommand query = new MySqlCommand("SELECT * FROM usuarios", conn);
        using (MySqlDataReader reader = query.ExecuteReader())
        {
            while (reader.Read())
            {
                userbd = reader["Nome"].ToString();
                passwordbd = reader["Senha"].ToString();
                if(userbd == user && passwordbd == password){
                    mainMenu();
                    veri = 1;
                }
            }

            if(veri != 1){
                msgErro.text = "Login ou senha incorretos!";    
            }
        }   
    }

    //Verificação se usuario existe para poder ser registrado
    public void validacaoRegistro(){
        //Nao mexa, GAMBIARRA PESADA
        int veri = 2;
        string userbd;
        
        //Comando SQL pra validação
        MySqlCommand query = new MySqlCommand("SELECT * FROM usuarios", conn);
        using (MySqlDataReader reader = query.ExecuteReader())
        {
            while (reader.Read())
            {
                userbd = reader["Nome"].ToString();
                if(userbd == user){
                    msgErro.text = "Usuario já cadastrado";   
                    veri = 1;
                }
            }
        } 

        if(veri != 1){
            MySqlCommand command = new MySqlCommand("INSERT INTO usuarios (Nome, Senha, Score) VALUES ('"+user+"','"+password+"', 0);", conn);
            command.ExecuteNonQuery();
            mainMenu();
        }
    }

    /* ------------------------- LOGIN ------------------- */
    public void LoginPinaka(){
        SoundButton.Play();
        user = txtUser.text.ToString();
        password = txtPassword.text.ToString();
        if(user == "" || password == ""){
            msgErro.text = "Preencha todos os campos"; 
        }else{
            validacao();
        }
        
        // msgErro.text = "Login ou senha incorretos!";  -------- linha que altera o texto caso a senha/login estejam errados
    }

    /* ------------------------- Registro ----------------- */
    public void RegisterPinaka(){
        SoundButton.Play();
        user = txtUser.text.ToString();
        password = txtPassword.text.ToString();
        if(user == "" || password == ""){
            msgErro.text = "Preencha todos os campos"; 
        }else{
            validacaoRegistro();
        }
    }

    public void AbrirRank(){
        SoundButton.Play();
        Menu.SetActive(false);
        TelaRank.SetActive(true);

        Ranking();
    }

    public void FecharRank(){
        SoundButton.Play();
        TelaRank.SetActive(false);
        Menu.SetActive(true);
    }

    public void Ranking(){
        Rank.text = "Pontos\n";
        User.text = "Jogador\n";
        
        MySqlCommand command = new MySqlCommand("SELECT * FROM usuarios ORDER BY Score DESC LIMIT 10", conn);
        
        using (MySqlDataReader results = command.ExecuteReader())
        {
            while (results.Read())
            {
                Rank.text += "\n" + results["Score"].ToString();
                User.text += "\n" + results["Nome"].ToString();
            }
        };
    }

    public static void UpdateRank(int ptn){
        MySqlCommand command = new MySqlCommand("UPDATE usuarios SET Score = Score + " + ptn +" WHERE Nome LIKE '" + user + "'", conn);
        command.ExecuteNonQuery();
    }

    public void mostrarPerfil(){
        SoundButton.Play();
        Menu.SetActive(false);
        TelaUser.SetActive(true);
        TelaClose.SetActive(false);
        msgErroPerfil.text = "";
        MySqlCommand command = new MySqlCommand("SELECT * FROM usuarios WHERE Nome = '"+user+"'", conn);
        using (MySqlDataReader results = command.ExecuteReader())
        {
            while (results.Read())
            {
                userPerfil.text = results["Nome"].ToString();
                passwordPerfil.text = results["Senha"].ToString();
                txtScore.text = results["Score"].ToString();
            }
        };
    }

    public void AlterarNome(){
        SoundButton.Play();
        if(user == txtUserPerfil.text.ToString()){
            msgErroPerfil.text = "O Nome inserido é o mesmo registrado";
        }else{
            MySqlCommand command = new MySqlCommand("UPDATE usuarios SET Nome = '" + txtUserPerfil.text.ToString() + "' WHERE Nome LIKE '" + user + "'", conn);
            command.ExecuteNonQuery();
            user = txtUserPerfil.text.ToString();
            msgErroPerfil.text = "Nome alterado com sucesso";
        }
    }

    public void AlterarSenha(){
        SoundButton.Play();
        MySqlCommand command = new MySqlCommand("UPDATE usuarios SET Senha = '" + txtPasswordPerfil.text.ToString() + "' WHERE Nome LIKE '" + user + "'", conn);
        command.ExecuteNonQuery();
        msgErroPerfil.text = "Senha alterado com sucesso";
    }

    public void DeleteUser(){
        SoundButton.Play();
        TelaClose.SetActive(false);
        MySqlCommand command = new MySqlCommand("DELETE FROM usuarios WHERE Nome LIKE '" + user + "'", conn);
        command.ExecuteNonQuery();

        msgErro.text = "Conta apagada com sucesso";
        TelaUser.SetActive(false);
        returnLogin();
    }
}
