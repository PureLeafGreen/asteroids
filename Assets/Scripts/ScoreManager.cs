using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{

    private static ScoreManager _instance;
    public GameObject bonusRapide;
    public static ScoreManager Instance { get { return _instance; } }

    private int score = 0;
    private float time;
    private float timeDisappear;
    private Transform spawn;

    private void Awake()
    {
        spawn.SetPositionAndRotation(new Vector3(Random.Range(-9, 9), Random.Range(-5, 5)), Quaternion.Euler(new Vector3(0, 0, 0)));
        time = 0;
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    private void Update()
    {
        time += Time.deltaTime;
        timeDisappear += Time.deltaTime;
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene("MenuScene");
        }
        if (time >= 5f)
        {
            Instantiate(bonusRapide, spawn.position, spawn.rotation);
            timeDisappear = 0;
        }
        if (timeDisappear >= 15f)
        {
            Destroy(bonusRapide);
        }
    }

    public void AddScore(int inc = 1)
    {
        score += inc;
        Debug.Log($"Score: {score} (+{inc})"); // Equivalent a Debug.Log("Score: " + score + "(+" + inc + ")");
    }

    //TODO Arreter la partie quand il n'y a plus d'asteroides.
}
