using UnityEngine;

public class EmissionHighlight : MonoBehaviour
{
    private Material mat;
    private Color originalEmission;
    public Color highlightColor = Color.yellow;

    void Start()
    {
        mat = GetComponent<Renderer>().material;

        // Emission 기본 색상 저장
        if (mat.HasProperty("_EmissionColor"))
        {
            originalEmission = mat.GetColor("_EmissionColor");
            HighlightOn();
        }
    }

    public void HighlightOn()
    {
        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", highlightColor);
    }

    public void HighlightOff()
    {
        mat.SetColor("_EmissionColor", originalEmission);
        mat.DisableKeyword("_EMISSION");
    }
}