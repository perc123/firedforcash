using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public float totalTime = 155f;
    private float remainingTime;

    public TMP_Text timerText;
    public AudioSource backgroundAudio;

    public float minPitch = 1f;
    public float maxPitch = 2f;

    void Start()
    {
        remainingTime = totalTime;
        if (backgroundAudio != null)
            backgroundAudio.pitch = minPitch;
    }

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            UpdateTimerDisplay();
            UpdateAudioPitch();
        }
        else
        {
            remainingTime = 0;
            UpdateTimerDisplay();
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    void UpdateAudioPitch()
    {
        if (backgroundAudio == null) return;

        float t = 1f - (remainingTime / totalTime); // 0 at start, 1 at end
        backgroundAudio.pitch = Mathf.Lerp(minPitch, maxPitch, t);
    }
}
