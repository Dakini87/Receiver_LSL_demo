using UnityEngine;

public class ControlCube : MonoBehaviour
{
    private StreamInfo streamInfo;

    void Start()
    {
        streamInfo = GetComponent<StreamInfo>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float h = 2.0f * Input.GetAxis("Mouse X");
            float v = 2.0f * Input.GetAxis("Mouse Y");
            transform.Rotate(v, h, 0);

            // Send the current position data to LSL
            streamInfo.SendData(transform.position);
        }
    }
}

