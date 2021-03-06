﻿using System;
using DDrop.BE.Enums;

namespace DDrop.BE.Models
{
    public abstract class BasePhoto
    {
        public Guid PhotoId { get; set; }

        public string Name { get; set; }

        public byte[] Content { get; set; }

        public DateTime AddedDate { get; set; }

        public DateTime CreationDateTime { get; set; }

        public PhotoType PhotoType { get; set; }
    }
}