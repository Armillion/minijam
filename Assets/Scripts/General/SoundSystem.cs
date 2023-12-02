using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    [SerializeField] private List<AudioClip> sounds;
    [SerializeField] public AudioSource audioSource;
    public List<bool> clauses;

    // Update is called once per frame
    void Update()
    {
        if(Random.Range(1,1200) <= 2)
        {
            clauses[3] = true;
        }

        for(int i = 0; i < clauses.Count; i++)
        {
            if (clauses[i])
            {
                audioSource.PlayOneShot(sounds[i]);
                clauses[i] = false;
            }
        }
    }
}
