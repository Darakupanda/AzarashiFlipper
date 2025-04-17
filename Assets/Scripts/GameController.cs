using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    enum State{
        Ready,
        Play,
        GameOver
    }

    State state;

    public AzarasiController azarashi;
    public GameObject blocks;

    void Start()
    {
        Ready();
    }

    void Ready(){
        state = State.Ready;

        azarashi.SetSteerActive(false);
        blocks.SetActive(false);
    }

    void GameStart(){
        state = State.Play;
        
        azarashi.SetSteerActive(true);
        blocks.SetActive(true);

        azarashi.Flap();
    }

    void GameOver(){
        state = State.GameOver;

        ScrollObject[] scrollObjects = FindObjectsOfType<ScrollObject>();
        foreach(ScrollObject so in scrollObjects) so.enabled = false;
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
}
