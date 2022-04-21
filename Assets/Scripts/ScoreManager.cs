using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{

    private static ScoreManager _instance;
    public static ScoreManager Instance { get { return _instance; } }

    private int maxTemps = 60 * 5;
    private int score = 0;
    private float time = 0;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            SceneManager.LoadScene("MenuScene");
        time += Time.deltaTime;
        tempsLimite
    }

    public void AddScore(int inc = 1)
    {
        score += inc;
        Debug.Log($"Score: {score} (+{inc})"); // Equivalent a Debug.Log("Score: " + score + "(+" + inc + ")");
    }

    public void tempsLimite(float time)
    {
        if (time >= maxTemps)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
