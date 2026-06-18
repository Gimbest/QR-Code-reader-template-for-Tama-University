using Meta.XR.MRUtilityKit;
using UnityEngine;

public class QrCodeScanner : MonoBehaviour
{
    public GameObject objectToSpawn;
    void Start()
    {
        MRUK.Instance.SceneSettings.TrackableAdded.AddListener(OnTrackableAdded);
        MRUK.Instance.SceneSettings.TrackableRemoved.AddListener(OnTrackableRemoved);
    }
    /// <summary>
    /// Function called when a trackable is seen by the headset. We make sure that the trackable really is a QR Code
    /// </summary>
    /// <param name="qrcode">The QR Code tracked</param>
    public void OnTrackableAdded(MRUKTrackable qrcode)
    {
        if(qrcode.TrackableType != OVRAnchor.TrackableType.QRCode)return;
        Debug.Log("QRCodeReader | Trackable Added");

        Vector3 targetPosition = qrcode.transform.position;
        //Necessary to have the checkmark on the right side of the QR Code 
        Quaternion targetRotation = Quaternion.LookRotation(-qrcode.transform.forward, qrcode.transform.up);
        GameObject spawned = Instantiate(objectToSpawn, targetPosition, targetRotation);

        float width = qrcode.PlaneRect.Value.width;
        float height = qrcode.PlaneRect.Value.height;

        Vector3 targetScale = new(width, height, 0);
        spawned.transform.localScale = targetScale;
        spawned.transform.parent = qrcode.transform;

        //Displays the Qr Code's payload in the Unity console, feel free to replace with your logic
        string payloadString = qrcode.MarkerPayloadString;
        
        Debug.Log(payloadString);
    }
    /// <summary>
    /// Function called when a trackable is removed
    /// </summary>
    /// <param name="qrcode">The QR Code tracked</param>
    public void OnTrackableRemoved(MRUKTrackable trackable)
    {
        //Feel free to change the logic of the function
        Debug.Log("QRCodeReader | Trackable Removed");
    }

}