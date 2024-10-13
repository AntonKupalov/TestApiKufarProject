namespace KufarRealEstateAnalysis.ApiModels;

public class AdParametr
{
    public object vl { get; set; }
    
    public string pl { get; set; }
    public object v { get; set; }  // Используем object, так как v может быть как массивом, так и строкой
    public string p { get; set; }
}