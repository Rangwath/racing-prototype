using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] bool isFinishLine;

    LapsTimer lapsTimer = null;

    void Start()
    {
        lapsTimer = FindObjectOfType<LapsTimer>();
        if (lapsTimer == null)
        {
            Debug.LogError("LapTimer is null, add LapTimer to the scene");
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (isFinishLine)
        {
            lapsTimer.AddLap();
        }
        else
        {
            lapsTimer.GoThroughCheckpoint();
        }
    }
}
