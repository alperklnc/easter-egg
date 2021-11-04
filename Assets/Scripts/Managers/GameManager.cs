using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
        platformManager.CreatePlatform(50);
    }
    
    public void StartPlaying()
    {
        state = GameState.Playing;
    }

    public bool IsPlaying() {
        return state == GameState.Playing;
    }


}

public enum GameState {
    Waiting,
    Playing,
    EndGame
}
