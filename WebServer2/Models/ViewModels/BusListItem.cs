﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer2.Models.ViewModels
{
    public class BusListItem
    {
        public int Id { get; set; }
        [DisplayName("Bus number")]
        public int Number { get; set; }
        [DisplayName("Bus type")]
        public string Type { get; set; }
    }
}
