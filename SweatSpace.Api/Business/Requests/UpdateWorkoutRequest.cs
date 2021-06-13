using System;
using System.ComponentModel.DataAnnotations;

namespace SweatSpace.Api.Business.Requests
{
    public class UpdateWorkoutRequest
    {
        [Required, MaxLength(255)]
        public string Name { get; init; }

        [Range(0, 5)]
        public int Rating { get; init; }
        public DateTime? Date { get; init; }
    }
}