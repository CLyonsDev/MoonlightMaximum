using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScore : MonoBehaviour {

    public Text scoreText;

    public int points = 0;

    void Start()
    {
        scoreText.text = "Points: " + points;
    }

    public void AddPoints(int amt)
    {
        points += amt;
        scoreText.text = "Points: " + points;
    }

    public void RemovePoints(int amt)
    {
        points -= amt;
        scoreText.text = "Points: " + points;
    }
}
