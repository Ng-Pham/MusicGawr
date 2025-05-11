using Models;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IUserRepository
    {
        NguoiDung GetUserByUsername(string username);
        NguoiDung GetUserById(string id);
        NguoiDung GetUserByEmail(string email);
        void AddUser(NguoiDung user);
        void LuuMaXacNhan(MaXacNhanDatLaiMatKhau ma);
        MaXacNhanDatLaiMatKhau LayMaXacNhan(string ma);
        void UpdateUserPassword(NguoiDung user);
        void UpdateResetToken(MaXacNhanDatLaiMatKhau resetToken);
    }
}
