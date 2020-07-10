﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int Lives { get; private set; }

    public event Action<int> OnLivesChanged;
    public event Action<int> OnCoinsChange;

    private int coins;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);

            RestartGame();
        }
    }

    internal void KillPlayer()
    {
        Lives--;
        if(OnLivesChanged != null)
            OnLivesChanged(Lives);

        if (Lives <= 0)
            RestartGame();
        else
            SendPlayerToCheckpoint();
    }

    private void SendPlayerToCheckpoint()
    {
        var checkpointManager = FindObjectOfType<CheckpointManager>();

        var checkpoint = checkpointManager.GetLastCheckpointThatWasPassed();

        var player = FindObjectOfType<PlayerMovementController>();

        player.transform.position = checkpoint.transform.position;
    }

    internal void AddCoin()
    {
        coins++;
        if (OnCoinsChange != null)
            OnCoinsChange(coins);
    }

    private void RestartGame()
    {
        Lives = 3;
        coins = 0;

        if (OnCoinsChange != null)
            OnCoinsChange(coins);

        SceneManager.LoadScene(0);
    }
}
