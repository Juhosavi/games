using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = System.Random;

public class FoodSpawnerScript : MonoBehaviour
{
    Random rnd = new Random();
    public int foodAmount;
    public GameObject foodPrefab;
    public class FoodItem
    {
        public int foodID;
        public Vector2 foodCoordinates;
    }

    // Start is called before the first frame update
    void Start()
    {

        this.transform.position = new Vector2(0, 0);
        // SpawnFood(foodAmount);
    }

    // Update is called once per frame
    void Update()
    {

    }
    // public void SpawnFood(int foodAmount)
    // {
    //     List<FoodItem> foodItemList = new List<FoodItem>();
    //     Vector2 newFoodCoordinates = new Vector2(0, 0);
    //     // Add items with random coordinates to the food list
    //     for (int i = 0; i < foodAmount + 1; i++)
    //     {
    //         foodItemList.Add(new FoodItem() { foodID = i, foodCoordinates = (newFoodCoordinates = new Vector2(newFoodCoordinates.x + rnd.Next(1, 2), newFoodCoordinates.y + rnd.Next(1, 2))) });
    //     }
    //     // Spawn the items on the food list
    //     for (int i = 0; i < foodItemList.Count; i++)
    //     {
    //         Debug.Log(foodItemList.foodID + i);
    //         Instantiate(foodPrefab, foodItemList[i].foodCoordinates);
    //     }
    // }
}
