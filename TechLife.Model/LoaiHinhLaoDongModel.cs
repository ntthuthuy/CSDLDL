﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Model
{
    public class LoaiHinhLaoDongModel
    {
        public int Id { get; set; }
        public string TenLoaiHinh { get; set; }
        public string MoTa { get; set; }
        public bool IsStatus { get; set; }
        public bool IsDelete { get; set; }
    }
}
