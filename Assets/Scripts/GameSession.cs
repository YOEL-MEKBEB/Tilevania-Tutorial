using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int playerScore = 0;
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

    

    public void ProcessPlayerDeath(){
        if(playerLives > 1){
            TakeLife();
        }
        else{
            ResetGameSession();
        }
    }

    void ResetGameSession(){
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    void TakeLife(){
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void AddScore(){
        playerScore++;
    }

}
