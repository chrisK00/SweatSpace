﻿using System;
using System.ComponentModel.DataAnnotations;

namespace SweatSpace.Api.Business.Requests
{
    public class UpdateWorkoutRequest
    {
        [Required, MaxLength(255)]
        public string Name { get; set; }

        [Range(0, 5)]
        public int Rating { get; set; }
        public DateTime? Date { get; set; }
    }
}