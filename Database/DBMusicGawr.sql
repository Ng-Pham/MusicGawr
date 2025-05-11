CREATE DATABASE MusicGawr
USE MusicGawr
GO

-- Bảng Người Dùng -- 
CREATE TABLE NguoiDung (
    NguoiDungId nvarchar(15) NOT NULL,
    Email nvarchar(30),
    VaiTro nvarchar(10),
    GioiTinh nvarchar(3),
    AnhDaiDien nvarchar(100),
    TrangThaiTK nvarchar(10),
    NgayTao DateTime,
    TenDangNhap nvarchar(20),
    MatKhau nvarchar(100),
    PRIMARY KEY (NguoiDungId)
);

-- Bảng Lịch Sử Phát --
CREATE TABLE LichSuPhat (
    LichSuId nvarchar(15) NOT NULL,
    NguoiDungId nvarchar(15) NOT NULL,
    BaiHatId nvarchar(15) NOT NULL,
    PRIMARY KEY (LichSuId),
    CONSTRAINT FK_LichSuPhat_NguoiDung FOREIGN KEY (NguoiDungId) REFERENCES NguoiDung(NguoiDungId)
);

-- Bảng Thể Loại --
CREATE TABLE TheLoai (
    TheLoaiId nvarchar(15) NOT NULL,
    TenTheLoai nvarchar(25),
    PRIMARY KEY (TheLoaiId)
);

-- Bảng Nghệ Sỹ --
CREATE TABLE NgheSy (
    NgheSyId nvarchar(15) NOT NULL,
    TenNgheSy nvarchar(30),
    AnhDaiDien nvarchar(100),
    PRIMARY KEY (NgheSyId)
);

-- Bảng Album --
CREATE TABLE Album (
    AlbumId nvarchar(15) NOT NULL,
    TenAlbum nvarchar(30),
    AnhBia nvarchar(100),
    PRIMARY KEY (AlbumId)
);

-- Bảng BaiHat --
CREATE TABLE BaiHat (
    BaiHatId nvarchar(15) NOT NULL,
    TenBaiHat nvarchar(40),
    ThoiLuong Float,
    UrlFile nvarchar(100),
    AnhBia nvarchar(100),
    LuotNghe Int,
    NgheSyId nvarchar(15) NOT NULL,
    TheLoaiId nvarchar(15) NOT NULL,
    PRIMARY KEY (BaiHatId),
    CONSTRAINT FK_BaiHat_TheLoai FOREIGN KEY (TheLoaiId) REFERENCES TheLoai(TheLoaiId),
    CONSTRAINT FK_BaiHat_NgheSy FOREIGN KEY (NgheSyId) REFERENCES NgheSy(NgheSyId)
);

-- Bảng Playlist --
CREATE TABLE Playlist (
    PlaylistId nvarchar(15) NOT NULL,
    TieuDe nvarchar(30),
    NguoiSoHuuId nvarchar(15) NOT NULL,
    TrangThai nvarchar(10),
    AnhBia nvarchar(100),
    PRIMARY KEY (PlaylistId),
    CONSTRAINT FK_Playlist_NguoiDung FOREIGN KEY (NguoiSoHuuId) REFERENCES NguoiDung(NguoiDungId)
);

-- Bảng trung gian cho BaiHat và Playlist
CREATE TABLE ChiTietPlaylist (
    PlaylistId nvarchar(15) NOT NULL,
    BaiHatId nvarchar(15) NOT NULL,
    PRIMARY KEY (PlaylistId, BaiHatId),
    FOREIGN KEY (PlaylistId) REFERENCES Playlist(PlaylistId),
    FOREIGN KEY (BaiHatId) REFERENCES BaiHat(BaiHatId)
);

-- Bảng trung gian cho BaiHat và Album
CREATE TABLE ChiTietAlbum (
    AlbumId nvarchar(15) NOT NULL,
    BaiHatId nvarchar(15) NOT NULL,
    NgheSyId nvarchar(15) NOT NULL,
    PRIMARY KEY (AlbumId, BaiHatId),
	FOREIGN KEY (AlbumId) REFERENCES Album(AlbumId),
	FOREIGN KEY (BaiHatId) REFERENCES BaiHat(BaiHatId),
	FOREIGN KEY (NgheSyId) REFERENCES NgheSy(NgheSyId)
);

-- Bảng trung gian cho BaiHat yêu thích của NguoiDung
CREATE TABLE YeuThich (
    YeuThichId nvarchar(15) NOT NULL,
    BaiHatId nvarchar(15) NOT NULL,
    NguoiDungId nvarchar(15) NOT NULL,
    PRIMARY KEY (YeuThichId, NguoiDungId, BaiHatId),
    FOREIGN KEY (NguoiDungId) REFERENCES NguoiDung(NguoiDungId),
    FOREIGN KEY (BaiHatId) REFERENCES BaiHat(BaiHatId)
);

--Bảng lưu mã xác nhận để đặt lại mật khẩu
CREATE TABLE MaXacNhanDatLaiMatKhau (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NguoiDungId NVARCHAR(15) FOREIGN KEY (NguoiDungId) REFERENCES NguoiDung(NguoiDungId) NOT NULL,
    MaXacNhan NVARCHAR(50) NOT NULL,
    ThoiGianHetHan DATETIME NOT NULL,
    DaSuDung BIT DEFAULT 0,
);


-- Thêm dữ liệu cho admin
--password admin1234
INSERT INTO NguoiDung (NguoiDungId, Email, VaiTro, GioiTinh, AnhDaiDien, TrangThaiTK, NgayTao, TenDangNhap, MatKhau)
VALUES 
('AD001', 'admin@gmail.com', 'admin', 'Nam', 'ad_default.png', 'HoatDong', GETDATE(), 'admin01', '$2a$11$XpXMqUZbhUKJwjV8c7rEheXRaXuwHn7IeVvWtgy.3LBAVNGnhXAEi');

-- Thêm dữ liệu cho user
--password user1234
INSERT INTO NguoiDung (NguoiDungId, Email, VaiTro, GioiTinh, AnhDaiDien, TrangThaiTK, NgayTao, TenDangNhap, MatKhau)
VALUES 
('US001', 'user1@gmail.com', 'user', 'Nu', 'us_default.jpg', 'HoatDong', GETDATE(), 'user01', '$2a$11$jVfVCaVNm38h2Y0BTvATQOD6iOQxjh842N9fAAyi8rnrnpx5Q2RJS'),
('US002', 'user2@gmail.com', 'user', 'Nam', 'us_default.jpg', 'HoatDong', GETDATE(), 'user02', '$2a$11$jVfVCaVNm38h2Y0BTvATQOD6iOQxjh842N9fAAyi8rnrnpx5Q2RJS');
