using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SearchAlgorithms : MonoBehaviour
{
    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    public int LinearSearch(int[] numbers, int target) 
    {
        for (int i = 0; i < numbers.Length; i++) {
            if (numbers[i] == target)
                return i;
        }
        return -1;
    }

    public int JumpSearch(int[] numbers, int target) 
    {
        int size = numbers.Length;
        int step = (int)Math.Sqrt(size);
        int prev = 0;
        int curr = step;
        while (numbers[curr - 1] < target) {
            prev = curr;
            curr += step;
            if (curr > size) {
                curr = size;
            }
            if (prev > size) {
                return -1;
            }
        }
        while (numbers[prev] < target) {
            prev++;
            if (prev == curr) {
                return -1;
            }
        }
        if (numbers[prev] == target) {
            return prev;
        }
        return -1;
    }

    public int BinarySearch(int[] numbers, int target)
    {
        int low = 0, high = numbers.Length - 1;
        while (low <= high) {
            int mid = low + (high - low) / 2;
            if (numbers[mid] == target)
                return mid;
            else if (numbers[mid] < target)
                low = mid + 1;
            else
                high = mid - 1;
        }
        return -1;
    }
}
