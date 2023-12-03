using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomProvider : IRandomProvider
{
    public float Value => Random.value;
    public Vector2 InsideCircle => Random.insideUnitCircle;
    public Vector3 InsideSphere => Random.insideUnitSphere;

    public Color Color
    {
        get
        {
            Color randomColor = Random.ColorHSV();
            randomColor.a = 1f;
            return randomColor;
        }
    }

    /// <summary>
    /// Max is exclusive.
    /// </summary>
    public int Range (int min, int max)
    {
        return Random.Range(min, max);
    }

    /// <summary>
    /// Max is inclusive.
    /// </summary>
    public float Range (float min, float max)
    {
        return Random.Range(min, max);
    }

    public Vector2 Range (Vector2 min, Vector2 max)
    {
        return new Vector2(Range(min.x, max.x), Range(min.y, max.y));
    }

    public Vector3 Range (Vector3 min, Vector3 max)
    {
        return new Vector3(Range(min.x, max.x), Range(min.y, max.y), Range(min.z, max.z));
    }

    public T WeightedRandom<T> (List<WeightedObject<T>> weightedList)
    {
        //TODO: check for optimization possibilities
        float totalWeight = 0;
        foreach (WeightedObject<T> weightObject in weightedList)
            totalWeight += weightObject.Weight;

        if (totalWeight <= 0)
            throw new InvalidOperationException("Total weight in the submitted list must be greater than zero.");
        
        float randomNumber = Range(0, totalWeight);
        float summedWeight = 0;
        
        foreach (WeightedObject<T> weightObject in weightedList)
        {
            summedWeight += weightObject.Weight;
            if (randomNumber <= summedWeight)
            {
                return weightObject.Obj;
            }
        }
        
        throw new InvalidOperationException("Weighted random selection failed.");
    }
}

public class WeightedObject<T>
{
    public T Obj;
    public float Weight;
    
    public WeightedObject (T obj, float weight)
    {
        Obj = obj;
        Weight = weight;
    }
}