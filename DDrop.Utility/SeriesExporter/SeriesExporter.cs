﻿using System;
using System.IO;
using System.Threading.Tasks;
using DDrop.Db.DbEntities;
using DDrop.Utility.SeriesLocalStorageOperations;

namespace DDrop.Utility.SeriesExporter
{
    public static class SeriesExporter
    {
        public static async Task<DbSeries> ImportLocalSeriesAsync(Stream content, DbUser user)
        {
            var series = await Task.Run(() => JsonSerializeProvider.DeserializeFromStream<DbSeries>(content));
            series.AddedDate = DateTime.Now;
            series.SeriesId = Guid.NewGuid();
            series.CurrentUser = user;
            series.CurrentUserId = user.UserId;
            
            if (series.ReferencePhotoForSeries != null)
            {
                foreach (var referencePhoto in series.ReferencePhotoForSeries)
                {
                    referencePhoto.PhotoId = Guid.NewGuid();
                    referencePhoto.CurrentSeriesId = series.SeriesId;
                }
            }

            return series;
        }

        public static async Task<DbMeasurement> ImportLocalMeasurementAsync(Stream content, DbSeries series)
        {
            var measurement = await Task.Run(() => JsonSerializeProvider.DeserializeFromStream<DbMeasurement>(content));

            measurement.MeasurementId = Guid.NewGuid();
            measurement.AddedDate = DateTime.Now;

            foreach (var dropPhoto in measurement.DropPhotos)
            {
                dropPhoto.PhotoId = Guid.NewGuid();

                if (dropPhoto.Contour != null)
                {
                    dropPhoto.Contour.ContourId = dropPhoto.PhotoId;
                }
            }

            measurement.Drop.DropId = measurement.MeasurementId;
            measurement.CurrentSeriesId = series.SeriesId;
            measurement.CurrentSeries = series;

            return measurement;
        }

        public static async Task ExportMeasurementAsync(string fileName, DbMeasurement dbMeasurement)
        {
            await Task.Run(() => JsonSerializeProvider.SerializeToFileAsync(dbMeasurement, fileName));
        }

        public static async Task ExportSeriesLocalAsync(string fileName, DbSeries dbSeries)
        {
            await Task.Run(() => JsonSerializeProvider.SerializeToFileAsync(dbSeries, fileName));
        }
    }
}