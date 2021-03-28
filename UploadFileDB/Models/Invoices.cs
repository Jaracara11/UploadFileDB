using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadFileDB.Models
{
    public partial class Invoices
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public byte[] Attachment { get; set; }
    }
}
