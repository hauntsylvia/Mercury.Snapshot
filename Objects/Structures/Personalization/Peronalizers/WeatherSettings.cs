﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Objects.Structures.Personalization.Peronalizers
{
    public class WeatherSettings
    {
        public WeatherSettings(string Zip)
        {
            this.Zip = Zip;
        }

        public string Zip { get; }
    }
}