using UnityEngine;
using System.Collections;

public class GameFeelManager : MonoBehaviour
{
    public static GameFeelManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // EL HIT-STOP: Congela el tiempo brevemente
    public void HitStop(float duration)
    {
        StartCoroutine(HitStopRoutine(duration));
    }

    private IEnumerator HitStopRoutine(float duration)
    {
        Time.timeScale = 0.01f; // Casi congelado totalmente (antes era 0.05)
        yield return new WaitForSecondsRealtime(0.15f); // Aumentamos la duración a 0.15s
        Time.timeScale = 1f;
    }

}
