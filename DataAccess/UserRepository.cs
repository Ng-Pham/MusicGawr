using DataAccess.Interfaces;
using Models;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public UserRepository()
        {
            _connectionFactory = DbConnectionFactory.Instance;
        }
        public NguoiDung GetUserByUsername(string username)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                string query = "SELECT NguoiDungId, Email, VaiTro, GioiTinh, AnhDaiDien, TrangThaiTK, NgayTao, TenDangNhap, MatKhau FROM NguoiDung WHERE TenDangNhap = @TenDangNhap";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@TenDangNhap", username);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new NguoiDung
                    {
                        NguoiDungId = reader.GetString(0),
                        Email = reader.GetString(1),
                        VaiTro = reader.GetString(2),
                        GioiTinh = reader.IsDBNull(3) ? null : reader.GetString(3),
                        AnhDaiDien = reader.GetString(4),
                        TrangThaiTK = reader.GetString(5),
                        NgayTao = reader.GetDateTime(6),
                        TenDangNhap = reader.GetString(7),
                        MatKhau = reader.GetString(8)
                    };
                }
                return null;
            }
        }
        public NguoiDung GetUserById(string id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                string query = "SELECT NguoiDungId, Email, VaiTro, GioiTinh, AnhDaiDien, TrangThaiTK, NgayTao, TenDangNhap, MatKhau FROM NguoiDung WHERE NguoiDungId = @NguoiDungId";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@NguoiDungId", id);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new NguoiDung
                    {
                        NguoiDungId = reader.GetString(0),
                        Email = reader.GetString(1),
                        VaiTro = reader.GetString(2),
                        GioiTinh = reader.IsDBNull(3) ? null : reader.GetString(3),
                        AnhDaiDien = reader.GetString(4),
                        TrangThaiTK = reader.GetString(5),
                        NgayTao = reader.GetDateTime(6),
                        TenDangNhap = reader.GetString(7),
                        MatKhau = reader.GetString(8)
                    };
                }
                return null;
            }
        }

        public NguoiDung GetUserByEmail(string email)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                string query = "SELECT NguoiDungId, Email, VaiTro, GioiTinh, AnhDaiDien, TrangThaiTK, NgayTao, TenDangNhap, MatKhau FROM NguoiDung WHERE Email = @Email";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Email", email);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new NguoiDung
                    {
                        NguoiDungId = reader.GetString(0),
                        Email = reader.GetString(1),
                        VaiTro = reader.GetString(2),
                        GioiTinh = reader.IsDBNull(3) ? null : reader.GetString(3),
                        AnhDaiDien = reader.GetString(4),
                        TrangThaiTK = reader.GetString(5),
                        NgayTao = reader.GetDateTime(6),
                        TenDangNhap = reader.GetString(7),
                        MatKhau = reader.GetString(8)
                    };
                }
                return null;
            }
        }
        public string GenerateNewUserId()
        {
            using (SqlConnection conn = DbConnectionFactory.Instance.CreateConnection())
            {
                conn.Open();

                // Lấy mã lớn nhất hiện có
                string query = "SELECT TOP 1 NguoiDungId FROM NguoiDung WHERE NguoiDungId LIKE 'US%' ORDER BY NguoiDungId DESC";
                SqlCommand cmd = new SqlCommand(query, conn);

                var result = cmd.ExecuteScalar();
                int newNumber = 1;

                if (result != null)
                {
                    string lastId = result.ToString(); // e.g., US005
                    string numberPart = lastId.Substring(2); // lấy "005"
                    if (int.TryParse(numberPart, out int parsed))
                    {
                        newNumber = parsed + 1;
                    }
                }

                return "US" + newNumber.ToString("D3"); // e.g., US006
            }
        }
        public void AddUser(NguoiDung user)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                string newId = GenerateNewUserId();
                var command = new SqlCommand(
                    "INSERT INTO NguoiDung(NguoiDungId, Email, VaiTro, GioiTinh, AnhDaiDien, TrangThaiTK, NgayTao, TenDangNhap, MatKhau) VALUES(@NguoiDungId, @Email, @VaiTro, @GioiTinh, @AnhDaiDien, @TrangThaiTK, @NgayTao, @TenDangNhap, @MatKhau)",
                    connection);
                command.Parameters.AddWithValue("@NguoiDungId", newId);
                command.Parameters.AddWithValue("@Email", (object)user.Email ?? DBNull.Value);
                command.Parameters.AddWithValue("@VaiTro", "user");
                command.Parameters.AddWithValue("@GioiTinh", (object)DBNull.Value);
                command.Parameters.AddWithValue("@AnhDaiDien", "us_default.jpg");
                command.Parameters.AddWithValue("@TrangThaiTK", "HoatDong");
                command.Parameters.AddWithValue("@NgayTao", DateTime.Now);
                command.Parameters.AddWithValue("@TenDangNhap", user.TenDangNhap);
                command.Parameters.AddWithValue("@MatKhau", user.MatKhau);

                command.ExecuteNonQuery();
            }
        }

        public void LuuMaXacNhan(MaXacNhanDatLaiMatKhau ma)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                string query = @"INSERT INTO MaXacNhanDatLaiMatKhau (NguoiDungId, MaXacNhan, ThoiGianHetHan, DaSuDung)
                             VALUES (@NguoiDungId, @MaXacNhan, @ThoiGianHetHan, @DaSuDung)";
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@NguoiDungId", ma.NguoiDungId);
                    cmd.Parameters.AddWithValue("@MaXacNhan", ma.MaXacNhan);
                    cmd.Parameters.AddWithValue("@ThoiGianHetHan", ma.ThoiGianHetHan);
                    cmd.Parameters.AddWithValue("@DaSuDung", ma.DaSuDung);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public MaXacNhanDatLaiMatKhau LayMaXacNhan(string ma)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                string query = @"SELECT Id, NguoiDungId, MaXacNhan, ThoiGianHetHan, DaSuDung
                             FROM MaXacNhanDatLaiMatKhau
                             WHERE MaXacNhan = @MaXacNhan 
                                   AND DaSuDung = 0 AND ThoiGianHetHan >= GETUTCDATE()";

                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@MaXacNhan", ma);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new MaXacNhanDatLaiMatKhau
                            {
                                Id = reader.GetInt32(0),
                                NguoiDungId = reader.GetString(1),
                                MaXacNhan = reader.GetString(2),
                                ThoiGianHetHan = reader.GetDateTime(3),
                                DaSuDung = reader.GetBoolean(4)
                            };
                        }
                    }
                }
            }
            return null;
        }
        public void UpdateUserPassword(NguoiDung user)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                var command = new SqlCommand(
                    "UPDATE NguoiDung SET MatKhau = @MatKhau WHERE NguoiDungId = @NguoiDungId",
                    connection);
                command.Parameters.AddWithValue("@MatKhau", user.MatKhau);
                command.Parameters.AddWithValue("@NguoiDungId", user.NguoiDungId);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateResetToken(MaXacNhanDatLaiMatKhau resetToken)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                var command = new SqlCommand(
                    "UPDATE MaXacNhanDatLaiMatKhau SET DaSuDung = @DaSuDung WHERE Id = @Id",
                    connection);
                command.Parameters.AddWithValue("@DaSuDung", resetToken.DaSuDung);
                command.Parameters.AddWithValue("@Id", resetToken.Id);
                command.ExecuteNonQuery();
            }
        }
    }
}
