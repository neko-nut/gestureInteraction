using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddObjects: MonoBehaviour
{

    public GameObject cube;
    public GameObject sphere;
    public GameObject plain;
    public GameObject cat;

    bool hasCube = false;
    bool hasSphere = false;
    bool hasPlain = false;
    bool hasCat = false;

    float distance = 0;
    float size = 1;
    int numberOfObjects = 5;
    GameObject[] objectList;

    [SerializeField]
    Sprite activeFrame, inactiveFrame;
    private Color activeColor;
    public Button cubeButton;
    public Button sphereButton;
    public Button plainButton;
    public Button catButton;

    private Color targetColor;
    private Vector3 targetPosition;
    private GameObject target;

    void Start(){
        objectList = new GameObject[numberOfObjects];
        activeColor = new Color32(61, 87, 127, 255);
        targetColor = new Color32(255, 255, 0, 255);
        targetPosition = new Vector3(0, 0.5f, 1);
        target = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        target.transform.localPosition = targetPosition;
        target.transform.localScale *= 0.1f;
        MeshRenderer render = target.GetComponent<MeshRenderer>();
        render.material.color = targetColor;
    }

    void Update()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            if (objectList[i] != null)
            {
                if (Vector3.Distance(objectList[i].transform.localPosition, targetPosition) < 0.3)
                {
                    Destroy(objectList[i]);
                }
            }
        }
    }

    public void addCube(){
        if (!hasCube){
            hasSphere = false;
            hasPlain = false;
            hasCat = false;
            for (int i = 0; i < numberOfObjects; i++){
                Destroy(objectList[i]);
                objectList[i] = Instantiate(cube, new Vector3((i - 2) * distance, -0.5f, 1), Quaternion.identity);
                objectList[i].transform.localScale *= size;
            }
            hasCube = true;
        } else {
            for(int i = 0; i < numberOfObjects; i++){
                Destroy(objectList[i]);
            }
            hasCube = false;
        }
        UpdateIconAndFrame(cubeButton, hasCube);
        UpdateIconAndFrame(sphereButton, hasSphere);
        UpdateIconAndFrame(plainButton, hasPlain);
        UpdateIconAndFrame(catButton, hasCat);
    }

    public void addSphere(){
        if (!hasSphere){
            hasCube = false;
            hasPlain = false;
            hasCat = false;
            for (int i = 0; i < numberOfObjects; i++){
                Destroy(objectList[i]);
                objectList[i] = Instantiate(sphere, new Vector3((i - 2) * distance, -0.5f, 1), Quaternion.identity);
                objectList[i].transform.localScale *= size;
            }
            hasSphere = true;
        }else{
            for (int i = 0; i < numberOfObjects; i++)
            {
                Destroy(objectList[i]);
            }
            hasSphere = false;
        }
        UpdateIconAndFrame(cubeButton, hasCube);
        UpdateIconAndFrame(sphereButton, hasSphere);
        UpdateIconAndFrame(plainButton, hasPlain);
        UpdateIconAndFrame(catButton, hasCat);
    }

    public void addPlain()
    {
        if (!hasPlain)
        {
            hasCube = false;
            hasSphere = false;
            hasCat = false;
            for (int i = 0; i < numberOfObjects; i++)
            {
                Destroy(objectList[i]);
                objectList[i] = Instantiate(plain, new Vector3((i - 2) * distance, -0.5f, 1), Quaternion.identity);
                objectList[i].transform.localScale *= size;
            }
            hasPlain = true;
        }
        else
        {
            for (int i = 0; i < numberOfObjects; i++)
            {
                Destroy(objectList[i]);
            }
            hasPlain = false;
        }
        UpdateIconAndFrame(cubeButton, hasCube);
        UpdateIconAndFrame(sphereButton, hasSphere);
        UpdateIconAndFrame(plainButton, hasPlain);
        UpdateIconAndFrame(catButton, hasCat);
    }


    public void addCat()
    {
        if (!hasCat)
        {
            hasCube = false;
            hasPlain = false;
            hasSphere = false;
            for (int i = 0; i < numberOfObjects; i++)
            {
                Destroy(objectList[i]);
                objectList[i] = Instantiate(cat, new Vector3((i - 2) * distance, -0.5f, 1), Quaternion.Euler(-90, 90, 0));
                objectList[i].transform.localScale *= size;
            }
            hasCat = true;
        }
        else
        {
            for (int i = 0; i < numberOfObjects; i++)
            {
                Destroy(objectList[i]);
            }
            hasCat = false;
        }
        UpdateIconAndFrame(cubeButton, hasCube);
        UpdateIconAndFrame(sphereButton, hasSphere);
        UpdateIconAndFrame(plainButton, hasPlain);
        UpdateIconAndFrame(catButton, hasCat);
    }


    void updateObjects(){
        if (hasCube)
        {
            for (int i = 0; i < numberOfObjects; i++)
            {
                Destroy(objectList[i]);
                objectList[i] = Instantiate(cube, new Vector3((i - 2) * distance, -0.5f, 1), Quaternion.identity);
                objectList[i].transform.localScale *= size;
            }
        }
        else if (hasSphere)
        {
            for (int i = 0; i < numberOfObjects; i++)
            {
                Destroy(objectList[i]);
                objectList[i] = Instantiate(sphere, new Vector3((i - 2) * distance, -0.5f, 1), Quaternion.identity);
                objectList[i].transform.localScale *= size;
            }
        }
        else if (hasPlain)
        {
            for (int i = 0; i < numberOfObjects; i++)
            {
                Destroy(objectList[i]);
                objectList[i] = Instantiate(plain, new Vector3((i - 2) * distance, -0.5f, 1), Quaternion.identity);
                objectList[i].transform.localScale *= size;
            }
        }
        else if (hasCat)
        {
            for (int i = 0; i < numberOfObjects; i++)
            {
                Destroy(objectList[i]);
                objectList[i] = Instantiate(cat, new Vector3((i - 2) * distance, -0.5f, 1), Quaternion.Euler(-90, 90, 0));
                objectList[i].transform.localScale *= size;
            }
        }
    }

    public void updateDistance(float value){
        distance = value;
        updateObjects();
    }

    public void updateSize(float value){
        size = value;
        updateObjects();
    }



    private void UpdateIconAndFrame(Button button, bool state)
    {
        Image buttonFrame, buttonIcon;
        buttonFrame = button.transform.Find("Frame").GetComponent<Image>();
        buttonIcon = button.transform.Find("Icon").GetComponent<Image>();

        if (!buttonFrame)
        {
            buttonFrame = transform.Find("Frame").GetComponent<Image>();
        }

        if (!buttonIcon)
        {
            buttonIcon = transform.Find("Icon").GetComponent<Image>();
        }

        if (state)
        {
            buttonFrame.sprite = activeFrame;
            buttonIcon.color = activeColor;
        }

        else
        {
            buttonFrame.sprite = inactiveFrame;
            buttonIcon.color = Color.white;
        }
    }

}
