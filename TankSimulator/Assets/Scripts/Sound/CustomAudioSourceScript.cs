using UnityEngine;

public class CustomAudioSourceScript : MonoBehaviour
{
    private void Start()
    {
         Invoke("CheckAudioSourceDestroy", GetComponent<AudioSource>().clip.length + 0.1f);

    }

    private void CheckAudioSourceDestroy()
    {
        if (!GetComponent<AudioSource>().isPlaying)
        {
            Destroy(gameObject);
        }
        else
        {
            Invoke("CheckAudioSourceDestroy", GetComponent<AudioSource>().clip.length + 0.1f);
        }
    }
}
