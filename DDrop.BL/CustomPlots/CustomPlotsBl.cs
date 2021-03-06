﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DDrop.BE.Models;
using DDrop.DAL;
using DDrop.Db.DbEntities;
using DDrop.Utility.SeriesLocalStorageOperations;

namespace DDrop.BL.CustomPlots
{
    public class CustomPlotsBl : ICustomPlotsBl
    {
        private readonly IDDropRepository _dDropRepository;
        private readonly IMapper _mapper;

        public CustomPlotsBl(IDDropRepository dDropRepository, IMapper mapper)
        {
            _dDropRepository = dDropRepository;
            _mapper = mapper;
        }

        public async Task CreatePlot(Plot plot)
        {
            await _dDropRepository.CreatePlot(_mapper.Map<Plot, DbPlot>(plot));
        }

        public async Task DeletePlot(Plot plot)
        {
            await _dDropRepository.DeletePlot(_mapper.Map<Plot, DbPlot>(plot));
        }

        public async Task UpdatePlot(Plot plot)
        {
            var dbPlot = _mapper.Map<Plot, DbPlot>(plot);

            await _dDropRepository.UpdatePlot(dbPlot);
        }

        public async Task UpdatePlotName(string text, Guid plotId)
        {
            await Task.Run(() => _dDropRepository.UpdatePlotName(text, plotId));
        }

        public async Task<Plot> GetPlot(Guid plotId)
        {
            return _mapper.Map<DbPlot, Plot>(await Task.Run(() => _dDropRepository.GetPlot(plotId)));
        }

        public async Task<List<SimplePoint>> GetPlotPoints(Guid plotId, double xDimensionlessDivider, double yDimensionlessDivider, bool dimensionless = false)
        {
            var points = JsonSerializeProvider.DeserializeFromString<List<SimplePoint>>(await _dDropRepository.GetPlotPoints(plotId));

            if (dimensionless)
            {
                foreach (var point in points)
                {
                    point.X = point.X / xDimensionlessDivider;
                    point.Y = point.Y / yDimensionlessDivider;
                }
            }

            return points;
        }
    }
}
