using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    Vector2Int coords;
    int value;
    public Item(Vector2Int coords, int value) {
        this.coords = coords;
        this.value = value;
    }
}
