using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class InputListener : MonoBehaviour
{
    List<InputDevice> inputDevices;
    // Start is called before the first frame update
    InputDeviceCharacteristics deviceCharacteristics;
    public XRNode controllerNode;
    private void Awake()
    {
        inputDevices = new List<InputDevice>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //InputDevices.GetDevices(inputDevices);
        deviceCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left;
        //InputDevices.GetDevicesWithCharacteristics(deviceCharacteristics, inputDevices);
        InputDevices.GetDevicesAtXRNode(controllerNode, inputDevices);

        foreach (InputDevice inputDevice in inputDevices)
        {
            Debug.Log("Device found with name:" + inputDevice.name);
            bool inputValue;
            if (inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out inputValue) && inputValue)
            {
                Debug.Log("You pressed the Primary Button");
            }
        }
    }
}
