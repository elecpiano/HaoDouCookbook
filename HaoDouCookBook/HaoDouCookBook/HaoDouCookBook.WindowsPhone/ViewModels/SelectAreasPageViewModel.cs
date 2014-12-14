using Shared.Infrastructures;
using System.Collections.ObjectModel;

namespace HaoDouCookBook.ViewModels
{
    public class SelectAreasPageViewModel : BindableBase
    {
        public ObservableCollection<Province> Provinces { get; set; }

        public SelectAreasPageViewModel()
        {
            Provinces = new ObservableCollection<Province>();
        }
    }

    public class Province : BindableBase
    {
        public ObservableCollection<City> Cities { get; set; }

        private int provinceId;

        public int ProvinceId
        {
            get { return provinceId; }
            set { SetProperty<int>(ref provinceId, value); }
        }

        private string provinceName;

        public string ProvinceName
        {
            get { return provinceName; }
            set { SetProperty<string>(ref provinceName, value); }
        }


        public Province()
        {
            Cities = new ObservableCollection<City>();
            provinceId = 0;
            provinceName = string.Empty;
        }
    }


    public class City : BindableBase
    {
        private int cityId;

        public int CityId
        {
            get { return cityId; }
            set { SetProperty<int>(ref cityId, value); }
        }

        private string cityName;

        public string CityName
        {
            get { return cityName; }
            set { SetProperty<string>(ref cityName, value); }
        }

        public City()
        {
            cityId = 0;
            cityName = string.Empty;
        }
    }

}
