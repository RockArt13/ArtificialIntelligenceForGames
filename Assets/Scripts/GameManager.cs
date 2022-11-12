using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // This is the single instance of the class
    private static GameManager instance = null;

    // Game Objects
    [SerializeField] GameObject greenPlayerPrefab;
    [SerializeField] GameObject purplePlayerPrefab;

    public GameObject goal;

    // Green Players
    private const int numGreenPlayers = 5;
    private readonly List<Player> greenPlayers = new(numGreenPlayers);

    // Purple Players
    private const int numPurplePlayers = 2;
    private readonly List<Player> purplePlayers = new(numPurplePlayers);

    public List<GameObject> obstacles = new();
    private readonly List<GameObject> fences = new();

    public string purpleTag = "Purple";
    public string greenTag = "Green";
    public string fencesTag = "Boundary";
    public string obstacleTag = "Obstacle";

    public static int caught = 0; // How many Green players are in jail.

    public bool gameOver = false; // Tracking the end of the game

    public const int numPlayers = numGreenPlayers + numPurplePlayers; // number of ALL players

    [SerializeField] private Text caughtText; // text for the number of green players who have been caught
    [SerializeField] private Text endText; // text for the "Game Over"
    [SerializeField] private Text timerText; // text for the Timer

    private float timer = 0.0f; // Timer (as a winning condition for Green Players)
    private float gameDuration = 15f;

    // Game Sounds
    [SerializeField] public AudioSource jailedSound;
    [SerializeField] public AudioSource obsticleSound;
    [SerializeField] public AudioSource noExitSound;
    [SerializeField] public AudioSource timesUp;
    [SerializeField] public AudioSource purpleWin;
    [SerializeField] public AudioSource timerBackground;

    //Blue background image
    [SerializeField] public Image imgEnd;

   
    // Start is called before the first frame update
    void Start()
    {
        // If there already is an official instance, this instance deletes itself.
        if (instance == null)
        { instance = this; }
        else
        {
            Destroy(this);
            return;
        }

        foreach (GameObject obstacle in GameObject.FindGameObjectsWithTag(obstacleTag))
        {
            obstacles.Add(obstacle);
        }

        foreach (GameObject fence in GameObject.FindGameObjectsWithTag(fencesTag))
        {
            fences.Add(fence);
        }

        for (int i = 0; i < numGreenPlayers; i++)
        {
            GameObject newGreenPlayer = Instantiate(greenPlayerPrefab, new Vector3(0f + i, 0f, 0f), Quaternion.identity);
            greenPlayers.Add(newGreenPlayer.GetComponent<GreenPlayer>());
        }

        for (int i = 0; i < numPurplePlayers; i++)
        {
            GameObject newPurplePlayer = Instantiate(purplePlayerPrefab, new Vector3(-3.0f + 3*i, 0f, -20f), Quaternion.identity);
            purplePlayers.Add(newPurplePlayer.GetComponent<PurplePlayer>());
        }

        caughtText.text = "Caught: " + caught;     // 0 players 
        timerText.text = "";                      // 15 seconds
    }

    void Update()
    {
        caughtText.text = "Caught: " + caught; // Updating caught players count
        timer += Time.deltaTime;
        float seconds = timer % 60;

        timerText.text = "Timer: " + (gameDuration - Mathf.Round(seconds)); // Updating the timer

        if (seconds > gameDuration && !gameOver) // times up!
        {
            GreenWins();
        }

        if (caught == numGreenPlayers && !gameOver) // all green player have been caught!
        {
            PurpleWins();
        }

    }

    // Return the single instance of the class
    public static GameManager Instance()
    {
        return instance;
    }

   
    //if Purple Team Wins
    private void PurpleWins()
    {
        TurnOffAllMusic();
        purpleWin.Play();
        imgEnd.gameObject.SetActive(true);
        endText.text = "Purple Team Wins!";
        Time.timeScale = 0;
        gameOver = true;
    }

    //if Green Team Wins
    private void GreenWins()
    {
        TurnOffAllMusic();
        timesUp.Play();
        imgEnd.gameObject.SetActive(true);
        endText.text = "Time's Up!\n\n\n" + "Green Team Wins!";
        Time.timeScale = 0;
        gameOver = true;
    }

    public void TurnOffAllMusic()
    {
        Destroy(GetComponent<AudioSource>());

        //AudioListener.volume = 0f;
        timerBackground.Pause();
        jailedSound.Pause();
        obsticleSound.Pause();
        noExitSound.Pause();

    }

    public void RemoveGreenPlayer()
    {
        jailedSound.Play();
        caught++;
    }
}

