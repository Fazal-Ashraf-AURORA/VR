using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    public bool showController = false;
    public InputDeviceCharacteristics controllerCharacteristics;
    public List<GameObject> controllerPrefabs;
    public GameObject handModelPrefab;

    private InputDevice targetDevice;
    private GameObject spawnController;
    private GameObject spawnHandModel;
    private Animator handAnimator;

    // Start is called before the first frame update
    void Start()
    {
        TryInitialize();

        //List<InputDevice> devices = new List<InputDevice>();

        //InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        //foreach (var item in devices) {
        //    Debug.Log(item.name);
        //}

        //if (devices.Count > 0) {
        //    targetDevice = devices[0];
        //    GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
        //    if (prefab) {
        //        spawnController = Instantiate(prefab, transform);
        //        Debug.Log("Boom");
        //    } else {
        //        Debug.Log("Did not find corresponding controller model");
        //        spawnController = Instantiate(controllerPrefabs[0], transform);
        //    }

        //    spawnHandModel = Instantiate(handModelPrefab, transform);
        //    handAnimator = spawnHandModel.GetComponent<Animator>();
        //}
    }


    void UpdateHandAnimation() {
        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue)) {
            handAnimator.SetFloat("Trigger", triggerValue);
        } else {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue)) {
            handAnimator.SetFloat("Grip", gripValue);
        } else {
            handAnimator.SetFloat("Grip", 0);
        }
    }

    void TryInitialize() {
        List<InputDevice> devices = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        foreach (var item in devices) {
            Debug.Log(item.name);
        }

        if (devices.Count > 0) {
            targetDevice = devices[0];
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            if (prefab) {
                spawnController = Instantiate(prefab, transform);
                Debug.Log("Boom");
            } else {
                Debug.Log("Did not find corresponding controller model");
                spawnController = Instantiate(controllerPrefabs[0], transform);
            }

            spawnHandModel = Instantiate(handModelPrefab, transform);
            handAnimator = spawnHandModel.GetComponent<Animator>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        //if(targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue )
        //    Debug.Log("Pressing primary button");

        //if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) &&triggerValue > 0.1f) 
        //    Debug.Log("Trigger pressed" + triggerValue);

        //if(targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue) && primary2DAxisValue != Vector2.zero)
        //    Debug.Log("Primary Touchpad" + primary2DAxisValue);

        if (!targetDevice.isValid) {
            TryInitialize();
        } else {
            if (showController) {
                spawnHandModel.SetActive(false);
                spawnController.SetActive(true);
            } else {
                spawnHandModel.SetActive(true);
                spawnController.SetActive(false);
                UpdateHandAnimation();
            }

        }

       

    }
}
