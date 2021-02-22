﻿/*
Copyright 2017 Ryan Bales (https://github.com/ryanbales)
Date: 10/2/2017
Class: DataExtractor/Models/FlirImage.cs

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/


using System;
using System.Collections.Generic;
using System.Drawing;

namespace DDrop.BE.Models.Thermal
{
    public class FlirImage
    {
        public CameraInfo CameraInfo { get; set; }
        public CompassInfo CompassInfo { get; set; }
        public DateTime DateTaken { get; set; }
        public string Description { get; set; }
        public GpsInfo GpsInfo { get; set; }
        public int Height { get; set; }
        public int MaxSignalValue { get; set; }
        public int MinSignalValue { get; set; }
        public int Precision { get; set; }
        public Enums.TempUnit TemperatureUnit { get; set; }
        public ThermalParameters ThermalParameters { get; set; }
        public IList<ThermalData> ThermalData { get; set; }
        public string Title { get; set; }
        public int Width { get; set; }
        public Bitmap Image { get; set; }
    }
}