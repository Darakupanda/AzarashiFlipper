using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTrigger : MonoBehaviour
{
    public GameController gameController;

    void Start()
    {
    //    gameController = FindObjectOfType<GameController>();
        gameController = GameController.Instance;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        gameController.IncreaseScore();
    }

}