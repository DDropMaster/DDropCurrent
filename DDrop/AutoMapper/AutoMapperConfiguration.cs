﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows.Shapes;
using AutoMapper;
using DDrop.BE.Enums.Options;
using DDrop.BE.Models;
using DDrop.BE.Models.Thermal;
using DDrop.Db.DbEntities;
using DDrop.Models;
using DDrop.Models.Thermal;
using DDrop.Utility.SeriesLocalStorageOperations;
using PlotSettingsView = DDrop.Models.PlotSettingsView;

namespace DDrop.AutoMapper
{
    public static class AutoMapperConfiguration
    {
        public static IMapper InitializeAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SubstanceModelView, SubstanceModel>();
                cfg.CreateMap<SubstanceModel, SubstanceModelView>();
                cfg.CreateMap<SubstanceModel, DbSubstances>()
                    .ForMember(x => x.Series, opt => opt.Ignore());
                cfg.CreateMap<DbSubstances, SubstanceModel>();


                cfg.CreateMap<SubstanceDetailsResponse, SubstanceModel>()
                    .ForMember(x => 
                        x.SubstanceId, opt => opt.Ignore());

                cfg.CreateMap<DropView, Drop>();
                cfg.CreateMap<Drop, DropView>();
                cfg.CreateMap<Drop, DbDrop>()
                    .ForMember(x => x.Measurement, opt => opt.Ignore());
                cfg.CreateMap<DbDrop, Drop>();


                cfg.CreateMap<ContourView, Contour>();
                cfg.CreateMap<Contour, ContourView>()
                    .ForMember(x =>
                        x.Lines, opt => opt.MapFrom<ContourResolver>());
                cfg.CreateMap<Contour, DbContour>()
                    .ForMember(x =>
                        x.ConnectedLines, opt =>
                        opt.MapFrom(src => JsonSerializeProvider.SerializeToString(src.SimpleLines)))
                    .ForMember(x =>
                        x.CalculationParameters, opt =>
                        opt.MapFrom(src => JsonSerializeProvider.SerializeToString(src.Parameters)))
                    .ForMember(x =>
                        x.CalculationProvider, opt =>
                        opt.MapFrom(src => src.CalculationVariants.ToString()));
                cfg.CreateMap<DbContour, Contour>()
                    .ForMember(x =>
                        x.Parameters, opt =>
                        opt.MapFrom(src => src.CalculationParameters != null ?
                            JsonSerializeProvider.DeserializeFromString<AutoCalculationParameters>(
                                src.CalculationParameters) : null))
                    .ForMember(x =>
                        x.CalculationVariants, opt =>
                        opt.MapFrom(src =>
                            (CalculationVariants)Enum.Parse(typeof(CalculationVariants), src.CalculationProvider,
                                true)))
                    .ForMember(x =>
                        x.SimpleLines, opt =>
                        opt.MapFrom(src => src.ConnectedLines != null ?
                            JsonSerializeProvider.DeserializeFromString<ObservableCollection<SimpleLine>>(src.ConnectedLines) : null));


                cfg.CreateMap<MeasurementView, Measurement>();
                cfg.CreateMap<Measurement, MeasurementView>()
                    .ForMember(x =>
                        x.ProcessedThermal, opt => opt.Ignore())
                    .ForMember(x =>
                        x.IsChecked, opt => opt.Ignore());
                cfg.CreateMap<Measurement, DbMeasurement>()
                    .ForMember(x => 
                        x.DropPhotos, opt => opt.MapFrom(src => src.DropPhotos.Where(p => p.PhotoId != Guid.Empty)))
                    .ForMember(x =>
                        x.CurrentSeries, opt => opt.Ignore());
                cfg.CreateMap<DbMeasurement, Measurement>();


                cfg.CreateMap<ReferencePhotoView, ReferencePhoto>()
                    .PreCondition(x => x.PhotoId != Guid.Empty);
                cfg.CreateMap<ReferencePhoto, ReferencePhotoView>()
                    .ForMember(x =>
                        x.Line, opt => opt.MapFrom<ReferencePhotoResolver>());
                cfg.CreateMap<ReferencePhoto, DbReferencePhoto>()
                    .ForMember(x =>
                        x.Series, opt => opt.Ignore())
                    .ForMember(dest =>
                        dest.ReferenceLine, opt =>
                        opt.MapFrom(src => JsonSerializeProvider.SerializeToString(src.SimpleLine)))
                    .ForMember(dest =>
                        dest.MeasuringDevice, opt =>
                        opt.MapFrom(src => JsonSerializeProvider.SerializeToString(src.MeasuringDevice))); 
                cfg.CreateMap<DbReferencePhoto, ReferencePhoto>()
                    .ForMember(dest =>
                        dest.MeasuringDevice, opt =>
                        opt.MapFrom(src => src.MeasuringDevice != null ?
                            JsonSerializeProvider.DeserializeFromString<MeasuringDevice>(src.ReferenceLine) : null))
                    .ForMember(dest =>
                        dest.SimpleLine, opt =>
                        opt.MapFrom(src => src.ReferenceLine != null ? 
                            JsonSerializeProvider.DeserializeFromString<SimpleLine>(src.ReferenceLine) : null));

                
                cfg.CreateMap<DropPhoto, DropPhotoView>()
                    .ForMember(x =>
                        x.IsChecked, opt => opt.Ignore())
                    .ForMember(x =>
                        x.RequireSaving, opt => opt.Ignore())
                    .ForMember(x =>
                        x.Processed, opt => opt.Ignore())
                    .ForMember(x =>
                        x.Lines, opt => opt.MapFrom<DropPhotoLinesResolver>());
                cfg.CreateMap<DropPhotoView, DropPhoto>();
                cfg.CreateMap<SimpleLineView, TypedLineView>()
                    .ForMember(x =>
                        x.Line, opt => opt.MapFrom<LineResolver>());
                cfg.CreateMap<SimpleLineView, Line>()
                    .ConvertUsing(new SimpleLineViewToLineConverter());
                cfg.CreateMap<DropPhoto, DbDropPhoto>()
                    .ForMember(x => x.Measurement, opt => opt.Ignore())
                    .ForMember(x =>
                        x.SimpleLines, opt =>
                        opt.MapFrom(src => src.SimpleLines != null ?
                            JsonSerializeProvider.SerializeToString(src.SimpleLines) : null));
                cfg.CreateMap<DbDropPhoto, DropPhoto>()
                    .ForMember(x =>
                        x.SimpleLines, opt =>
                        opt.MapFrom(src => src.SimpleLines != null ?
                            JsonSerializeProvider.DeserializeFromString<List<SimpleLine>>(src.SimpleLines) : null));

                cfg.CreateMap<Series, SeriesView>()
                    .ForMember(x =>
                        x.Sub, opt => opt.Ignore())
                    .ForMember(x =>
                        x.IsSubstanceEdited, opt => opt.Ignore())
                    .ForMember(x =>
                        x.IsSubstanceExists, opt => opt.Ignore())
                    .ForMember(x =>
                        x.ExactCalculationModel, opt => opt.Ignore())
                    .ForMember(x =>
                        x.IsChecked, opt => opt.Ignore())
                    .ForMember(x =>
                        x.CanDrawPlot, opt => opt.Ignore())
                    .ForMember(x =>
                        x.CanDrawTemperaturePlot, opt => opt.Ignore())
                    .ForMember(x =>
                        x.CanDrawAnyPlot, opt => opt.Ignore())
                    .ForMember(x =>
                        x.Loaded, opt => opt.Ignore())
                    .ForMember(x =>
                        x.RegionOfInterest, opt => opt.MapFrom<RegionOfInterestResolver>());
                cfg.CreateMap<SeriesView, Series>();
                cfg.CreateMap<Series, DbSeries>()
                    .ForMember(x =>
                        x.CurrentUser, opt => opt.Ignore())
                    .ForMember(x =>
                        x.RegionOfInterest, opt =>
                        opt.MapFrom(src => src.RegionOfInterest != null ?
                            JsonSerializeProvider.SerializeToString(src.RegionOfInterest) : null))
                    .ForMember(x =>
                        x.Settings, opt =>
                        opt.MapFrom(src => src.Settings != null ?
                            JsonSerializeProvider.SerializeToString(src.Settings) : null));
                cfg.CreateMap<DbSeries, Series>()
                    .ForMember(x =>
                        x.Settings, opt => opt.MapFrom<SeriesSettingsResolver>())
                    .ForMember(x =>
                        x.RegionOfInterest, opt =>
                        opt.MapFrom(src => src.RegionOfInterest != null ?
                            JsonSerializeProvider.DeserializeFromString<ObservableCollection<TypedRectangle>>(src.RegionOfInterest) : null));

                cfg.CreateMap<User, UserView>()
                    .ForMember(x => x.IsAnySelectedSeriesCanDrawPlot, opt =>
                        opt.Ignore())
                    .ForMember(x => x.IsLoggedIn, opt =>
                        opt.Ignore());
                cfg.CreateMap<UserView, User>();
                cfg.CreateMap<User, DbUser>()
                    .ForMember(x => x.UserSeries, opt => opt.Ignore());
                cfg.CreateMap<DbUser, User>();


                cfg.CreateMap<ThermalPhoto, ThermalPhotoView>()
                    .ForMember(x =>
                        x.Ellipse, opt => opt.MapFrom<ThermalPhotoEllipseResolver>())
                    .ForMember(x =>
                        x.Processed, opt => opt.Ignore());
                cfg.CreateMap<ThermalPhotoView, ThermalPhoto>();
                cfg.CreateMap<ThermalPhoto, DbThermalPhoto>()
                    .ForMember(x => x.Measurement, opt => opt.Ignore())
                    .ForMember(x =>
                        x.EllipseCoordinate, opt =>
                        opt.MapFrom(src => src.EllipseCoordinate != null ?
                            JsonSerializeProvider.SerializeToString(src.EllipseCoordinate) : null));
                cfg.CreateMap<DbThermalPhoto, ThermalPhoto>()
                    .ForMember(x => x.FlirImage, opt =>
                        opt.Ignore())
                    .ForMember(x =>
                        x.EllipseCoordinate, opt =>
                        opt.MapFrom(src => !string.IsNullOrWhiteSpace(src.EllipseCoordinate) ?
                            JsonSerializeProvider.DeserializeFromString<Point>(src.EllipseCoordinate) : new Point()));
                

                cfg.CreateMap<ThermalPhoto, BasePhoto>().ReverseMap();
                cfg.CreateMap<DropPhoto, BasePhoto>().ReverseMap();

                cfg.CreateMap<ThermalPhotoView, BasePhotoView>().ReverseMap();
                cfg.CreateMap<DropPhotoView, BasePhotoView>().ReverseMap();


                cfg.CreateMap<Comment, CommentView>()
                    .ForMember(x => x.CommentAdded, opt =>
                        opt.Ignore());
                cfg.CreateMap<CommentView, Comment>();
                cfg.CreateMap<Comment, DbComment>();
                cfg.CreateMap<DbComment, Comment>();


                cfg.CreateMap<Plot, PlotView>()
                    .ForMember(x => x.IsChecked, opt =>
                        opt.Ignore())
                    .ForMember(x => x.IsDeletable, opt =>
                        opt.Ignore())
                    .ForMember(x => x.IsEditable, opt =>
                        opt.Ignore());
                cfg.CreateMap<PlotView, Plot>();
                cfg.CreateMap<Plot, DbPlot>()
                    .ForMember(x =>
                        x.Series, opt =>
                        opt.MapFrom(src =>
                            src.SeriesId != Guid.Empty
                                ? new Series() { SeriesId = src.SeriesId }
                                : null))
                    .ForMember(x => x.CurrentUser, opt =>
                        opt.Ignore())
                    .ForMember(dest =>
                        dest.Points, opt =>
                        opt.MapFrom(src => src.Points != null ?
                            JsonSerializeProvider.SerializeToString(src.Points) : null))
                    .ForMember(x =>
                        x.Settings, opt =>
                        opt.MapFrom(src => src.Settings != null ?
                            JsonSerializeProvider.SerializeToString(src.Settings) : null));
                cfg.CreateMap<DbPlot, Plot>()
                    .ForMember(x =>
                        x.SeriesId, opt =>
                        opt.MapFrom(src =>
                            src.Series != null
                                ? src.Series.SeriesId
                                : Guid.Empty))
                    .ForMember(x =>
                        x.Points, opt =>
                        opt.MapFrom(src =>
                            src.Points != null
                                ? JsonSerializeProvider.DeserializeFromString<ObservableCollection<SimplePoint>>(src.Points)
                                : null))
                    .ForMember(x =>
                        x.Settings, opt =>
                        opt.MapFrom(src =>
                            src.Settings != null
                                ? JsonSerializeProvider.DeserializeFromString<PlotSettings>(src.Settings)
                                : new PlotSettings {DimensionlessSettings = new DimensionlessSettings()}));


                cfg.CreateMap<CameraInfoView, CameraInfo>().ReverseMap();
                cfg.CreateMap<CompassInfoView, CompassInfo>().ReverseMap();
                cfg.CreateMap<FlirImageView, FlirImage>().ReverseMap();
                cfg.CreateMap<GpsInfoView, GpsInfo>().ReverseMap();
                cfg.CreateMap<ThermalDataView, ThermalData>().ReverseMap();
                cfg.CreateMap<ThermalParametersView, ThermalParameters>().ReverseMap();

                cfg.CreateMap<SimpleLineView, SimpleLine>().ReverseMap();
                cfg.CreateMap<AutoCalculationParametersView, AutoCalculationParameters>().ReverseMap();
                cfg.CreateMap<SimplePointView, SimplePoint>().ReverseMap();
                cfg.CreateMap<SeriesSettingsView, SeriesSettings>().ReverseMap();
                cfg.CreateMap<AutoCalculationSettingsView, AutoCalculationSettings>();
                cfg.CreateMap<AutoCalculationSettings, AutoCalculationSettingsView>();
                cfg.CreateMap<GeneralSeriesSettingsView, GeneralSeriesSettings>().ReverseMap();
                cfg.CreateMap<ErrorSettingsView, ErrorSettings>().ReverseMap();
                cfg.CreateMap<MeasuringDeviceView, MeasuringDevice>().ReverseMap();
                cfg.CreateMap<TypedRectangleView, TypedRectangle>().ReverseMap();
                cfg.CreateMap<PlotSettingsView, PlotSettings>().ReverseMap();
                cfg.CreateMap<DimensionlessSettingsView, DimensionlessSettings>().ReverseMap();
            });

            IMapper mapper = config.CreateMapper();
            
            config.AssertConfigurationIsValid();

            return mapper;
        }
    }
}