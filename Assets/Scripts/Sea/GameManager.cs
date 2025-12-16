using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    [SerializeField] private float delayTime = 5f;
    [SerializeField] private GameObject screen;
    [SerializeField] private TextMeshProUGUI restart;
    [SerializeField] private TextMeshProUGUI scoreText;

    public void Start() {
        screen.SetActive(false);
    }

    public void Update() {
        if ( gameHasEnded == true ) {
            restart.text = "restart " + Mathf.Ceil(delayTime -= Time.deltaTime) + " seconds later";
        }
    }
    public void EndGame() {
        if (gameHasEnded == false) {
            gameHasEnded = true;
            scoreText.text = BoatController.Instance.score.ToString();
            screen.SetActive(true);
            Debug.Log("GAME OVER");
            Invoke("Restart", delayTime);
        }
    }

    void Restart () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
