using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RockPaperScissor : MonoBehaviour
{
    // References to the main GameObjects to hide them when not needed
    public GameObject Rock;
    public GameObject Paper;
    public GameObject Scissor;

    public Button resetButton;  // Reference to the Reset Button for the end of the game

    public GameObject gameOverPanel;    // Gameover Winning Status Panel
    public Text gameOverText;           // Text of Gameover Winning Status Panel

    public Text scoreText;              // Text of Current Score Panel

    public string[] choices = new string[] { "Rock", "Paper", "Scissor" };

    public ParticleSystem fireworks;    // Reference to Particle System Fireworks

    // Variables for technical counts and loops for game rules
    [Header("Set in Inspector")]
    public int numOfTurns = 10;

    [Header("Set Dynamically")]
    public int turns = 0;
    public int userScore = 0;
    public int computerScore = 0;
    public int ties = 0;

    // Main gameplay of the Rock Paper Scissors classical game
    void gamePlay(int userChoice) {

        System.Random rnd = new System.Random();
        int computerChoice = rnd.Next(3); // Random integer between 0 and 2

        if (userChoice == 0 && computerChoice == 2)             // Win Case #1
        {
            print("You win 1! You: Rock VS Computer: Scissor");
            userScore++;
        }

        else if (userChoice == 1 && computerChoice == 0)        // Win Case #2
        {
            print("You win! You: Paper VS Computer: Rock");
            userScore++;
        }

        else if (userChoice == 2 && computerChoice == 1)        // Win Case #3
        {
            print("You win You: Scissor VS Computer: Paper! ");
            userScore++;
        }

        else if (userChoice == computerChoice)                  // Tie Case
        {
            print("Tie"); 
            ties++;
        }

        else                                                    // Lose Case
        {
            print("You lose! You:" + choices[userChoice]  +" VS Computer: "  +choices[computerChoice]);
            computerScore++;
        }

        if(turns >= numOfTurns - 1) // Checks if it has passed the limit for turns
        {
            gameOver();     // Calls gameOver() to end the game if so
        }

    }

    // Displays Winner in the gameOverPanel and gives option to reset the game
    void gameOver() {

        // Hides the 3 GameObjects so user cannot click them
        Rock.SetActive(false);  
        Paper.SetActive(false);
        Scissor.SetActive(false);

        // Show gameOver Panel with status
        gameOverPanel.SetActive(true);
        if (userScore > computerScore)  // General Win Case
        {
            gameOverText.text = "You Win!";
            fireworks.Play();   // Play Fireworks Particle System
        }
        else if (userScore < computerScore) gameOverText.text = "You Lose!"; // Lose Case
        else gameOverText.text = "Tie Game!";   // Tie Case

    }

    // Listener action due to clicking "Reset Game" Button on GameOver Panel
    void TaskOnClick()  
    {
        endGame(); 
    }

    // endGame is called to completely reset the game back to "_Scene_0"
    void endGame() {   
        SceneManager.LoadScene("_Scene_0");     // Reload main scene "_Scene_0"
        // Reset variables to make sure a new game is from scratch
        userScore = 0;
        computerScore = 0;
        turns = 0;
        ties = 0;
    }

    // Awake is called as soon as the script is initiated
    void Awake()
    {
        gameOverPanel.SetActive(false); // Hide the GameOver Panel throughout the game till needed
    }

    // Start is called before the first frame update
    void Start()
    {
        Button btn = resetButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick); // Listens for the "Reset Game" Button Click
    }

    // Update is called once per frame
    void Update()
    {
        // Updates the Score every frame & round no.
        scoreText.text = "Round " +turns + " of " +numOfTurns +"\nWins: " + userScore + " Loses: " + computerScore +" Ties: "+ties;

        if (Input.GetMouseButtonDown(0))    // Checks for Mouse Button Click
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Rock")  // Rock Chosen
                {
                    gamePlay(0); turns++;
                }
                else if (hit.collider.gameObject.tag == "Paper")    // Paper Chosen
                {
                    gamePlay(1); turns++;
                }
                else if (hit.collider.gameObject.tag == "Scissor")  // Scissor Chosen
                {
                    gamePlay(2); turns++;
                }
            }
        }
    }
}
