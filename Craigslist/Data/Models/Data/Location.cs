using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Models.Data
{
    public class Location : IEquatable<Location>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Area { get; set; }

        [Required]
        public string Locale { get; set; }

        [Required]
        public string Slug { get; set; }

        public bool Active { get; set; } = false;

        public bool Equals(Location other)
        {
            return !(other is null) && Id.Equals(other.Id);
        }
    }
}
