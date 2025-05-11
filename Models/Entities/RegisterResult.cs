using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class RegisterResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public NguoiDung NguoiDung { get; set; }
    }
}
