using UnityEngine;

public class ItemGen
{
    public static Item CreateItem(int itemID)
    {
        Item temp = new Item();
        #region Variables
        string name = "";
        int value = 0;
        string description = "";
        string icon = "";
        string mesh = "";
        ItemType type = ItemType.Food;
        #endregion
        #region Switch Item Data
        switch (itemID)
        {
            #region Food 0-99
            case 0:
                name = "Apple";
                value = 5;
                description = "Delicious Sweet";
                icon = "Apple";
                mesh = "Apple";
                type = ItemType.Food;
                break;
            case 1:
                name = "Cheese";
                value = 10;
                description = "Golden Goodness";
                icon = "Cheese";
                mesh = "Cheese";
                type = ItemType.Food;
                break;
            #endregion
            #region Weapon 100-199
                  case 0:
                name = "Axe";
                value = 50;
                description = "Brutal Sharp Axe";
                icon = "Axe";
                mesh = "Axe";
                type = ItemType.Weapon;
                break;
            case 1:
                name = "Bow And Arrow";
                value = 45;
                description = "Shoot Down Your Enemies From Afar";
                icon = "Bow And Arrow";
                mesh = "Bow And Arrow";
                type = ItemType.Weapon;
                break;
            #endregion
            #region Apparel 200-299
                     case 0:
                name = "Cloak";
                value = 20;
                description = "Hide In The Shadows";
                icon = "Cloak";
                mesh = "Cloak";
                type = ItemType.Apparel;
                break;
            case 1:
                name = "Glove";
                value = 5;
                description = "Get A Better Grip On Your Weapons";
                icon = "Glove";
                mesh = "Glove";
                type = ItemType.Apparel;
                break;
            #endregion
            #region Crafting 300-399
                       case 0:
                name = "Diamond";
                value = 1000;
                description = "Shiny Diamond";
                icon = "Diamond";
                mesh = "Diamond";
                type = ItemType.Crafting;
                break;
            case 1:
                name = "Ingot";
                value = 500;
                description = "Upgrade Your Armour Immensely";
                icon = "Ingot";
                mesh = "Ingot";
                type = ItemType.Crafting;
                break;
            #endregion
            #region Quest 400-499
            #endregion
            #region Money 500-599
            #endregion
            #region Ingridients 600-699
            #endregion
            #region Potions 700-799
            #endregion
            #region Scrolls 800-899
            #endregion
            default:
                itemID = 0;
                name = "Apple";
                value = 5;
                description = "Delicious Sweet";
                icon = "Apple";
                mesh = "Apple";
                type = ItemType.Food;
                break;
        }
        #endregion
        #region Temp Connect
        temp.ID = itemID;
        temp.Name = name;
        temp.Value = value;
        temp.Description = description;
        temp.Icon = Resources.Load("Icons/" + icon) as Texture2D;
        temp.Mesh = mesh;
        temp.Type = type;
        #endregion
        return temp;
    }
}
