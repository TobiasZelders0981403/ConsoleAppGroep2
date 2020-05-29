using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace cinemaApp
{
    class jsonFood
    {
        public static void jsonFoodMain()
        {

            string fileName = "food.json";

            string rawJson = File.ReadAllText(@fileName);

            List<Food> menu = JsonConvert.DeserializeObject<List<Food>>(rawJson);

            string newJson = JsonConvert.SerializeObject(menu);

            FoodMenu.Caterer();

            File.WriteAllText(fileName, newJson);

        }
    }
}
