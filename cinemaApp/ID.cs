using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace cinemaApp
{
    class ID
    {

        public static int foodId()
        {
            string fileName = "IdFood.json";
            string rawJson = File.ReadAllText(fileName);
            List<int> foodIds = JsonConvert.DeserializeObject<List<int>>(rawJson);
            int id = foodIds.Max() + 1;
            foodIds.Add(id);
            string newJson = JsonConvert.SerializeObject(foodIds);
            File.WriteAllText(fileName, newJson);
            return id; 
        }

        public static int orderId()
        {
            string fileName = "IdOrder.json";
            string rawJson = File.ReadAllText(fileName);
            List<int> orderIds = JsonConvert.DeserializeObject<List<int>>(rawJson);
            int id = orderIds.Max() + 1;
            orderIds.Add(id);
            string newJson = JsonConvert.SerializeObject(orderIds);
            File.WriteAllText(fileName, newJson);
            return id;
        }

        public static int foodMax()
        {
            string fileName = "IdFood.json";
            string rawJson = File.ReadAllText(fileName);
            List<int> foodIds = JsonConvert.DeserializeObject<List<int>>(rawJson);
            return foodIds.Max();
        }

        public static int orderMax()
        {
            string fileName = "IdOrder.json";
            string rawJson = File.ReadAllText(fileName);
            List<int> orderIds = JsonConvert.DeserializeObject<List<int>>(rawJson);
            List<int> oderIds = new List<int> { 1, 2 };
            return oderIds.Max() + 1;
        }     
    }
}
