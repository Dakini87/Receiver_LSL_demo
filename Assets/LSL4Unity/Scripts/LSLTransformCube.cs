using UnityEngine;
using LSL;

public class LSLTransformCube : MonoBehaviour
{
    private liblsl.StreamInlet inlet;
    private float[] sample;
    public GameObject[] targetObjects; // Array of GameObjects to be controlled

    void Start()
    {
        // Resolve the stream by the unique name set in the sender script
        var streamInfos = liblsl.resolve_stream("name", "UnityCubePosition"); // Adjusted to correct name
        if (streamInfos.Length > 0)
        {
            inlet = new liblsl.StreamInlet(streamInfos[0]);
            sample = new float[3];
            Debug.Log("LSL stream found and connected.");
        }
        else
        {
            Debug.LogError("No LSL stream found with the name 'UnityCubePosition'.");
        }
    }

    void Update()
    {
        if (inlet != null && inlet.pull_sample(sample, 0.01f) > 0)
        {
            // Debug log to show the data being received
            Debug.Log("Received position: " + sample[0] + ", " + sample[1] + ", " + sample[2]);
            {
                foreach (var obj in targetObjects)
                {
                    obj.transform.position = new Vector3(sample[0], sample[1], sample[2]);
                }
            }
        }
    }
}
