using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DisallowMultipleComponent]
public class PerObjectColor : MonoBehaviour
{
    static readonly int ColorId = Shader.PropertyToID("_Color");
    static readonly int FadeId = Shader.PropertyToID("_Fade");

    [SerializeField, ColorUsage(true, true)]
    Color color_A = Color.white;

    [SerializeField, ColorUsage(true, true)]
    Color color_B = Color.white;

    Color baseColor;

    [SerializeField, Range(0f, 10f)]
    float fade = 0.05f;

    [SerializeField]
    float bpm = 10f;


    [SerializeField]
    LightingMode.RotationModeName rmode;

    [SerializeField]
    LightingMode.ColorModeName cmode;

    float baseFade;

    public Renderer[] renderers;

    public Transform[] targets;


    MaterialPropertyBlock block;

    float beat;

    void Awake()
    {

        block = new MaterialPropertyBlock();


        if (targets == null || targets.Length == 0)
            targets = GetComponentsInChildren<Transform>().Where(t => t.parent == transform).ToArray();


        if (renderers == null || renderers.Length == 0)
        {
            List<Renderer> list = new List<Renderer>();

            foreach (var t in targets)
            {
                if (t.childCount > 0)
                {
                    Renderer r = t.GetChild(0).GetComponent<Renderer>();

                    if (r != null)
                        list.Add(r);
                }
            }


            renderers = list.ToArray();
        }

    }

    void Update()
    {

        beat += Time.deltaTime * (bpm / 60f);


        LightingMode.RotationMode rm = LightingMode.RGetMode(rmode);
        LightingMode.ColorMode cm = LightingMode.CGetMode(cmode);

        for (int i = 0; i < targets.Length; i++)
        {
            rm(beat, targets[i], i);
            LightingMode.LightState state = cm(beat, fade, color_A, color_B, i);

            Apply(renderers[i], state.color, state.intensity);
        }

    }


    void Apply(Renderer rend, Color color, float fade)
    {
        rend.GetPropertyBlock(block);
        block.SetColor(ColorId, color);
        block.SetFloat(FadeId, fade);
        rend.SetPropertyBlock(block);
    }


    public void SetFade(float value)
    {
        fade = value;

    }
    public void SetBpm(float value)
    {
        bpm = value;
    }

    public void SetRMode(int value)
    {
        rmode = (LightingMode.RotationModeName)value;
    }
    public void SetCMode(int value)
    {
        cmode = (LightingMode.ColorModeName)value;
    }
}
