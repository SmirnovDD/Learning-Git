using UnityEngine;

public class VectorPid
{
    public float pFactor, iFactor, dFactor;

    private float integral;
    private float lastError;

    public VectorPid(float pFactor, float iFactor, float dFactor)
    {
        this.pFactor = pFactor;
        this.iFactor = iFactor;
        this.dFactor = dFactor;
    }

    public float Update(float currentError, float timeFrame)
    {
        integral += currentError * timeFrame;
        var deriv = (currentError - lastError) / timeFrame;
        lastError = currentError;
        return currentError * pFactor
            + integral * iFactor
            + deriv * dFactor;
    }
}
