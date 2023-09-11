using UnityEngine;

public static class AudioSourceExtensions
{
    public static void Pause(this AudioSource audioSource, bool value)
    {
        if (value)
            audioSource.Pause();
        else
            audioSource.UnPause();
    }
}
