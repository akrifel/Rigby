using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static bool isGameOver;
    public GameObject gameOverScreen;
    public GameObject completeGameScreen;
    public GameObject player;

    public GameObject diamondUI;

    private void Awake() 
    {
        isGameOver = false;
    }

    void Update()
    {
        CheckIsOver();
    }

    public void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void CheckIsOver()
    {
        if (isGameOver){
            gameOverScreen.SetActive(true);
            player.SetActive(false);
            diamondUI.SetActive(false);
        }
    }

    public IEnumerator CompleteGame(){
        completeGameScreen.SetActive(true);
        yield return new WaitForSeconds(1);
        player.SetActive(false);
        diamondUI.SetActive(false);
    }
}