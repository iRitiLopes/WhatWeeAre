using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class RecipesDisplay : MonoBehaviour {
    public GameObject slot;
    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    public List<PowerUpEffect> powerUpEffects = new();

    private IEnumerator Start() {
        yield return new WaitUntil(() => ItemDatabase.instance != null);
        this.loadItems();
        this.loadPowerUpsRecipes();
    }

    public void loadItems() {
        foreach (var item in ItemDatabase.instance.ItensWithRaw()) {
            var recipe = Instantiate(slot, transform, false);
            var recipe1 = recipe.transform.Find("recipe_1");
            var recipe2 = recipe.transform.Find("recipe_2");
            var recipe3 = recipe.transform.Find("recipe_3");
            var recipe_result = recipe.transform.Find("recipe_result");

            var raw1 = item.RawItems.ElementAtOrDefault(0);
            var raw2 = item.RawItems.ElementAtOrDefault(1);
            var raw3 = item.RawItems.ElementAtOrDefault(2);

            if (raw1 != null) {
                showOnRecipe(ItemDatabase.findItem(raw1.id), raw1.quantity, recipe1);
            }else{
                recipe1.gameObject.SetActive(false);
            }

            if(raw2 != null){
                showOnRecipe(ItemDatabase.findItem(raw2.id), raw2.quantity, recipe2);
            }else{
                recipe2.gameObject.SetActive(false);
            }

            if(raw3 != null){
                showOnRecipe(ItemDatabase.findItem(raw3.id), raw3.quantity, recipe3);
            }else{
                recipe3.gameObject.SetActive(false);
            }

            showOnRecipe(item, 1, recipe_result);
        }
    }
    
    public void loadPowerUpsRecipes(){
        foreach (var powerup in powerUpEffects) {
            var recipe = Instantiate(slot, transform, false);
            var recipe1 = recipe.transform.Find("recipe_1");
            var recipe2 = recipe.transform.Find("recipe_2");
            var recipe3 = recipe.transform.Find("recipe_3");
            var recipe_result = recipe.transform.Find("recipe_result");

            var raw1 = powerup.rawItems.ElementAtOrDefault(0);
            var raw2 = powerup.rawItems.ElementAtOrDefault(1);
            var raw3 = powerup.rawItems.ElementAtOrDefault(2);

            if (raw1 != null) {
                showOnRecipe(ItemDatabase.findItem(raw1.id), raw1.quantity, recipe1);
            }else{
                recipe1.gameObject.SetActive(false);
            }

            if(raw2 != null){
                showOnRecipe(ItemDatabase.findItem(raw2.id), raw2.quantity, recipe2);
            }else{
                recipe2.gameObject.SetActive(false);
            }

            if(raw3 != null){
                showOnRecipe(ItemDatabase.findItem(raw3.id), raw3.quantity, recipe3);
            }else{
                recipe3.gameObject.SetActive(false);
            }

            showOnRecipe(powerup, 1, recipe_result);
        }
    }

    private void showOnRecipe(Item item, int quantity, Transform recipe) {
        var recipeSlot = recipe.gameObject.GetComponent<RecipeSlot>();
        recipeSlot.item = item;
        recipeSlot.quantity = quantity;
        var iSlot = recipe.Find("ItemSlot");
        iSlot.gameObject.GetComponent<Image>().sprite = item.icon;
    }

    private void showOnRecipe(PowerUpEffect powerUp, int quantity, Transform recipe) {
        var recipeSlot = recipe.gameObject.GetComponent<RecipeSlot>();
        recipeSlot.powerUpEffect = powerUp;
        recipeSlot.quantity = quantity;
        var iSlot = recipe.Find("ItemSlot");
        iSlot.gameObject.GetComponent<Image>().sprite = powerUp.sprite;
    }
}