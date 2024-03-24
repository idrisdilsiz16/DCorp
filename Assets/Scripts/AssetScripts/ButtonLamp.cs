using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLamp : MonoBehaviour
{
    public static ButtonLamp instance;
    public enum lampColors
    {
            Red,
            Yellow,
            Green,
            Blue,
    }

    public bool on = true;
    public Transform lamp;
    public lampColors lightColor;

    Renderer render;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        render = lamp.GetComponent<Renderer>();
        lightColor = lampColors.Green;
    }

    void Update()
    {
        if (on)
        {
            switch (lightColor)
            {
                case lampColors.Red:
                    render.material.SetColor("_EmissionColor", new Color(1f, 0f, 0.02f, 1f));
                    break;
                case lampColors.Yellow:
                    render.material.SetColor("_EmissionColor", new Color(1f, 0.65f, 0f, 1f));
                    break;
                case lampColors.Green:
                    render.material.SetColor("_EmissionColor", new Color(0.15f, 1f, 0f, 1f));
                    break;
                case lampColors.Blue:
                    render.material.SetColor("_EmissionColor", new Color(0f, 0.33f, 1f, 1f));
                    break;
                default:
                    break;
            }
            
        }
        else
        {
            render.material.SetColor("_EmissionColor", new Color(0.0f, 0.0f, 0.0f, 0.0f));
        }
    }
}
