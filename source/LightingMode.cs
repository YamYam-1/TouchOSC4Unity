using UnityEditor;
using UnityEngine;

public class LightingMode
{
    public struct LightState
    {
        public Color color;
        public float intensity;
    }

    public delegate void RotationMode(float beat, Transform targets, int idx);
    public delegate LightState ColorMode(float beat, float fade, Color color_a, Color color_b, int idx);

    // 회전 모드
    public enum  RotationModeName { Common, Odd_Even };
    static RotationMode[] Rmodes = { Common, Odd_Even };

    public static RotationMode RGetMode(RotationModeName name)
    {
        return Rmodes[(int)name];
    }

    // 색 모드
    public enum ColorModeName { Common, Odd_Even, Flashing };
    static ColorMode[] Cmodes = { Common, Odd_Even, Flashing };
    
    public static ColorMode CGetMode(ColorModeName name)
    {
        return Cmodes[(int)name];

    }

    
    public static void Common(float beat, Transform targets, int idx)
    {

        float angle = (1f - Mathf.Cos(beat * Mathf.PI * 2f)) * 30f;

        float x = Mathf.Sin(beat * Mathf.PI * 2f) * 60f;
        float y = Mathf.Cos(beat * Mathf.PI * 2f) * 45f;

        targets.localRotation = Quaternion.Euler(x, y, 0f);

    }

    public static void Odd_Even(float beat, Transform targets, int idx)
    {
        float offset = (idx % 2 == 0) ? 0.5f : 0f;
        beat += offset;
        float angle = (1f - Mathf.Cos(beat * Mathf.PI * 2f)) * 30f;
        targets.localRotation = Quaternion.Euler(angle, 0f, 0f);
    }


    public static LightState Common(float beat, float fade, Color color_a, Color color_b, int idx)
    {
        LightState state;

        state.color = color_a;
        state.intensity = fade;

        return state;
    }

    public static LightState Odd_Even(float beat, float fade, Color color_a, Color color_b, int idx)
    {
        Color color = (idx % 2 == 0) ? color_a : color_b;

        LightState state;
        state.color = color;
        state.intensity = fade;
        return state;
    }
    
    public static LightState Flashing(float beat, float fade, Color color_a, Color color_b, int idx)
    {
        float pulse = Mathf.Sin(Time.time * 8f) * 0.5f + 0.5f;
        pulse = Mathf.SmoothStep(0f, 1f, pulse);

        LightState state;
        state.color = color_a;
        state.intensity = pulse * fade;
        return state;
        
    }

}
