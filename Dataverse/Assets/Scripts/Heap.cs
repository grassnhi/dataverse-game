using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Heap<T> where T: IHeapItem<T>
{
    T[] items;
    int currentItemCount;
    public Heap(int maxSize) {
        items = new T[maxSize];
    }
    public void Add(T item) {
        item.HeapIndex = currentItemCount;
        items[currentItemCount] = item;
        SortUp(item);
        currentItemCount++;
    }
    public T RemoveFirst() {
        T firstItem = items[0];
        currentItemCount--;
        items[0] = items[currentItemCount];
        items[0].HeapIndex = 0;
        SortDown(items[0]);
        return firstItem;
    }
    public void UpdateItem(T item) {
        SortUp(item);
    }
    public void Clear() {
        currentItemCount = 0;
    }
    public int Count {
        get {
            return currentItemCount;
        }
    }
    public bool Contains(T item) {
        if (item.HeapIndex < currentItemCount) return Equals(items[item.HeapIndex], item);
        else return false;
    }
    void SortDown(T item) {
        while (true) {
            int leftIdx = item.HeapIndex * 2 + 1;
            int rightIdx = item.HeapIndex * 2 + 2;
            int swapIndex = 0;
            if (leftIdx < currentItemCount) {
                swapIndex = leftIdx;
                if (rightIdx < currentItemCount) {
                    if (items[leftIdx].CompareTo(items[rightIdx]) < 0) {
                        swapIndex = rightIdx;
                    }
                }
                if (item.CompareTo(items[swapIndex]) < 0) {
                    Swap(item, items[swapIndex]);
                }
                else {
                    return;
                }
            }
            else {
                return;
            }
        }
    }
    void SortUp(T item) {
        int parentIndex = (item.HeapIndex - 1) / 2;
        while (true) {
            T parentItem = items[parentIndex];
            if (item.CompareTo(parentItem) > 0) {
                Swap(item, parentItem);
            }
            else {
                break;
            }
        }
    }
    void Swap(T item1, T item2) {
        items[item1.HeapIndex] = item2;
        items[item2.HeapIndex] = item1;
        int tmp = item1.HeapIndex;
        item1.HeapIndex = item2.HeapIndex;
        item2.HeapIndex = tmp;
    }
}

public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex { get; set; }
}
