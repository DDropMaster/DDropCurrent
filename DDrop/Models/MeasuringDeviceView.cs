﻿namespace DDrop.Models
{
    public class MeasuringDeviceView : BindableObject
    {
        public double _valueOfDivision;
        public double ValueOfDivision
        {
            get => _valueOfDivision;
            set
            {
                _valueOfDivision = value;
                RaisePropertyChanged("ValueOfDivision");
            }
        }
    }
}