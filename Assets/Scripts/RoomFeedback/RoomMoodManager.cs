using UnityEngine;

public class RoomMoodManager : MonoBehaviour {
    [SerializeField] private Light lamp;                       // lamp light
    [SerializeField] private Light spotLight;                 // suspect spotlight
    [SerializeField] private Light entranceLight;                // suspect entrance light
    [SerializeField] private Material backWall;             // wall behind the suspect
    [SerializeField] private float baseLightIntensity = 1f;

    void Start() {
        // Clone the material so changes only affect this scene instance (almost missed this lol)
        backWall = new Material(backWall);
    }

    // to be called when ShiftManager ends a shift
    public void UpdateEnvironment(int incorrectCount, int correctCount) {
        int value = incorrectCount - correctCount;  // negative if doing well
        float affinity = Mathf.Max(value, 0f);     // update the negative values to 0

        // Change light colour to red based on how many mistakes were made
        lamp.color = Color.Lerp(Color.white, Color.red, affinity * 0.1f); 

        entranceLight.color = Color.Lerp(Color.white, Color.red, affinity * 0.1f);

        spotLight.color = Color.Lerp(Color.white, Color.red, affinity * 0.1f);

        // Increase wall bumpiness (normal map intensity) to make it look weird af
        if (backWall.HasProperty("_BumpScale")) {
            backWall.SetFloat("_BumpScale", Mathf.Clamp(affinity * 0.2f, 0f, 1f));
        }

        // adjust the suspects spotlight to make it brighter and the colour to be more red
        if (spotLight != null) {
            spotLight.intensity = Mathf.Clamp(1f + (affinity * 0.2f), 1f, 2f);
        }

        
    }
}