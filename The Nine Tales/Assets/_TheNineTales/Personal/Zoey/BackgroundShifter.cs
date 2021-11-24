using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundShifter : MonoBehaviour
{

    public Vector3 transitionStartPos;
    public Vector3 transitionEndPos;

    // stores all potential transition values
    public Vector4[] backgroundRendererEndValues = new Vector4[3];
    public Vector4[] backgroundCloudRendererEndValues = new Vector4[3];
    public Vector4[] foregroundCloudRendererEndValues = new Vector4[3];

    // stores all background starting values
    public Vector4 backgroundRendererStartValues;
    public Vector4 backgroundCloudRendererStartValues;
    public Vector4 foregroundCloudRendererStartValues;

    public float[] bulbOpacity = new float[3];
    public Vector4[] backgroundColors = new Vector4[3];

    // store whether or not we are tansitioning between color palletes
    public bool transitioning = false;
    public float transitionValue;
    public int targetColorPalette;
    // track the bulbs we are swapping between
    public int targetBulb;
    public int startingBulb;
    public int previousBulb;

    bool finalTransition;

    // initialize the background start state
    private void Start()
    {
        targetColorPalette = 1;
        targetBulb = 1;
        transitionValue = 1;
        backgroundColors[0] = GetBackgroundColor(transitionValue);
        backgroundColors[1] = GetBackgroundCloudColor(transitionValue);
        backgroundColors[2] = GetForegroundCloudColor(transitionValue);
        SetBulbOpacities();
    }

    // Update is called once per frame
    void Update()
    {
        if(transitioning || finalTransition)
        {
            if(!finalTransition)
            {
                CalculateTransitionValue();
            } else
            {
                finalTransition = false;
            }
            
            backgroundColors[0] = GetBackgroundColor(transitionValue);
            backgroundColors[1] = GetBackgroundCloudColor(transitionValue);
            backgroundColors[2] = GetForegroundCloudColor(transitionValue);
            SetBulbOpacities();
        }
    }

    // calculate transition values
    public void CalculateTransitionValue()
    {
        // stores the distance between the two threshold values
        float thresholdSepration = Vector3.Distance(transitionStartPos, transitionEndPos);

        // calculate the decimal value of the transition
        transitionValue = 1 - ((Vector3.Distance(transform.position, transitionEndPos)) / thresholdSepration);
    }

    // scales the background renderers color values
    public Color GetBackgroundColor(float percentComplete)
    {
        Color color = new Color();

        color.r = Mathf.Lerp(backgroundRendererStartValues.x, backgroundRendererEndValues[targetColorPalette].x, transitionValue);

        color.g = Mathf.Lerp(backgroundRendererStartValues.y, backgroundRendererEndValues[targetColorPalette].y, transitionValue);

        color.b = Mathf.Lerp(backgroundRendererStartValues.z, backgroundRendererEndValues[targetColorPalette].z, transitionValue);

        color.a = 255;

        return color;
    }

    // scales the foreground renderers color values after calculating the proper value to set it to
    public Color GetBackgroundCloudColor(float percentComplete)
    {
        Color color = new Color();

        color.r = Mathf.Lerp(backgroundCloudRendererStartValues.x, backgroundCloudRendererEndValues[targetColorPalette].x, transitionValue);

        color.g = Mathf.Lerp(backgroundCloudRendererStartValues.y, backgroundCloudRendererEndValues[targetColorPalette].y, transitionValue);

        color.b = Mathf.Lerp(backgroundCloudRendererStartValues.z, backgroundCloudRendererEndValues[targetColorPalette].z, transitionValue);

        color.a = 255;

        return color;
        
    }

    // scales the foreground renderers color values after calculating the proper value to set it to
    public Color GetForegroundCloudColor(float percentComplete)
    {

        Color color = new Color();

        color.r = Mathf.Lerp(foregroundCloudRendererStartValues.x, foregroundCloudRendererEndValues[targetColorPalette].x, transitionValue);

        color.g = Mathf.Lerp(foregroundCloudRendererStartValues.y, foregroundCloudRendererEndValues[targetColorPalette].y, transitionValue);

        color.b = Mathf.Lerp(foregroundCloudRendererStartValues.z, foregroundCloudRendererEndValues[targetColorPalette].z, transitionValue);

        color.a = 255;

        return color;
    }

    // swaps the bulbs after reaching a opacity threshold
    public void SetBulbOpacities()
    {
        for(int i = 0; i < bulbOpacity.Length; i++)
        {
            if(i == targetBulb)
            {
                bulbOpacity[i] = transitionValue;
            } else if( i == previousBulb)
            {
                bulbOpacity[i] = 1 - transitionValue;
            } else
            {
                bulbOpacity[i] = 0; 
            }
        }

    }

    // finishes the background transition
    public void CompleteTransition()
    {
        if(transitionValue > .5)
        {
            transitionValue = 1;
        } else
        {
            transitionValue = 0;
        }
    }

    // store starting values
    public void storeStartingValues(int threshold)
    {
        backgroundRendererStartValues = backgroundColors[0];
        backgroundCloudRendererStartValues = backgroundColors[1];
        foregroundCloudRendererStartValues = backgroundColors[2];

    }

    // processes the threshold data for this transition
    public void ProcessThresholdData(ThresholdContainer thresholdData)
    {
        // store the correct end threshold for the player
        if(Vector3.Distance(thresholdData.threshold1.transform.position, transform.position) > Vector3.Distance(thresholdData.threshold2.transform.position, transform.position))
        {
            Debug.Log("threshold1 selected");

            transitionEndPos = thresholdData.threshold1.transform.position;

            // store the transition storage node inside othe local y rotation of the threshold node
            targetColorPalette = thresholdData.threshold1TargetPallete;

            previousBulb = targetBulb;
            targetBulb = thresholdData.threshold1TargetBulb;

            // store the starting values for our color lerp
            storeStartingValues(thresholdData.threshold2TargetPallete);

            transitionStartPos = transform.position;
        } else
        {

            Debug.Log("threshold2 selected");

            transitionEndPos = thresholdData.threshold2.transform.position;

            // store the transition storage node inside othe local y rotation of the threshold node
            targetColorPalette = thresholdData.threshold2TargetPallete;

            previousBulb = targetBulb;
            targetBulb = thresholdData.threshold2TargetBulb;

            // store the starting values for our color lerp
            storeStartingValues(thresholdData.threshold1TargetPallete);

            transitionStartPos = transform.position;
        }

    }

    // detects the end of a transition
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // process the collision data
        ProcessThresholdData(collision.gameObject.GetComponent<ThresholdContainer>());
        transitioning = true;
    }

    // detects the end of a transition 
    private void OnTriggerExit2D(Collider2D other)
    {
        finalTransition = true;
        transitioning = false;
        CompleteTransition();
    }
}
