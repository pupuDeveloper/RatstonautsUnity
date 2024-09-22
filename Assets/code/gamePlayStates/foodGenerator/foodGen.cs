using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class foodGen : MonoBehaviour
{
    [SerializeField] private Transform selectedFoodDisplay;
    public food foodInSpot;
    private food blank;
    public List<food> allFoods = new List<food>();
    public List<food> unlockedFoods { get; private set; }
    [SerializeField] private GameObject[] foodsUI;
    public GameObject scrollableList;
    public GameObject closeFoodListButton;


    private void Start()
    {
        unlockedFoods = new List<food>();

        food asteroidCheese = new food("Asteroid Cheese", 1, "Not as good as the real thing, but it works. Tiny 1% xp boost to all room effects.", true, false);
        if (!allFoods.Contains(asteroidCheese)) allFoods.Add(asteroidCheese);

        food friedEgg = new food("Sunny-Side-Up Egg", 2, "Write buff here.", false, false);
        if (!allFoods.Contains(friedEgg)) allFoods.Add(friedEgg);

        //blank food

        blank = new food("Blank", 0,"Blank food", false, false);
        if (!allFoods.Contains(blank)) allFoods.Add(blank);
        foodInSpot = blank;


        foreach (food f in allFoods)
        {
            foreach (GameObject g in foodsUI)
            {
                string name = f.name.ToLower();
                name = name.Trim();
                string name2 = g.name.ToLower();
                name2 = name2.Trim();
                if (name == name2)
                {
                    if (f.isUnlocked == false)
                        g.transform.GetChild(3).gameObject.SetActive(true);
                    else
                        g.transform.GetChild(3).gameObject.SetActive(false);
                }
                else
                {
                    Debug.LogWarning("food name did not match gameobject!");
                }
            }
        }
    }

    public void addButton()
    {
        food foodTobeAdded = blank;

        foreach (food f in allFoods)
        {
            string foodName = f.name.ToLower();
            foodName = foodName.Trim();
            string selectedFoodName = EventSystem.current.currentSelectedGameObject.transform.parent.name.ToLower();
            selectedFoodName = selectedFoodName.Trim();

            if (selectedFoodName == foodName)
            {
                foodTobeAdded = f;
                addFood(foodTobeAdded);
                break;
            }
        }
    }

    public void addFood(food addedFood)
    {
        Debug.Log("called");
        if (foodInSpot == addedFood)
        {
            foodInSpot = blank;
            Debug.Log("Food removed");
            selectedFoodDisplay.GetChild(0).transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = "";
            return;
        }

        if (foodInSpot == blank || foodInSpot != addedFood)
        {
            foodInSpot = addedFood;
            selectedFoodDisplay.GetChild(0).transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = addedFood.name;
        }
    }
}
