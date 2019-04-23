using UnityEngine;
using UnityEngine.Audio;
using Valve.VR;

public class ControllerMixerInterface : MonoBehaviour
{
    [SerializeField]
    private AudioMixer mixer;

    [SerializeField]
    private float maxPitchDeviation = .25f;

    public void PitchChange(Vector2 position)
    {
        mixer.SetFloat("pitchShift", 1.0f + position.x * maxPitchDeviation);
    }

    public void ModulationChange(Vector2 position)
    {
        mixer.SetFloat("pitchShift", 1.0f + position.x * maxPitchDeviation);
    }
}
