using System;

namespace Chunked.Models
{
    [Serializable]
    public class House
    {
        public int OldId { get; set; }
        public string EntityType { get; set; }
        public string RefNumber { get; set; }
        public int Bedrooms { get; set; }
        public float Bathrooms { get; set; }
        public int Rooms { get; set; }
        public int ParkingSpaces { get; set; }
    }
}