using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gradient : MonoBehaviour
{
    public Camera cam;

    [SerializeField] Color dawn;
    [SerializeField] Color day;
    [SerializeField] Color dusk;
    [SerializeField] Color night;
    
    private float dayLength = 240f;
    private float currentTime = 0f;
    private float quarterLength;
    private Quarter currentQuarter;

    private enum Quarter
    {
        afterDawn,
        afterNoon,
        afterDusk,
        afterMidnight
    }

    private void Start()
    {
        quarterLength = dayLength / 4;
        currentQuarter = Quarter.afterNoon;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > quarterLength)
            currentQuarter = ChangeQuarter(currentQuarter);

        if (currentQuarter.Equals(Quarter.afterNoon))
            cam.backgroundColor = Color.Lerp(dawn, day, currentTime / quarterLength);
        else if (currentQuarter.Equals(Quarter.afterDusk))
            cam.backgroundColor = Color.Lerp(day, dusk, currentTime / quarterLength);
        else if (currentQuarter.Equals(Quarter.afterMidnight))
            cam.backgroundColor = Color.Lerp(dusk, night, currentTime / quarterLength);
        else
            cam.backgroundColor = Color.Lerp(night, dawn, currentTime / quarterLength);
    }

    private Quarter ChangeQuarter(Quarter current)
    {
        currentTime = 0f;
        switch (current)
        {
            case Quarter.afterDawn:
                return Quarter.afterNoon;
            case Quarter.afterNoon:
                return Quarter.afterDusk;
            case Quarter.afterDusk:
                return Quarter.afterMidnight;
            default:
                return Quarter.afterDawn;
        }
    }
}
