using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinWave : MonoBehaviour
{
    public float amplitude = 1f, speed = 1f;

    public enum SinState
    {
        vertical,
        horizontal
    }

    public SinState sinState;

    Vector2 initialPos;

    private void Awake() => initialPos = transform.position;

    void Update() 
    {
        float xAxis = 0, yAxis = 0;

        switch(sinState)
        {
            case SinState.vertical:
                yAxis = amplitude * Mathf.Sin(Time.time * speed);
                break;

            case SinState.horizontal:
                xAxis = amplitude * Mathf.Sin(Time.time * speed);
                break;
        }

        transform.position = new Vector2(xAxis, yAxis) + initialPos;
    }

    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
        {
            switch (sinState)
            {
                case SinState.vertical:
                    Gizmos.DrawLine(initialPos, new Vector2(initialPos.x, initialPos.y + amplitude));
                    Gizmos.DrawLine(initialPos, new Vector2(initialPos.x, initialPos.y - amplitude));
                    break;

                case SinState.horizontal:
                    Gizmos.DrawLine(initialPos, new Vector2(initialPos.x + amplitude, initialPos.y));
                    Gizmos.DrawLine(initialPos, new Vector2(initialPos.x - amplitude, initialPos.y));
                    break;
            }
        } else
        {
            switch (sinState)
            {
                case SinState.vertical:
                    Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + amplitude));
                    Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - amplitude));
                    break;

                case SinState.horizontal:
                    Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + amplitude, transform.position.y));
                    Gizmos.DrawLine(transform.position, new Vector2(transform.position.x - amplitude, transform.position.y));
                    break;
            }
        }
    }
}
