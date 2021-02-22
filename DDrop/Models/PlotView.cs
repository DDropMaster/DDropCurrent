﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using DDrop.Enums;

namespace DDrop.Models
{
    public class PlotView : BindableObject
    {
        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                _isChecked = value;
                RaisePropertyChanged("IsChecked");
            }
        }

        private Guid _plotId;
        public Guid PlotId
        {
            get => _plotId;
            set
            {
                _plotId = value;
                RaisePropertyChanged("PlotId");
            }
        }

        private ObservableCollection<SimplePointView> _points;
        public ObservableCollection<SimplePointView> Points
        {
            get => _points;
            set
            {
                _points = value;
                RaisePropertyChanged("Points");
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }

        private Guid _currentSeriesId;
        public Guid CurrentUserId
        {
            get => _currentSeriesId;
            set
            {
                _currentSeriesId = value;
                RaisePropertyChanged("CurrentUserId");
            }
        }

        private PlotTypeView _plotType;

        public PlotTypeView PlotType
        {
            get => _plotType;
            set
            {
                _plotType = value;
                RaisePropertyChanged("PlotType");
            }
        }

        private UserView _currentUser;
        public UserView CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                RaisePropertyChanged("CurrentUser");
            }
        }

        private bool _isReadOnly;
        public bool IsReadOnly
        {
            get => _isReadOnly;
            set
            {
                _isReadOnly = value;
                RaisePropertyChanged("IsReadOnly");
            }
        }
    }
}