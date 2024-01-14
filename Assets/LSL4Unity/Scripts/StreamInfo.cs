using UnityEngine;
using LSL;

public class StreamInfo : MonoBehaviour
{
    private liblsl.StreamOutlet outlet;
    private liblsl.StreamInfo streamInfo;
    private float[] currentSample;

    public string StreamName = "UnityCubePosition";
    public string StreamType = "Unity.Position";
    private const double dataRate = liblsl.IRREGULAR_RATE;

    void Start()
    {
        currentSample = new float[3]; // For 3D position (x, y, z)

        streamInfo = new liblsl.StreamInfo(StreamName, StreamType, 3, dataRate, liblsl.channel_format_t.cf_float32, System.Guid.NewGuid().ToString());
        outlet = new liblsl.StreamOutlet(streamInfo);
    }

    public void SendData(Vector3 position)
    {
        currentSample[0] = position.x;
        currentSample[1] = position.y;
        currentSample[2] = position.z;

        outlet.push_sample(currentSample, liblsl.local_clock());
    }
}
