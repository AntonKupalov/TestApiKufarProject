using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using KufarRealEstateAnalysis.ApiModels;
using KufarRealEstateAnalysis.ApiService;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KufarRealEstateAnalysis
{
    public class Program
    {
        private const string API_URL_KUFAR = "https://api.kufar.by/search-api/v2/" +
                                             "search/rendered-paginated?cat=1010&cur=USD&" +
                                             "gtsy=country-belarus~province-minsk~locality-minsk&lang=ru&size=30&typ=sell";
        
        public static async Task Main(string[] args)
        {
            var find = new FindService();
            var calculateService = new CalculationService();
            List<Ad> ads = await GetAdsFromKufar();
            
            
            var floorPriceData = calculateService.CalculatePriceForFloor(ads);
            
            var roomsPriceData = calculateService.CalculatePriceForRoom(ads);
            
            var metroPriceData = calculateService.CalculatePriceForMetro(ads);
            
            

            Console.WriteLine("Зависимость стоимости квадратного метра от этажа квартиры");
            foreach (var data in floorPriceData)
            { 
                Console.WriteLine($"Этаж: {data.Floor}, Средняя стоимость за м²: {data.Price:F2} USD");
            }
            
            Console.WriteLine("Зависимость стоимости квадратного метра от количества комнат");
            foreach (var data in roomsPriceData)
            { 
                Console.WriteLine($"Количество комнат: {data.Room}, Средняя стоимость за м²: {data.Price:F2} USD");
            }
            Console.WriteLine("Зависимость стоимости от ближайшей станции метро");
            foreach (var data in metroPriceData)
            { 
                Console.WriteLine($"Метро: {data.Metro}, Средняя стоимость за м²: {data.Price:F2} USD");
            }
            
        }
        
        static async Task<List<Ad>> GetAdsFromKufar()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(API_URL_KUFAR);
                var adsData = JsonConvert.DeserializeObject<RootObject>(response);
                return adsData.ads;
            }
        }
    }
}
