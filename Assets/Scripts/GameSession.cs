using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int playerScore = 0;
    // [SerializeField] int coinValue = 100;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    void Awake()
    {
        //implementing singleton design pattern
        int numGamesessions = FindObjectsOfType<GameSession>().Length;
        if(numGamesessions > 1){
            Destroy(gameObject);
        }
        else{
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start(){
        livesText.text = playerLives.ToString();
        scoreText.text = (playerScore*100).ToString();
    }

    

    public void ProcessPlayerDeath(){
        if(playerLives > 1){
            TakeLife();
        }
        else{
            
            ResetGameSession();
        }
    }

    void ResetGameSession(){
        FindObjectOfType<ScenePersist>().ResetScene();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    void TakeLife(){
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();
    }

    public void AddScore(int coinValue){
        playerScore+=coinValue;
        scoreText.text = playerScore.ToString();
    }

}
