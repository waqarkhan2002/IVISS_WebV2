namespace IVISS_WebV2.Classes
{
  
    public class SearchFilterCriteriaLP
    {
        public List<SearchFilterDataListLicensePlate> FilterDataList { get; set; }
    }

    public class SearchFilterDataListLicensePlate
    {
        public string FieldName { get; set; }
        public string Value { get; set; }
        public string Operator { get; set; }
        public string DataType { get; set; }
    }

    public class Params
    {
        public int page { get; set; }
        public int itemsperpage { get; set; }
    }

    public class RootSearchFilterCriteria
    {
        public SearchFilterCriteriaLP data { get; set; }
        public Params @params { get; set; }
    }
}
