using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Text breadDeliveredText;
    [SerializeField] private DeliveryCounter deliveryCounter;

    private int score = 0;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        deliveryCounter.OnBreadDelivered += DeliveryCounter_OnBreadDelivered;

        Hide();
    }

    private void DeliveryCounter_OnBreadDelivered(object sender, System.EventArgs e)
    {
        score++;
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        Debug.Log("State changed!");
        if (GameManager.Instance.IsGameOver())
        {
            Show();
            breadDeliveredText.text = score.ToString();
        }
        else
        {
            Hide();
        }
    }


    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
