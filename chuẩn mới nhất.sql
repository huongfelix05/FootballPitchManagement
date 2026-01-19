
CREATE DATABASE QuanLychuoiDatSan;
GO
USE QuanLychuoiDatSan;
GO

-- 1. BẢNG CHI NHÁNH
CREATE TABLE ChiNhanh (
    MaChiNhanh INT PRIMARY KEY IDENTITY(1,1),
    TenChiNhanh NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(200) NOT NULL,
    DienThoai VARCHAR(15),
    Email VARCHAR(100),
    NguoiQuanLy NVARCHAR(100),
    TrangThai BIT DEFAULT 1, -- 1: Hoạt động, 0: Đóng cửa
    NgayTao DATETIME DEFAULT GETDATE(),
    CONSTRAINT UQ_ChiNhanh_Ten UNIQUE (TenChiNhanh)
);

-- 2. BẢNG LOẠI SÂN
CREATE TABLE LoaiSan (
    MaLoaiSan INT PRIMARY KEY IDENTITY(1,1),
    TenLoaiSan NVARCHAR(50) NOT NULL, -- Sân 5, Sân 7, Sân 11
    SoNguoiToiDa INT NOT NULL,
    MoTa NVARCHAR(200)
);

-- 3. BẢNG TÌNH TRẠNG SÂN
CREATE TABLE TinhTrangSan (
    MaTinhTrang INT PRIMARY KEY IDENTITY(1,1),
    TenTinhTrang NVARCHAR(50) NOT NULL, -- Trống, Đang sử dụng, Bảo trì
    MoTa NVARCHAR(200)
);


-- 4. BẢNG SÂN BÓNG
CREATE TABLE San (
    MaSan INT IDENTITY(1,1) PRIMARY KEY,

    -- Thông tin hiển thị
    TenSan NVARCHAR(50) NOT NULL,           -- Sân 5, Sân 7, Sân B1
              
                  

    -- Liên kết nghiệp vụ
    MaLoaiSan INT NOT NULL,                  -- Sân 5 / 7 / 11
    MaChiNhanh INT NOT NULL,

    -- Trạng thái
    MaTinhTrang INT NOT NULL DEFAULT 1,      -- Trống / Đang dùng / Bảo trì
    TrangThai BIT DEFAULT 1,                 -- 1: đang sử dụng, 0: ngừng khai thác

    -- Giá hiển thị nhanh (không tính toán)
    GiaMacDinh DECIMAL(10,2),                -- VD: 300000

    -- Hỗ trợ UI
    ThuTuHienThi INT DEFAULT 0,
    GhiChu NVARCHAR(200),

    NgayTao DATETIME DEFAULT GETDATE(),

    CONSTRAINT FK_San_LoaiSan 
        FOREIGN KEY (MaLoaiSan) REFERENCES LoaiSan(MaLoaiSan),

    CONSTRAINT FK_San_ChiNhanh 
        FOREIGN KEY (MaChiNhanh) REFERENCES ChiNhanh(MaChiNhanh),

    CONSTRAINT FK_San_TinhTrang 
        FOREIGN KEY (MaTinhTrang) REFERENCES TinhTrangSan(MaTinhTrang)
);



-- 5. BẢNG GIÁ SÂN
CREATE TABLE GiaSan (
    MaGia INT PRIMARY KEY IDENTITY(1,1),
    MaSan INT NOT NULL,
    GioBatDau TIME NOT NULL,
    GioKetThuc TIME NOT NULL,
    GiaTien DECIMAL(10,2) NOT NULL,
    NgayApDung DATE DEFAULT GETDATE(),
    TrangThai BIT DEFAULT 1,
    FOREIGN KEY (MaSan) REFERENCES San(MaSan),
    CONSTRAINT CHK_Gio CHECK (GioBatDau < GioKetThuc)
);

-- 6. BẢNG KHÁCH HÀNG
CREATE TABLE KhachHang (
    MaKH INT PRIMARY KEY IDENTITY(1,1),
    HoTen NVARCHAR(100) NOT NULL,
    DienThoai VARCHAR(15) NOT NULL,
    Email VARCHAR(100),
    GioiTinh NVARCHAR(10),
    NgaySinh DATE,
    DiaChi NVARCHAR(200),
    PhanLoaiKH NVARCHAR(20) DEFAULT 'THUONG', -- THUONG, THANTHIET, VIP
    DiemTichLuy INT DEFAULT 0,
    NgayDangKy DATETIME DEFAULT GETDATE(),
    TrangThai BIT DEFAULT 1,
    CONSTRAINT UQ_KhachHang_DienThoai UNIQUE (DienThoai),
    CONSTRAINT UQ_KhachHang_Email UNIQUE (Email)
);

-- 7. BẢNG LOẠI TÀI KHOẢN
CREATE TABLE LoaiTaiKhoan (
    MaLoaiTK INT PRIMARY KEY IDENTITY(1,1),
    TenLoaiTK NVARCHAR(50) NOT NULL, -- Admin, Quản lý, Nhân viên, Khách hàng
    MoTa NVARCHAR(200)
);

-- 8. BẢNG TÀI KHOẢN
CREATE TABLE TaiKhoan (
    MaTK INT PRIMARY KEY IDENTITY(1,1),
    TenDangNhap VARCHAR(50) NOT NULL,
    MatKhau VARCHAR(255) NOT NULL,
    MaKH INT,
    MaLoaiTK INT NOT NULL,
    MaChiNhanh INT, -- NULL nếu là Admin hoặc Khách hàng
    TrangThai BIT DEFAULT 1,
    NgayTao DATETIME DEFAULT GETDATE(),
    SoLanDangNhapSai INT DEFAULT 0,
    ThoiGianMoKhoa DATETIME NULL,
    CONSTRAINT UQ_TaiKhoan_TenDangNhap UNIQUE (TenDangNhap),
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
    FOREIGN KEY (MaLoaiTK) REFERENCES LoaiTaiKhoan(MaLoaiTK),
    FOREIGN KEY (MaChiNhanh) REFERENCES ChiNhanh(MaChiNhanh)
);

-- ============================================
-- BẢNG ĐẶT SÂN & HÓA ĐƠN
-- ============================================

-- 9. BẢNG LỊCH ĐẶT SÂN
CREATE TABLE LichDatSan (
    MaDatSan INT PRIMARY KEY IDENTITY(1,1),
    MaKH INT NOT NULL,
    MaSan INT NOT NULL,
    NgayDat DATE NOT NULL,
    GioBatDau TIME NOT NULL,
    GioKetThuc TIME NOT NULL,
    SoGio INT NOT NULL,
    TongTienSan DECIMAL(12,2) NOT NULL,
    TrangThai NVARCHAR(20) DEFAULT 'CHO_XAC_NHAN', -- CHO_XAC_NHAN, DA_XAC_NHAN, DA_HUY, HOAN_THANH
    GhiChu NVARCHAR(200),
    NgayTao DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
    FOREIGN KEY (MaSan) REFERENCES San(MaSan)
);

-- 10. BẢNG HÓA ĐƠN
CREATE TABLE HoaDon (
    MaHoaDon INT PRIMARY KEY IDENTITY(1,1),
    MaDatSan INT NOT NULL,
    MaKH INT NOT NULL,
    MaChiNhanh INT NOT NULL,
    NgayLap DATETIME DEFAULT GETDATE(),
    TongTienSan DECIMAL(12,2) NOT NULL,
    TongTienDoAn DECIMAL(12,2) DEFAULT 0,
    GiamGia DECIMAL(12,2) DEFAULT 0,
    VAT DECIMAL(5,2) DEFAULT 0,
    ThanhTien DECIMAL(12,2) NOT NULL,
    TrangThaiThanhToan NVARCHAR(20) DEFAULT 'CHUA_THANH_TOAN', -- CHUA, DA, HUY
    PhuongThucTT NVARCHAR(50), -- TIEN_MAT, CHUYEN_KHOAN, VI_DIEN_TU
    NguoiLap INT, -- MaTK của nhân viên
    FOREIGN KEY (MaDatSan) REFERENCES LichDatSan(MaDatSan),
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
    FOREIGN KEY (MaChiNhanh) REFERENCES ChiNhanh(MaChiNhanh),
    FOREIGN KEY (NguoiLap) REFERENCES TaiKhoan(MaTK)
);

-- 11. BẢNG CHI TIẾT HÓA ĐƠN SÂN
CREATE TABLE ChiTietHoaDonSan (
    MaCTHD INT PRIMARY KEY IDENTITY(1,1),
    MaHoaDon INT NOT NULL,
    MaDatSan INT NOT NULL,
    DonGia DECIMAL(10,2) NOT NULL,
    SoGio INT NOT NULL,
    ThanhTien DECIMAL(12,2) NOT NULL,
    FOREIGN KEY (MaHoaDon) REFERENCES HoaDon(MaHoaDon),
    FOREIGN KEY (MaDatSan) REFERENCES LichDatSan(MaDatSan)
);

-- ============================================
-- BẢNG BÁN HÀNG (ĐỒ ĂN/NƯỚC)
-- ============================================

-- 12. BẢNG NHÀ CUNG CẤP
CREATE TABLE NhaCungCap (
    MaNCC INT PRIMARY KEY IDENTITY(1,1),
    TenNCC NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(200),
    DienThoai VARCHAR(15),
    Email VARCHAR(100),
    MaSoThue VARCHAR(20),
    GhiChu NVARCHAR(200),
    TrangThai BIT DEFAULT 1
);

-- 13. BẢNG DANH MỤC HÀNG HÓA
CREATE TABLE DanhMucHang (
    MaHang INT PRIMARY KEY IDENTITY(1,1),
    MaNhomHang INT, -- Tham chiếu đến NhomHang (nếu có phân nhóm)
    TenHang NVARCHAR(100) NOT NULL,
    DonViTinh NVARCHAR(20) NOT NULL, -- Chai, Lon, Phần, Kg
    GiaNhap DECIMAL(10,2) NOT NULL,
    GiaBan DECIMAL(10,2) NOT NULL,
    HinhAnh VARCHAR(255),
    MoTa NVARCHAR(200),
    TrangThai BIT DEFAULT 1, -- 1: Còn bán, 0: Ngừng bán
    MaChiNhanh INT, -- NULL nếu áp dụng toàn hệ thống
    FOREIGN KEY (MaChiNhanh) REFERENCES ChiNhanh(MaChiNhanh)
);

-- 14. BẢNG NHÓM HÀNG
CREATE TABLE NhomHang (
    MaNhom INT PRIMARY KEY IDENTITY(1,1),
    TenNhom NVARCHAR(50) NOT NULL, -- Nước giải khát, Đồ ăn nhanh, Khác
    MoTa NVARCHAR(200)
);

-- 15. BẢNG KHO HÀNG
CREATE TABLE KhoHang (
    MaKho INT PRIMARY KEY IDENTITY(1,1),
    MaHang INT NOT NULL,
    MaChiNhanh INT NOT NULL,
    SoLuongTon INT DEFAULT 0,
    SoLuongToiThieu INT DEFAULT 10,
    HanSuDung DATE NULL,
    ViTriKho NVARCHAR(50),
    FOREIGN KEY (MaHang) REFERENCES DanhMucHang(MaHang),
    FOREIGN KEY (MaChiNhanh) REFERENCES ChiNhanh(MaChiNhanh),
    CONSTRAINT UQ_KhoHang_Hang_ChiNhanh UNIQUE (MaHang, MaChiNhanh)
);

-- 16. BẢNG HÓA ĐƠN ĐỒ ĂN
CREATE TABLE HoaDonDoAn (
    MaHoaDonDoAn INT PRIMARY KEY IDENTITY(1,1),
    MaKH INT NULL,
    MaDatSan INT NULL, -- NULL nếu khách mua lẻ không đặt sân
    MaChiNhanh INT NOT NULL,
    NgayLap DATETIME DEFAULT GETDATE(),
    TongTien DECIMAL(12,2) NOT NULL,
    TrangThai NVARCHAR(20) DEFAULT 'CHO_THANH_TOAN', -- CHO_THANH_TOAN, DA_THANH_TOAN, HUY
    GhiChu NVARCHAR(200),
    NguoiLap INT, -- MaTK của nhân viên
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
    FOREIGN KEY (MaDatSan) REFERENCES LichDatSan(MaDatSan),
    FOREIGN KEY (MaChiNhanh) REFERENCES ChiNhanh(MaChiNhanh),
    FOREIGN KEY (NguoiLap) REFERENCES TaiKhoan(MaTK)
);

-- 17. BẢNG CHI TIẾT HÓA ĐƠN ĐỒ ĂN
CREATE TABLE ChiTietHoaDonDoAn (
    MaCTHD INT PRIMARY KEY IDENTITY(1,1),
    MaHoaDonDoAn INT NOT NULL,
    MaHang INT NOT NULL,
    SoLuong INT NOT NULL,
    DonGia DECIMAL(10,2) NOT NULL, -- Giá tại thời điểm bán
    ThanhTien DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (MaHoaDonDoAn) REFERENCES HoaDonDoAn(MaHoaDonDoAn),
    FOREIGN KEY (MaHang) REFERENCES DanhMucHang(MaHang)
);

-- 18. BẢNG PHIẾU NHẬP HÀNG
CREATE TABLE PhieuNhapHang (
    MaPhieuNhap INT PRIMARY KEY IDENTITY(1,1),
    MaNCC INT NOT NULL,
    MaChiNhanh INT NOT NULL,
    NgayNhap DATETIME DEFAULT GETDATE(),
    TongTienNhap DECIMAL(12,2) NOT NULL,
    NguoiLap INT NOT NULL, -- MaTK của nhân viên
    TrangThai NVARCHAR(20) DEFAULT 'MOI_TAO', -- MOI_TAO, DA_DUYET, DA_NHAP_KHO
    GhiChu NVARCHAR(200),
    FOREIGN KEY (MaNCC) REFERENCES NhaCungCap(MaNCC),
    FOREIGN KEY (MaChiNhanh) REFERENCES ChiNhanh(MaChiNhanh),
    FOREIGN KEY (NguoiLap) REFERENCES TaiKhoan(MaTK)
);

-- 19. BẢNG CHI TIẾT PHIẾU NHẬP
CREATE TABLE ChiTietPhieuNhap (
    MaCTPN INT PRIMARY KEY IDENTITY(1,1),
    MaPhieuNhap INT NOT NULL,
    MaHang INT NOT NULL,
    SoLuong INT NOT NULL,
    DonGiaNhap DECIMAL(10,2) NOT NULL,
    ThanhTien DECIMAL(10,2) NOT NULL,
    HanSuDung DATE NULL,
    FOREIGN KEY (MaPhieuNhap) REFERENCES PhieuNhapHang(MaPhieuNhap),
    FOREIGN KEY (MaHang) REFERENCES DanhMucHang(MaHang)
);

-- ============================================
-- BẢNG THANH TOÁN & DOANH THU
-- ============================================

-- 20. BẢNG THANH TOÁN
CREATE TABLE ThanhToan (
    MaThanhToan INT PRIMARY KEY IDENTITY(1,1),
    MaHoaDon INT, -- Có thể là HoaDon sân hoặc HoaDonDoAn
    MaHoaDonDoAn INT,
    MaKH INT NOT NULL,
    SoTien DECIMAL(12,2) NOT NULL,
    PhuongThuc NVARCHAR(50) NOT NULL, -- TIEN_MAT, CHUYEN_KHOAN, VI_DIEN_TU
    TrangThai NVARCHAR(20) DEFAULT 'THANH_CONG', -- THANH_CONG, THAT_BAI, CHO_XU_LY
    MaGiaoDich VARCHAR(100), -- Mã giao dịch ngân hàng/ ví điện tử
    NgayThanhToan DATETIME DEFAULT GETDATE(),
    NguoiThucHien INT, -- MaTK của nhân viên
    FOREIGN KEY (MaHoaDon) REFERENCES HoaDon(MaHoaDon),
    FOREIGN KEY (MaHoaDonDoAn) REFERENCES HoaDonDoAn(MaHoaDonDoAn),
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
    FOREIGN KEY (NguoiThucHien) REFERENCES TaiKhoan(MaTK)
);

-- 21. BẢNG DOANH THU
CREATE TABLE DoanhThu (
    MaDoanhThu INT PRIMARY KEY IDENTITY(1,1),
    MaChiNhanh INT NOT NULL,
    Ngay DATE NOT NULL,
    LoaiDoanhThu NVARCHAR(20) NOT NULL, -- SAN, DO_AN, TONG
    SoTien DECIMAL(12,2) NOT NULL,
    GhiChu NVARCHAR(200),
    FOREIGN KEY (MaChiNhanh) REFERENCES ChiNhanh(MaChiNhanh)
);

-- ============================================
-- BẢNG HỆ THỐNG & LOG
-- ============================================

-- 22. BẢNG LOG ĐĂNG NHẬP
CREATE TABLE LogDangNhap (
    MaLog INT PRIMARY KEY IDENTITY(1,1),
    MaTK INT NOT NULL,
    ThoiGian DATETIME DEFAULT GETDATE(),
    IPAddress NVARCHAR(50),
    TrangThai NVARCHAR(20), -- THANH_CONG, THAT_BAI
    ThietBi NVARCHAR(100),
    FOREIGN KEY (MaTK) REFERENCES TaiKhoan(MaTK)
);

-- 23. BẢNG LOG ĐĂNG KÝ
CREATE TABLE LogDangKy (
    MaLog INT PRIMARY KEY IDENTITY(1,1),
    MaKH INT,
    ThoiGian DATETIME DEFAULT GETDATE(),
    IPAddress NVARCHAR(50),
    TrangThai NVARCHAR(20), -- THANH_CONG, THAT_BAI
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH)
);

-- 24. BẢNG RESET PASSWORD TOKENS
CREATE TABLE ResetPasswordTokens (
    MaToken INT PRIMARY KEY IDENTITY(1,1),
    MaKH INT NOT NULL,
    Token NVARCHAR(100) NOT NULL,
    ExpiryTime DATETIME NOT NULL,
    IsUsed BIT DEFAULT 0,
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH)
);

-- 25. BẢNG KHUYẾN MÃI
CREATE TABLE KhuyenMai (
    MaKhuyenMai INT PRIMARY KEY IDENTITY(1,1),
    TenKhuyenMai NVARCHAR(100) NOT NULL,
    MaChiNhanh INT, -- NULL nếu áp dụng toàn hệ thống
    LoaiKhuyenMai NVARCHAR(20), -- THEO_SAN, THEO_DO_AN, THEO_HOADON
    GiaTri DECIMAL(10,2), -- Số tiền hoặc phần trăm
    DonVi NVARCHAR(10), -- VND hoặc %
    DieuKienApDung NVARCHAR(200),
    NgayBatDau DATE NOT NULL,
    NgayKetThuc DATE NOT NULL,
    TrangThai BIT DEFAULT 1,
    FOREIGN KEY (MaChiNhanh) REFERENCES ChiNhanh(MaChiNhanh)
);

-- 26. BẢNG ĐÁNH GIÁ
CREATE TABLE DanhGia (
    MaDanhGia INT PRIMARY KEY IDENTITY(1,1),
    MaKH INT NOT NULL,
    MaSan INT,
    MaHang INT,
    Diem INT CHECK (Diem BETWEEN 1 AND 5),
    NoiDung NVARCHAR(500),
    NgayDanhGia DATETIME DEFAULT GETDATE(),
    TrangThai BIT DEFAULT 1,
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
    FOREIGN KEY (MaSan) REFERENCES San(MaSan),
    FOREIGN KEY (MaHang) REFERENCES DanhMucHang(MaHang)
);

-- ============================================
-- THÊM RÀNG BUỘC KHÓA NGOẠI SAU KHI TẠO BẢNG
-- ============================================

-- Thêm ràng buộc cho DanhMucHang
ALTER TABLE DanhMucHang 
ADD CONSTRAINT FK_DanhMucHang_NhomHang 
FOREIGN KEY (MaNhomHang) REFERENCES NhomHang(MaNhom);

-- Thêm ràng buộc cho San
ALTER TABLE San 
ADD CONSTRAINT FK_San_TinhTrangSan 
FOREIGN KEY (MaTinhTrang) REFERENCES TinhTrangSan(MaTinhTrang);


-- chạy từng tự từ trên xuống bản nhân viên add sao 
CREATE TABLE NhanVien (
    MaNV INT PRIMARY KEY IDENTITY(1,1),
    HoTen NVARCHAR(100) NOT NULL,
    DienThoai VARCHAR(15),
    Email VARCHAR(100),
    DiaChi NVARCHAR(200),
    NgayVaoLam DATE DEFAULT GETDATE(),
    TrangThai BIT DEFAULT 1, -- 1: Đang làm, 0: Đã nghỉ
    MaChiNhanh INT NULL,    -- Có thể để NULL nếu là nhân viên quản lý tổng
    CONSTRAINT FK_NhanVien_ChiNhanh FOREIGN KEY (MaChiNhanh) REFERENCES ChiNhanh(MaChiNhanh)
);

-- Thêm cột MaNV vào bảng TaiKhoan để liên kết hồ sơ nhân sự
ALTER TABLE TaiKhoan ADD MaNV INT NULL;
GO

-- Tạo ràng buộc khóa ngoại
ALTER TABLE TaiKhoan 
ADD CONSTRAINT FK_TaiKhoan_NhanVien FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV);
GO