using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = BoatController.Instance.score.ToString();
    }
}
