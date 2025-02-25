using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    public float peopleScore = 0;
    public float totalPeople;
    public float whenToIncrement;
    public TextMeshProUGUI scoreText;
    public GameObject[] peoples;

    private bool hasIncreased;
    public AudioClip scoreUpSound;

    void Update()
    {
        totalPeople = GameObject.FindGameObjectsWithTag("people").Length;
        if (whenToIncrement > 0) whenToIncrement = totalPeople - 2;

        if (totalPeople == whenToIncrement && !hasIncreased)
        {
            AudioInterface.instance.PlayClip(scoreUpSound, transform, 0.05f);

            peopleScore++;
            hasIncreased = true;
            Vector3 peoplePlace = new Vector3(Random.Range(-10, 10), Random.Range(0, 7), 0);
            Vector3 peoplePlace2 = new Vector3(Random.Range(-10, 10), Random.Range(0, 7), 0);
            Vector3 rot1 = new Vector3(0, 0, Random.Range(0, 360));
            Vector3 rot2 = new Vector3(0, 0, Random.Range(0, 360));
            Vector3 rot3 = new Vector3(0, 0, 0);

            Vector3[] rotArray = { rot1, rot2, rot3, rot3, rot3 };
            Instantiate(peoples[0], peoplePlace, Quaternion.Euler(rotArray[Random.Range(0,4)]));
            Instantiate(peoples[1], peoplePlace2, Quaternion.Euler(rotArray[Random.Range(0, 4)]));

        }
        else
        {
            hasIncreased = false;
        }


        scoreText.text = "couples: " + peopleScore;

    }
}
