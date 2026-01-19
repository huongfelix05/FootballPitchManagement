USE QuanLychuoiDatSan;
GO

-- ================================================================
-- 1. DỌN DẸP SẠCH SẼ (XÓA HẾT KHÔNG CHỪA GÌ CẢ)
-- ================================================================
-- Tắt kiểm tra khóa ngoại tạm thời để xóa cho lẹ
EXEC sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'

-- Xóa dữ liệu các bảng
DELETE FROM ThanhToan;
DELETE FROM ChiTietHoaDonDoAn;
DELETE FROM ChiTietHoaDonSan;
DELETE FROM HoaDonDoAn;
DELETE FROM HoaDon;
DELETE FROM LichDatSan;
DELETE FROM DanhGia;
DELETE FROM GiaSan;
DELETE FROM KhoHang;
DELETE FROM ChiTietPhieuNhap;
DELETE FROM PhieuNhapHang;
DELETE FROM DanhMucHang;
DELETE FROM San;
DELETE FROM TaiKhoan; -- Xóa luôn admin cũ để tránh lỗi trùng
DELETE FROM KhachHang;
DELETE FROM NhomHang;
DELETE FROM NhaCungCap;
DELETE FROM TinhTrangSan;
DELETE FROM LoaiSan;
DELETE FROM LoaiTaiKhoan;
DELETE FROM ChiNhanh;

-- Reset bộ đếm ID về 0 (Để khi thêm mới nó bắt đầu từ 1)
DBCC CHECKIDENT ('San', RESEED, 0);
DBCC CHECKIDENT ('KhachHang', RESEED, 0);
DBCC CHECKIDENT ('HoaDon', RESEED, 0);
DBCC CHECKIDENT ('LichDatSan', RESEED, 0);
DBCC CHECKIDENT ('HoaDonDoAn', RESEED, 0);
DBCC CHECKIDENT ('ChiNhanh', RESEED, 0);
DBCC CHECKIDENT ('LoaiSan', RESEED, 0);
DBCC CHECKIDENT ('TinhTrangSan', RESEED, 0);
DBCC CHECKIDENT ('NhaCungCap', RESEED, 0);
DBCC CHECKIDENT ('NhomHang', RESEED, 0);
DBCC CHECKIDENT ('DanhMucHang', RESEED, 0);
DBCC CHECKIDENT ('TaiKhoan', RESEED, 0);
DBCC CHECKIDENT ('LoaiTaiKhoan', RESEED, 0);

-- Bật lại kiểm tra khóa ngoại
EXEC sp_msforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all'

PRINT N'✅ Đã dọn dẹp sạch sẽ 100%!';

-- ================================================================
-- 2. NHẬP LIỆU CẤU HÌNH (CHI NHÁNH, LOẠI, TÀI KHOẢN)
-- ================================================================

-- CHI NHÁNH (ID 1, 2, 3)
INSERT INTO ChiNhanh (TenChiNhanh, DiaChi, DienThoai, Email, NguoiQuanLy) VALUES
(N'Chi nhánh Quận 1', N'123 Nguyễn Huệ, Q1', '0901111111', 'q1@san.com', N'Nguyễn Văn A'),
(N'Chi nhánh Quận 7', N'456 Nguyễn Thị Thập, Q7', '0902222222', 'q7@san.com', N'Trần Văn B'),
(N'Chi nhánh Thủ Đức', N'789 Võ Văn Ngân, TĐ', '0903333333', 'td@san.com', N'Lê Văn C');

-- LOẠI SÂN & TÌNH TRẠNG
INSERT INTO LoaiSan (TenLoaiSan, SoNguoiToiDa, MoTa) VALUES (N'Sân 5', 10, N'Mini'), (N'Sân 7', 14, N'Vừa'), (N'Sân 11', 22, N'Lớn');
INSERT INTO TinhTrangSan (TenTinhTrang, MoTa) VALUES (N'Trống', N'Ok'), (N'Đang sử dụng', N'Busy'), (N'Bảo trì', N'Fixing');

-- KHÁCH HÀNG (ID 1, 2, 3)
INSERT INTO KhachHang (HoTen, DienThoai, Email, GioiTinh) VALUES
(N'Nguyễn Quốc Đạt', '0911111111', 'dat@gmail.com', N'Nam'),
(N'Trần Thị Lan', '0922222222', 'lan@gmail.com', N'Nữ'),
(N'Lê Quốc Bảo', '0933333333', 'bao@gmail.com', N'Nam');

-- TÀI KHOẢN (ID 1=Admin, 2=Nhân viên, 3=Khách)
INSERT INTO LoaiTaiKhoan (TenLoaiTK) VALUES (N'Admin'), (N'Nhân viên'), (N'Khách hàng');

INSERT INTO TaiKhoan (TenDangNhap, MatKhau, MaKH, MaLoaiTK, MaChiNhanh) VALUES
('admin', '123456', NULL, 1, NULL),       -- ID 1
('nhanvienq1', '123', NULL, 2, 1),        -- ID 2 (Nhân viên Q1)
('datkhach', '123', 1, 3, NULL);          -- ID 3

-- ================================================================
-- 3. NHẬP LIỆU SÂN & GIÁ (LOGIC CHUẨN)
-- ================================================================

-- SÂN BÓNG (ID 1, 2, 3...)
INSERT INTO San (TenSan, MaLoaiSan, MaChiNhanh, MaTinhTrang, GiaMacDinh, ThuTuHienThi) VALUES
(N'Sân Q1-A1 (Sân 5)', 1, 1, 1, 300000, 1), -- ID 1 (Q1)
(N'Sân Q1-A2 (Sân 5)', 1, 1, 1, 300000, 2), -- ID 2 (Q1)
(N'Sân Q1-VIP (Sân 7)', 2, 1, 1, 600000, 3),-- ID 3 (Q1)
(N'Sân Q7-B1 (Sân 5)', 1, 2, 1, 200000, 1), -- ID 4 (Q7)
(N'Sân Q7-PRO (Sân 7)', 2, 2, 1, 450000, 2),-- ID 5 (Q7)
(N'Sân TĐ-C1 (Sân 5)', 1, 3, 1, 150000, 1); -- ID 6 (Thủ Đức)

-- GIÁ SÂN (Khung giờ)
INSERT INTO GiaSan (MaSan, GioBatDau, GioKetThuc, GiaTien) VALUES
(1, '06:00', '10:00', 300000),
(1, '16:00', '22:00', 400000); -- Giờ vàng tăng giá

-- ================================================================
-- 4. NHẬP LIỆU GIAO DỊCH (ĐÃ KHỚP TIỀN)
-- ================================================================

-- LỊCH ĐẶT SÂN
-- Đơn 1: Khách 1 đá Sân 1 (300k) x 2h = 600k
INSERT INTO LichDatSan (MaKH, MaSan, NgayDat, GioBatDau, GioKetThuc, SoGio, TongTienSan, TrangThai)
VALUES (1, 1, '2026-01-16', '16:00', '18:00', 2, 600000, 'HOAN_THANH');

-- Đơn 2: Khách 2 đá Sân 4 (200k) x 2h = 400k
INSERT INTO LichDatSan (MaKH, MaSan, NgayDat, GioBatDau, GioKetThuc, SoGio, TongTienSan, TrangThai)
VALUES (2, 4, '2026-01-16', '18:00', '20:00', 2, 400000, 'HOAN_THANH');

-- HÓA ĐƠN (Khớp với Lịch đặt)
INSERT INTO HoaDon (MaDatSan, MaKH, MaChiNhanh, TongTienSan, ThanhTien, NguoiLap, TrangThaiThanhToan) VALUES
(1, 1, 1, 600000, 600000, 2, 'DA_THANH_TOAN'), -- NguoiLap=2 (nhanvienq1) -> OK
(2, 2, 2, 400000, 400000, 2, 'DA_THANH_TOAN');

-- CHI TIẾT HÓA ĐƠN SÂN
INSERT INTO ChiTietHoaDonSan (MaHoaDon, MaDatSan, DonGia, SoGio, ThanhTien) VALUES
(1, 1, 300000, 2, 600000),
(2, 2, 200000, 2, 400000);

-- THANH TOÁN
INSERT INTO ThanhToan (MaHoaDon, MaKH, SoTien, PhuongThuc) VALUES
(1, 1, 600000, N'TIỀN MẶT'),
(2, 2, 400000, N'CHUYỂN KHOẢN');

-- ================================================================
-- 5. NHẬP LIỆU KHO & ĐỒ ĂN
-- ================================================================
INSERT INTO NhaCungCap (TenNCC, DienThoai) VALUES (N'Coca Cola', '0909000001');
INSERT INTO NhomHang (TenNhom) VALUES (N'Nước uống'), (N'Đồ ăn');
INSERT INTO DanhMucHang (TenHang, DonViTinh, GiaNhap, GiaBan, MaNhomHang) VALUES
(N'Nước Suối', N'Chai', 5000, 10000, 1),
(N'Sting', N'Chai', 10000, 15000, 1);

INSERT INTO KhoHang (MaHang, MaChiNhanh, SoLuongTon) VALUES (1, 1, 100), (2, 1, 50);

-- Hóa đơn nước (Đã thanh toán)
INSERT INTO HoaDonDoAn (MaKH, MaChiNhanh, TongTien, TrangThai, NguoiLap) VALUES
(1, 1, 25000, 'DA_THANH_TOAN', 2);

INSERT INTO ChiTietHoaDonDoAn (MaHoaDonDoAn, MaHang, SoLuong, DonGia, ThanhTien) VALUES
(1, 1, 1, 10000, 10000), (1, 2, 1, 15000, 15000);

PRINT N'✅ ĐÃ NHẬP LIỆU THÀNH CÔNG RỰC RỠ!';