using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlCounterVisual : MonoBehaviour
{
    [SerializeField] private BowlCounter bowlCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform bowlVisualPrefab;


    private List<GameObject> bowlVisualGameObjectList;

    private void Awake()
    {
        bowlVisualGameObjectList = new List<GameObject>();
    }

    private void Start()
    {
        bowlCounter.OnBowlSpawned += BowlCounter_OnBowlSpawned;
        bowlCounter.OnBowlRemoved += BowlCounter_OnBowlRemoved;
    }

    private void BowlCounter_OnBowlSpawned(object sender, System.EventArgs e)
    {
        Transform bowlVisualTransform = Instantiate(bowlVisualPrefab, counterTopPoint);

        float bowlOffsetY = 0.1f;
        bowlVisualTransform.localPosition = new Vector3(0, bowlOffsetY * bowlVisualGameObjectList.Count, 0);

        bowlVisualGameObjectList.Add(bowlVisualTransform.gameObject);
    }
    private void BowlCounter_OnBowlRemoved(object sender, System.EventArgs e)
    {
        GameObject bowlGameObject = bowlVisualGameObjectList[bowlVisualGameObjectList.Count - 1];

        bowlVisualGameObjectList.Remove(bowlGameObject);
        Destroy(bowlGameObject);
    }
}
