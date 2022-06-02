using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class MyGizmoManager : MonoBehaviour
{
    #region Singleton

    private static MyGizmoManager _instance;
    public static MyGizmoManager Instance
    {
        get
        {
            return _instance;
        }

        set
        {
            _instance = value;
        }
    }

    #endregion


    public Color disabledStateColor;

    [SerializeField]
    private Image[] stateImages;

    [SerializeField]
    private GameObject handStatesGizmo;

    [SerializeField]
    private GameObject manoClassGizmo;

    [SerializeField]
    private GameObject triggerTextPrefab;

    [SerializeField]
    private GameObject depthEstimationGizmo;

    [SerializeField]
    private bool _showHandStates, _showManoClass, _showGrabTriggerGesture, _showReleaseTriggerGesture, _showDepthEstimation;

    private GameObject topFlag, leftFlag, rightFlag;
    private Text manoClassText;
    private TextMeshProUGUI depthEstimationValue;
    private Image depthFillAmmount;

    #region Properties


    public bool ShowManoClass
    {
        get
        {
            return _showManoClass;
        }

        set
        {
            _showManoClass = value;
        }
    }


    public bool ShowHandStates
    {
        get
        {
            return _showHandStates;
        }

        set
        {
            _showHandStates = value;
        }
    }


    public bool ShowGrabTriggerGesture
    {
        get
        {
            return _showGrabTriggerGesture;
        }

        set
        {
            _showGrabTriggerGesture = value;
        }
    }

    public bool ShowReleaseTriggerGesture
    {
        get
        {
            return _showReleaseTriggerGesture;
        }

        set
        {
            _showReleaseTriggerGesture = value;
        }
    }

    public bool ShowDepthEstimation
    {
        get
        {
            return _showDepthEstimation;
        }
        set
        {
            _showDepthEstimation = value;
        }
    }

    #endregion

    void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        Initialize();
    }

    private void Initialize()
    {
        SetGestureDescriptionParts();
        HighlightStatesToStateDetection(0);
        InitializeTriggerPool();
        ManomotionManager.OnManoMotionFrameProcessed += DisplayInformationAfterManoMotionProcessFrame;
        ShowDepthEstimation = true;
        //ShowManoClass = true;
        //ShowHandStates = true;
        _showGrabTriggerGesture = true;
        _showReleaseTriggerGesture = true;
    }

    /// <summary>
    /// Visualizes information from the ManoMotion Manager after the frame has been processed.
    /// </summary>
    void DisplayInformationAfterManoMotionProcessFrame()
    {
        GestureInfo gestureInfo = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info;
        TrackingInfo trackingInfo = ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info;
        Warning warning = ManomotionManager.Instance.Hand_infos[0].hand_info.warning;
        Session session = ManomotionManager.Instance.Manomotion_Session;

        DisplayManoclass(gestureInfo.mano_class);
        DisplayTriggerGesture(gestureInfo.mano_gesture_trigger, trackingInfo);
        DisplayHandState(gestureInfo.state);
        DisplayDepthEstimation(trackingInfo.depth_estimation);
    }

    #region Display Methods

    /// <summary>
    /// Displays the depth estimation of the detected hand.
    /// </summary>
    /// <param name="depthEstimation">Requires the float value of depth estimation.</param>
    void DisplayDepthEstimation(float depthEstimation)
    {
        depthEstimationGizmo.SetActive(ShowDepthEstimation);

        if (!depthEstimationValue)
        {
            depthEstimationValue = depthEstimationGizmo.transform.Find("DepthValue").gameObject.GetComponent<TextMeshProUGUI>();
        }
        if (!depthFillAmmount)
        {
            depthFillAmmount = depthEstimationGizmo.transform.Find("CurrentLevel").gameObject.GetComponent<Image>();
        }
        if (ShowDepthEstimation)
        {
            depthEstimationValue.text = depthEstimation.ToString("F2");
            depthFillAmmount.fillAmount = depthEstimation;
        }
    }


    /// <summary>
    /// Displays information regarding the detected manoclass
    /// </summary>
    /// <param name="manoclass">Manoclass.</param>
    void DisplayManoclass(ManoClass manoclass)
    {
        manoClassGizmo.SetActive(ShowManoClass);
        if (ShowManoClass)
        {
            switch (manoclass)
            {
                case ManoClass.NO_HAND:
                    manoClassText.text = "Manoclass: No Hand";
                    break;
                case ManoClass.GRAB_GESTURE:
                    manoClassText.text = "Manoclass: Grab Class";
                    break;
                case ManoClass.PINCH_GESTURE:
                    manoClassText.text = "Manoclass: Pinch Class";
                    break;
                case ManoClass.POINTER_GESTURE:
                    manoClassText.text = "Manoclass: Pointer Class";
                    break;
                default:
                    manoClassText.text = "Manoclass: No Hand";
                    break;
            }
        }
    }

   

    ///// <summary>
    ///// Updates the visual information that showcases the hand state (how open/closed) it is
    ///// </summary>
    ///// <param name="gesture_info"></param>
    void DisplayHandState(int handstate)
    {
        handStatesGizmo.SetActive(ShowHandStates);
        if (ShowHandStates)
        {
            HighlightStatesToStateDetection(handstate);
        }
    }

    ManoGestureTrigger previousTrigger;
    /// <summary>
    /// Display Visual information of the detected trigger gesture.
    /// In the case where a click is intended (Open pinch, Closed Pinch, Open Pinch) we are clearing out the visual information that are generated from the pick/drop
    /// </summary>
    /// <param name="triggerGesture">Requires an input of trigger gesture.</param>
    void DisplayTriggerGesture(ManoGestureTrigger triggerGesture, TrackingInfo trackingInfo)
    {
        if (triggerGesture != ManoGestureTrigger.NO_GESTURE)
        {
            if (_showGrabTriggerGesture)
            {
                if (triggerGesture == ManoGestureTrigger.GRAB_GESTURE)
                    TriggerDisplay(trackingInfo, ManoGestureTrigger.GRAB_GESTURE);
            }
            if (_showReleaseTriggerGesture)
            {
                if (triggerGesture == ManoGestureTrigger.RELEASE_GESTURE)
                    TriggerDisplay(trackingInfo, ManoGestureTrigger.RELEASE_GESTURE);
            }
        }
        previousTrigger = triggerGesture;
    }

    public List<GameObject> triggerObjectPool = new List<GameObject>();
    public int amountToPool = 20;

    /// <summary>
    /// Initializes the object pool for trigger gestures.
    /// </summary>
    private void InitializeTriggerPool()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject newTriggerObject = Instantiate(triggerTextPrefab);
            newTriggerObject.transform.SetParent(transform);
            newTriggerObject.SetActive(false);
            triggerObjectPool.Add(newTriggerObject);
        }
    }

    /// <summary>
    /// Gets the current pooled trigger object.
    /// </summary>
    /// <returns>The current pooled trigger.</returns>
    private GameObject GetCurrentPooledTrigger()
    {
        for (int i = 0; i < triggerObjectPool.Count; i++)
        {
            if (!triggerObjectPool[i].activeInHierarchy)
            {
                return triggerObjectPool[i];
            }
        }
        return null;
    }

    /// <summary>
    /// Displays the visual information of the performed trigger gesture.
    /// </summary>
    /// <param name="triggerGesture">Trigger gesture.</param>
    void TriggerDisplay(TrackingInfo trackingInfo, ManoGestureTrigger triggerGesture)
    {
        if (GetCurrentPooledTrigger())
        {
            GameObject triggerVisualInformation = GetCurrentPooledTrigger();

            triggerVisualInformation.SetActive(true);
            triggerVisualInformation.name = triggerGesture.ToString();
            triggerVisualInformation.GetComponent<TriggerGizmo>().InitializeTriggerGizmo(triggerGesture);
            triggerVisualInformation.GetComponent<RectTransform>().position = Camera.main.ViewportToScreenPoint(trackingInfo.palm_center);
        }
    }

    /// <summary>
    /// Visualizes the current hand state by coloring white the images up to that value and turning grey the rest
    /// </summary>
    /// <param name="stateValue">Requires a hand state value to assign the colors accordingly </param>
    void HighlightStatesToStateDetection(int stateValue)
    {
        for (int i = 0; i < stateImages.Length; i++)
        {
            if (i > stateValue)
            {
                stateImages[i].color = disabledStateColor;
            }
            else
            {
                stateImages[i].color = Color.white;
            }
        }
    }


    /// <summary>
    /// Initializes the components of the Manoclass,Continuous Gesture and Trigger Gesture Gizmos
    /// </summary>
    void SetGestureDescriptionParts()
    {
        manoClassText = manoClassGizmo.transform.Find("Description").GetComponent<Text>();
    }

    #endregion


   
}