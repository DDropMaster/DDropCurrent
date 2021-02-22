﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using DDrop.BE.Models;
using DDrop.DAL;
using DDrop.Db.DbEntities;

namespace DDrop.BL.ReferenceBL
{
    public class ReferenceBl : IReferenceBl
    {
        private readonly IDDropRepository _dDropRepository;
        private readonly IMapper _mapper;

        public ReferenceBl(IDDropRepository dDropRepository, IMapper mapper)
        {
            _dDropRepository = dDropRepository;
            _mapper = mapper;
        }

        public async Task<byte[]> GetReferencePhotoContent(Guid referencePhotoId)
        {
            return await Task.Run(() => _dDropRepository.GetReferencePhotoContent(referencePhotoId));
        }

        public async Task UpdateReferencePhoto(ReferencePhoto referencePhoto)
        {
            var dbReferencePhoto = _mapper.Map<ReferencePhoto, DbReferencePhoto>(referencePhoto);

            await Task.Run(() => _dDropRepository.UpdateReferencePhoto(dbReferencePhoto));
        }

        public async Task DeleteReferencePhoto(Guid referencePhotoId)
        {
            await Task.Run(() => _dDropRepository.DeleteReferencePhoto(referencePhotoId));
        }
    }
}