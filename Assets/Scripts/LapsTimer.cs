using UnityEngine;
using TMPro;

public class LapsTimer : MonoBehaviour
{
    [SerializeField] int numberOfLaps = 5;
    [SerializeField] TextMeshProUGUI timerText = null;
    [SerializeField] TextMeshProUGUI lapsText = null;

    float startTime = 0;
    int timer = 0;
    int laps = 0;
    bool isRaceRunning = false;
    bool wentThroughCheckpoint = true;

    MenuManager menuManager;

    void Start() 
    {
        menuManager = FindObjectOfType<MenuManager>();
        if (menuManager == null)
        {
            Debug.LogError("MenuManager is null, add MenuManager to the scene");
        }
    }

    void Update()
    {
        if (isRaceRunning)
        {
            UpdateTimer();
        }
    }

    private void UpdateTimer()
    {
        float time = Time.time - startTime;
        timer = Mathf.RoundToInt(time);
        float minutes = Mathf.Floor(time / 60);
        float seconds = Mathf.RoundToInt(time % 60);
        if (seconds < 10)
        {
            timerText.text = minutes.ToString() + ":0" + seconds.ToString();
        }
        else
        {
            timerText.text = minutes.ToString() + ":" + seconds.ToString();
        }
    }

    public int GetTimeInSeconds()
    {
        return timer;
    }

    public void AddLap()
    {
        if (wentThroughCheckpoint == true) 
        {
            Debug.Log("Adding a lap");
            wentThroughCheckpoint = false;
            laps += 1;

            if (laps > numberOfLaps)
            {
                isRaceRunning = false;
                lapsText.text = "YOU WON!";
                menuManager.DisplayEndMenu();
            }
            else if (laps == 1) // first lap should start the timer
            {
                isRaceRunning = true;
                startTime = Time.time;
                lapsText.text = laps.ToString() + "/" + numberOfLaps.ToString();
            }
            else
            {
                lapsText.text = laps.ToString() + "/" + numberOfLaps.ToString();
            }
        }
        else
        {
            Debug.Log("Not went through checkpoint, can't add a lap");
        }
    }

    public void GoThroughCheckpoint()
    {
        Debug.Log("Going through checkpoint");
        wentThroughCheckpoint = true;
    }
}
