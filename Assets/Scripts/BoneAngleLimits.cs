using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneAngleLimits : MonoBehaviour
{
    private const float m_minAllowedAngle = -180;
    private const float m_maxAllowedAngle = 180f;

    [SerializeField][Range(m_minAllowedAngle, m_maxAllowedAngle)]
    private float m_minAngle;
    [SerializeField][Range(m_minAllowedAngle, m_maxAllowedAngle)]
    private float m_maxAngle;
    [SerializeField]
    private bool m_flip;

    public float minAngle
    {
        get { return m_minAngle;  }
        set { m_minAngle = Mathf.Max(value, m_minAllowedAngle); }
    }

    public float maxAngle
    {
        get { return m_maxAngle; }
        set { m_maxAngle = Mathf.Max(value, m_maxAllowedAngle); }
    }

    public bool flip
    {
        get { return m_flip; }
        set { m_flip = value; }
    }

    public float ClampAngle(float angle, float minAngle, float maxAngle, float clampAroundAngle = 0)
    {
        if (angle > 180f) // remap 0 - 360 --> -180 - 180
            angle -= 360f;
        angle = Mathf.Clamp(angle, minAngle, maxAngle);
        if (angle < 0f) // map back to 0 - 360
            angle += 360f;
        return angle;
    }
}
