using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private DeliveryCounter deliveryCounter;

    private int score = 0;



    private void Start()
    {
        deliveryCounter.OnBreadDelivered += DeliveryCounter_OnBreadDelivered;

        scoreText.text = "Bread loafs delivered: " + score.ToString();

    }
    private void DeliveryCounter_OnBreadDelivered(object sender, System.EventArgs e)
    {
        score++;
        Debug.Log("Event received");
        scoreText.text = "Bread loafs delivered: " + score.ToString();
    }
    
    
}
    