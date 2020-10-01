using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibaries.Models
{
    public class WeatherList : ObservableCollection<TempratureModel>
    {

        public WeatherList()
        {

        }

        public WeatherList(string weather1,string weather2)
        {
            Add(new TempratureModel(weather1, weather2));
        }
    }
}
