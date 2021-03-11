using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessibilityOption : MonoBehaviour
{    
    public static bool accessibilityMode = false;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetAccessibility(bool value)
    {
        accessibilityMode = value;
    }
    public void ToggleAccesibility()
    {
        accessibilityMode = !accessibilityMode;
    }
}
