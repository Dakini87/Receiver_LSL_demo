using UnityEngine;
using LSL;
using System.Collections;

public class LSLTransformOutlet : MonoBehaviour
{
    private liblsl.StreamOutlet outlet;
    private liblsl.StreamInfo streamInfo;
    private float[] currentSample;
    // Reference to the cube that will be streamed
    public Transform sampleSource; 
    public string StreamName = "UnityCubePosition";
    public string StreamType = "Unity.Position";
    // 90Hz sampling rate
    private const double dataRate = 90; 

    void Start()
    {
        // For 3D position (x, y, z)
        currentSample = new float[3]; 
        streamInfo = new liblsl.StreamInfo(StreamName, StreamType, 3, dataRate, liblsl.channel_format_t.cf_float32, System.Guid.NewGuid().ToString());
        outlet = new liblsl.StreamOutlet(streamInfo);

        StartCoroutine(SendDataAtRate());
    }

    private IEnumerator SendDataAtRate()
    {
        while (true)
        {
            if (sampleSource != null)
            {
                Vector3 position = sampleSource.position;
                currentSample[0] = position.x;
                currentSample[1] = position.y;
                currentSample[2] = position.z;

                outlet.push_sample(currentSample, liblsl.local_clock());
            }
            // Wait for 1/90th of a second
            yield return new WaitForSeconds((float)(1f / dataRate)); 

        }
    }
}
