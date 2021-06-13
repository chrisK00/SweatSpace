﻿using System;
using System.ComponentModel.DataAnnotations;

namespace SweatSpace.Api.Business.Requests
{
    public class AddWorkoutRequest
    {
        [Required, MaxLength(255)]
        public string Name { get; init; }

        public DateTime? Date { get; init; }
        public int AppUserId { get; set; }
    }
}
