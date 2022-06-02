using System;
using UnityEngine;
using UnityEngine.UI;
public class FPSManagment : MonoBehaviour
{

    [SerializeField]
    private Text FPSValueText;

    private void Awake()
    {
        ManomotionManager.OnManoMotionFrameProcessed += DisplayInformationAfterManoMotionProcessFrame;
    }

    /// <summary>
    /// Displays information from the ManoMotion Manager after the frame has been processed.
    /// </summary>
    void DisplayInformationAfterManoMotionProcessFrame()
    {
        UpdateFPSText();
    }

    /// <summary>
    /// Toggles the visibility of a Gameobject.
    /// </summary>
    /// <param name="givenObject">Requires a Gameobject.</param>
    public void ToggleUIElement(GameObject givenObject)
    {
        givenObject.SetActive(!givenObject.activeInHierarchy);
    }

    /// <summary>
    /// Updates the text field with the calculated Frames Per Second value.
    /// </summary>
    public void UpdateFPSText()
    {
        FPSValueText.text = ManomotionManager.Instance.Fps.ToString();
    }

   
}
