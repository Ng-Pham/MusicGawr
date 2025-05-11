using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class MaXacNhanDatLaiMatKhau
    {
        public int Id { get; set; }
        public string NguoiDungId { get; set; }
        public string MaXacNhan { get; set; }
        public DateTime ThoiGianHetHan { get; set; }
        public bool DaSuDung { get; set; }
    }

}
