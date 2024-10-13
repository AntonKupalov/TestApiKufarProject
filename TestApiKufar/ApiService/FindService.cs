using KufarRealEstateAnalysis.ApiModels;
using Newtonsoft.Json.Linq;

namespace KufarRealEstateAnalysis.ApiService;

public class FindService
{
    public int GetFloorFromAd(List<AdParametr> parameters)
    {
        var floorParam = parameters.FirstOrDefault(p => p.p == "floor");

        if (floorParam != null && floorParam.v != null && floorParam.v is JArray floorArray && floorArray.Count > 0)
        {
            return (int)floorArray[0];
        }

        return 0; 
    }

    public  double GetApartmentSizeFromAd(List<AdParametr> parameters)
    {
        var sizeParam = parameters.FirstOrDefault(p => p.p == "size");

        if (sizeParam != null && sizeParam.v != null && double.TryParse(sizeParam.v.ToString(), out double size))
        {
            return size;
        }

        return 0; 
    }

    public int GetRoomsFroomAd(List<AdParametr> parametrs)
    {
        var roomsParam = parametrs.FirstOrDefault(p => p.p == "rooms");
         if (roomsParam != null && roomsParam.v != null && int.TryParse(roomsParam.v.ToString(), out int room))
         {
             return room;
         }

         return 0;
    }
    
    public string GetMetroFroomAd(List<AdParametr> parametrs)
    {
        var metroParam = parametrs.FirstOrDefault(p => p.p == "metro");
        if (metroParam != null && metroParam.vl != null && metroParam.vl is JArray metroArray && metroArray.Count > 0)
        {
            return (string)metroArray[0];
        }

        return null;
    }

    public decimal GetPrice(double argPriceUsd)
    {
        return Convert.ToDecimal(argPriceUsd/100);
    }
}