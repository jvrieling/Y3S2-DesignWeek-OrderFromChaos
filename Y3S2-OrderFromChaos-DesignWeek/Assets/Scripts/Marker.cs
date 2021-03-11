using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Marker : MonoBehaviour
{
    public float fadeDelay = 0.2f; // Number of seconds before fade begins
    public float fadeDuration = 1; // Number of seconds spent fading
    public Color startColour = new Color(255, 255, 255, 255); // Marker starts as this colour
    public Color endColour = new Color(255, 255, 255, 0); // Marker changes to this colour

    IEnumerator UpdateMarker(Vector2 newPosition)
    {
        // Update position of marker
        transform.position = newPosition;

        // Delay fade for number of seconds specified in inspector
        yield return new WaitForSeconds(fadeDelay);

        // Get image component of marker
        Image markerImage = GetComponent<Image>();

        // Reset time
        float time = 0;

        // If time it takes for marker to fade hasn't passed...
        while (time < fadeDuration)
        {
            // Track time
            time += Time.deltaTime;

            // Change colour
            markerImage.color = Color.Lerp(startColour, endColour, time / fadeDuration);

            yield return null;
        }
    }

    // Allows UpdateMarker to be called from other objects
    public void StartUpdateMarker(Vector2 newPosition)
    {
        StartCoroutine(UpdateMarker(newPosition));
    }
}
