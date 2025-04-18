using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{

    public static GameStateManager Instance;

    [HideInInspector]
    public int sheepSaved;

    [HideInInspector]
    public int sheepDropped;

    public int sheepDroppedBeforeGameOver;

    public SheepSpawner sheepSpawner;

    void Awake()
    {
        Instance = this;
    }

    //add # sheep saved
    public void SavedSheep()
    {
        sheepSaved++;
        UIManager.Instance.UpdateSheepSaved();
    }

    //delete all sheep when game ends
    private void GameOver()
    {
        sheepSpawner.canSpawn = false;
        sheepSpawner.DestroyAllSheep();

        UIManager.Instance.ShowGameOverWindow();
    }

    // add # sheep dropped + call game over if sheep dropped = 3
    public void DroppedSheep()
    {
        sheepDropped++;
        UIManager.Instance.UpdateSheepDropped();

        if (sheepDropped >= sheepDroppedBeforeGameOver)
        {
            GameOver();
        }
    }

    // Update is called once per frame
    void Update()
    {   
        //title scene
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title");
        }
    }
}
