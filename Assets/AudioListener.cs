using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AudioListener : MonoBehaviour
{
    public AudioSource audioSource;
    public float updateStep = 0.1f;
    public int sampleDataLength = 1024;

    private float currentUpdateTime = 0f;

    private float clipLoudness;
    private float[] clipSampleData;
    public GameObject sphere;
    private float mappedLoudness;
    public Light2D light;

    void Awake()
    {
        if (!audioSource)
        {
            Debug.LogError("No Audio");
        }

        clipSampleData = new float[sampleDataLength];
    }

    // Update is called once per frame
    void Update()
    {
        currentUpdateTime += Time.deltaTime;
        if (currentUpdateTime >= updateStep) {
            currentUpdateTime = 0f;
            audioSource.clip.GetData(clipSampleData, audioSource.timeSamples); //I read 1024 samples, which is about 80 ms on a 44khz stereo clip, beginning at the current sample position of the clip.
            clipLoudness = 0f;
            foreach (var sample in clipSampleData) {
                clipLoudness += Mathf.Abs(sample);
            }
            clipLoudness /= sampleDataLength; //clipLoudness is what you are looking for
            Debug.Log(clipLoudness);
            mappedLoudness = math.remap(0, 0.1f, 100, 1000, clipLoudness);
            // gameObject.transform.localScale = new Vector3(mappedLoudness , mappedLoudness);
            light.pointLightOuterRadius = mappedLoudness;
            
        }
    }
}
