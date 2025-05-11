using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class NguoiDung
    {
        public string NguoiDungId { get; set; }
        public string Email { get; set; }
        public string VaiTro { get; set; }
        public string GioiTinh { get; set; }
        public string AnhDaiDien { get; set; }
        public string TrangThaiTK { get; set; }
        public DateTime NgayTao { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
    }
}
