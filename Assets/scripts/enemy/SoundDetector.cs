using UnityEngine;

public class SoundDetector : MonoBehaviour
{
    public delegate void SoundHeardEventHandler(Vector3 soundPosition);
    public static event SoundHeardEventHandler OnFootstep;

    public static void AddFootstepListener(SoundHeardEventHandler listener)
    {
        OnFootstep += listener;
    }

    public static void RemoveFootstepListener(SoundHeardEventHandler listener)
    {
        OnFootstep -= listener;
    }

    public static void TriggerFootstep(Vector3 soundPosition)
    {
        OnFootstep?.Invoke(soundPosition);
    }
}