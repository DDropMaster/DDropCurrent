﻿/*
Copyright 2017 Ryan Bales (https://github.com/ryanbales)
Date: 10/2/2017
Class: DataExtractor/Models/CompassInfo.cs

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


namespace DDrop.Models.Thermal
{
    public class CompassInfoView
    {
        public int Degrees { get; set; }
        public int Pitch { get; set; }
        public int Roll { get; set; }
    }
}