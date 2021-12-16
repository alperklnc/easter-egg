using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    
    PlatformManager platformManager;
    
    public GameState state;

    private static GameManager instance;

    public static GameManager Instance { get { return instance; } }

    private void Awake() {
        if(instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
        }

        
        state = GameState.Waiting;

        platformManager = FindObjectOfType<PlatformManager>();
        /*
        obstacleManager = FindObjectOfType<ObstacleManager>();
        cameraController = FindObjectOfType<CameraController>();
        */
    }

    private void Start()
    {
        platformManager.CreatePlatform(60);
    }
    
    public void StartPlaying()
    {
        state = GameState.Playing;
    }

    public void StopPlaying()
    {
        state = GameState.EndGame;
    }

    public bool IsPlaying() {
        return state == GameState.Playing;
    }

    public bool IsEndGame()
    {
        return state == GameState.EndGame;
    }


}

public enum GameState {
    Waiting,
    Playing,
    EndGame
}
