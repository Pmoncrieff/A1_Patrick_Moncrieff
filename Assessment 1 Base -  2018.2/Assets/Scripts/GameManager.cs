﻿using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager>
{

    private float _timeRemaining;

    public float TimeRemaining
    {
        get { return _timeRemaining; }
        set { _timeRemaining = value; }
    }

    private int _numCoins;

    public int NumCoins
    {
        get { return _numCoins; }
        set { _numCoins = value; }
    }

    private float maxTime = 2 * 60; //in seconds

    // Use this for initialization
    void Start()
    {

        TimeRemaining = maxTime;

    }

    // Update is called once per frame
    void Update()
    {

        TimeRemaining -= Time.deltaTime;

        if (TimeRemaining <= 0)
        {
            Application.LoadLevel(Application.loadedLevel);
            TimeRemaining = maxTime;
        }

    }
}