using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace denyis
{
    public class Image
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Description { get; set; }
        public byte[] ImageData { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
