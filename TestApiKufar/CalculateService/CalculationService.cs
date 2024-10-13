using KufarRealEstateAnalysis.ApiModels;
using KufarRealEstateAnalysis.ApiService;
using KufarRealEstateAnalysis.ViewModel;

namespace KufarRealEstateAnalysis;

public class CalculationService
{
    private const double MIN_PRICE_FOR_SQUERE_METER = 500;
    private FindService _findService = new();
    public List<FloorPriceByData> CalculatePriceForFloor(List<Ad> ads)
    {
        return ads
            .Where(ad => _findService.GetFloorFromAd(ad.ad_parameters) > 0 &&
                         _findService.GetApartmentSizeFromAd(ad.ad_parameters) > 0 &&
                         ad.price_usd > 0 &&
                         (ad.price_usd / 100 / _findService.GetApartmentSizeFromAd(ad.ad_parameters)) >= MIN_PRICE_FOR_SQUERE_METER) // Фильтр по цене
            .GroupBy(ad => _findService.GetFloorFromAd(ad.ad_parameters)) // Группировка по этажу
            .Select(g => new FloorPriceByData
            {
                Floor = g.Key,
                Price = g.Average(ad => ad.price_usd / 100 / _findService.GetApartmentSizeFromAd(ad.ad_parameters))
            })
            .OrderBy(data => data.Floor)
            .ToList();
        
    }
    public List<RoomPriceByData> CalculatePriceForRoom(List<Ad> ads)
    {
       
        
        return ads
            .Where(ad =>
                _findService.GetRoomsFroomAd(ad.ad_parameters) > 0 &&
                _findService.GetApartmentSizeFromAd(ad.ad_parameters) > 0 &&
                ad.price_usd > 0 &&
                ad.price_usd / 100 / _findService.GetApartmentSizeFromAd(ad.ad_parameters) >= MIN_PRICE_FOR_SQUERE_METER)
            .GroupBy(ad => _findService.GetRoomsFroomAd(ad.ad_parameters)) 
            .Select(g => new RoomPriceByData()
            {
                Room = g.Key,
                Price = g.Average(ad => ad.price_usd / 100 / _findService.GetApartmentSizeFromAd(ad.ad_parameters))
            })
            .OrderBy(data => data.Room) 
            .ToList();
    }
    
    public List<MetroPriceByData> CalculatePriceForMetro(List<Ad> ads)
    {

        return ads
            .Where(ad =>
                _findService.GetMetroFroomAd(ad.ad_parameters) != null &&
                _findService.GetApartmentSizeFromAd(ad.ad_parameters) > 0 &&
                ad.price_usd > 0 &&
                ad.price_usd / 100 / this._findService.GetApartmentSizeFromAd(ad.ad_parameters) >= MIN_PRICE_FOR_SQUERE_METER) // Учитываем только объявления с корректными данными
            .GroupBy(ad => _findService.GetMetroFroomAd(ad.ad_parameters).ToString())
            .Select(g => new MetroPriceByData()
            {
                Metro= g.Key,
                Price = g.Average(ad => ad.price_usd / 100 / _findService.GetApartmentSizeFromAd(ad.ad_parameters))
            })
            .OrderBy(data => data.Metro)
            .ToList();
    }
}