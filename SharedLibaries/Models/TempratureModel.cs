﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibaries.Models
{
    public class TempratureModel
    {
        public string Temperature { get; set; }

        public string Humidity { get; set; }


        public TempratureModel()
        {

        }
        public TempratureModel(string temperature, string humidity)
        {
            Temperature = temperature;
            Humidity = humidity;
        }

    }

}
