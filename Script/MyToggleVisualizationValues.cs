using UnityEngine;
using System;
using UnityEngine.UI;

public class MyToggleVisualizationValues : MonoBehaviour
{
    public static Action<bool> OnShowBoundingBoxValueChanged;

    private ManoVisualization _manoVisualization;

    private void Start()
    {
        _manoVisualization = GetComponent<ManoVisualization>();
        _manoVisualization.Show_bounding_box = true;
    }

    /// <summary>
    /// Toggles the boolean value for showing the bounding box.
    /// </summary>
    public void ToggleBoundingBox()
    {
        _manoVisualization.Show_bounding_box = !_manoVisualization.Show_bounding_box;
        if (OnShowBoundingBoxValueChanged != null)
        {
            OnShowBoundingBoxValueChanged(_manoVisualization.Show_bounding_box);
        }
    }

}
