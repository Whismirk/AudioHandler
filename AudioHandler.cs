using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [SerializeField] private Coroutine currentLerp;
    [SerializeField] private AudioSource audioSource;
        public void SetSource(AudioSource source) {audioSource = source;}

    [SerializeField] private float lerpDuration;
        public float LerpDuration {get {return lerpDuration;}}
        public void SetLerpDuration(float newDuration) {lerpDuration = newDuration;}


    public void AcceleratePitch(float value) {ChangePitch(audioSource.pitch + value);}
    public void SlowPitch(float value) {ChangePitch(audioSource.pitch - value);}
    public void ResetPitch() {ChangePitch(1);}
    
    private void ChangePitch(float target)
    {
        if(currentLerp != null) StopCoroutine(currentLerp);
        currentLerp = StartCoroutine(PitchLerp(target, lerpDuration));
    }

    private IEnumerator PitchLerp(float target, float duration)
    {
        float clock = 0;
        float origin = audioSource.pitch;
        while(clock < 1)
        {
            clock += Time.deltaTime / duration;
            audioSource.pitch = Mathf.Lerp(origin, target, clock);
            yield return null;
        }
        audioSource.pitch = target;
    }
}
