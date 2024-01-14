using System.Collections;
using System.IO;
using UnityEngine;

public class DataSaver : MonoBehaviour
{
    private StreamWriter writer;
    private StreamInfo streamInfo;

    void Start()
    {
        streamInfo = GetComponent<StreamInfo>();
        writer = new StreamWriter("CubeData.txt", true);
        StartCoroutine(SaveDataCoroutine());
    }

    IEnumerator SaveDataCoroutine()
    {
        while (true)
        {
            // Save the current position of the cube at the specified sample rate
            Vector3 position = transform.position;
            string data = $"{Time.time}, {position.x}, {position.y}, {position.z}";
            writer.WriteLine(data);

            // Wait for the next sample period (1/90 seconds)
            yield return new WaitForSeconds(1f / 90f);
        }
    }

    void OnDestroy()
    {
        if (writer != null)
        {
            writer.Close();
        }
    }
}

