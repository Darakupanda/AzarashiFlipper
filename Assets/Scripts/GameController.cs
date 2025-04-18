using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Runtime.InteropServices;
using Unity.VisualScripting;

public class GameController : MonoBehaviour
{
    public static GameController Instance{get; private set;}
    enum State{
        Ready,
        Play,
        GameOver
    }

    State state;
    int score;

    public AzarasiController azarashi;
    public GameObject blocks; 
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI stateText;

    void Start()
    {
        Ready();
    }

    void Awake()
    {
        Instance = this;
    }

    void Ready(){
        state = State.Ready;

        azarashi.SetSteerActive(false);
        blocks.SetActive(false);

        scoreText.text = $"Score : 0";
        stateText.gameObject.SetActive(true);
        stateText.text = "Ready?";
    }

    void GameStart(){
        state = State.Play;
        
        azarashi.SetSteerActive(true);
        blocks.SetActive(true);

        azarashi.Flap();

        stateText.gameObject.SetActive(false);
        stateText.text = " ";
    }

    void GameOver(){
        state = State.GameOver;

        ScrollObject[] scrollObjects = FindObjectsOfType<ScrollObject>();
        foreach(ScrollObject so in scrollObjects) so.enabled = false;

        stateText.gameObject.SetActive(true);
        stateText.text = "GameOver";
    }

    void Reload(){
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    void LateUpdate()
    {
        switch(state){
            case State.Ready:
                if(Input.GetButtonDown("Fire1")) GameStart();
                break;
            case State.Play:
                if(azarashi.IsDead) GameOver();
                break;
            case State.GameOver:
                if(Input.GetButtonDown("Fire1")) Reload();
                break;
        }   
    }

    public void IncreaseScore(){
        score++;
        scoreText.text = $"Score : {score}";
    }
}
