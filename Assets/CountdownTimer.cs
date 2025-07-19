using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public float totalTime = 10f;
    private float remainingTime;

    public TMP_Text timerText;
    public AudioSource backgroundAudio;

    public float minPitch = 1f;
    public float maxPitch = 2f;

    public RectTransform endImage;
    public float popupTargetScale = 0.62f;
    public float scaleDuration = 0.5f;


    private float timeLeft;
    private bool hasTriggeredPopup = false;

    void Start()
    {
        remainingTime = totalTime;
        if (backgroundAudio != null)
            backgroundAudio.pitch = minPitch;

        endImage.localScale = Vector3.zero; // hide the image initially
    }
    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            UpdateTimerDisplay();
            UpdateAudioPitch();
        }
        else if (!hasTriggeredPopup)
        {
            remainingTime = 0;
            UpdateTimerDisplay();
            hasTriggeredPopup = true;
            StartCoroutine(ScaleUpImage());
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
    
        System.Collections.IEnumerator ScaleUpImage()
    {
        float elapsed = 0f;
        Vector3 targetScale = new Vector3(popupTargetScale, popupTargetScale, popupTargetScale);
        while (elapsed < scaleDuration)
        {
            float t = elapsed / scaleDuration;
            endImage.localScale = Vector3.Lerp(Vector3.zero, targetScale, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        endImage.localScale = targetScale; // final snap to correct scale
    }
}
