using UnityEngine;
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

    private float _playerHealth;

    public float PlayerHealth
    {
        get { return _playerHealth; }
        set { _playerHealth = value; }
    }

    private float maxTime = 2 * 60; //in seconds

    private int maxHealth = 5;

    private bool isInvulnerable = false;

    private int totalCoinsInLevel;

    private bool gameOver = false;

    void OnEnable()
    {
        DamagePlayerEvent.OnDamagePlayer += DecrementPlayerHealth;
    }

    void OnDisable()
    {
        DamagePlayerEvent.OnDamagePlayer -= DecrementPlayerHealth;
    }

    // Use this for initialization
    void Start()
    {

        TimeRemaining = maxTime;
        PlayerHealth = maxHealth;

        totalCoinsInLevel = GameObject.FindGameObjectsWithTag("Coin").Length;

    }

    // Update is called once per frame
    void Update()
    {

        TimeRemaining -= Time.deltaTime;
        Debug.Log(TimeRemaining);

        if (TimeRemaining <= 0)
        {
            Restart();
        }

        if (_numCoins == totalCoinsInLevel && !gameOver)
        {
            StartCoroutine(WonGame());
        }

    }

    private void DecrementPlayerHealth(GameObject player)
    {
        if (isInvulnerable)
        {
            return;
        }

        StartCoroutine(InvulnerableDelay());

        PlayerHealth--;

        if (PlayerHealth <= 0)
        {
            Restart();
        }
    }

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
        TimeRemaining = maxTime;
        PlayerHealth = maxHealth;
    }

    private IEnumerator InvulnerableDelay()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(1.0f);
        isInvulnerable = false;
    }

    public float GetPlayerHealthPercentage()
    {
        return PlayerHealth / (float)maxHealth;
    }

    private IEnumerator WonGame()
    {
        gameOver = true;
        FindObjectOfType<UpdateUI>().wonGamePanel.SetActive(true);
        yield return new WaitForSeconds(3);
        GameManager.Instance.Restart();
    }
}