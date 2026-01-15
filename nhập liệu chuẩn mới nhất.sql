--dữ liệu bảng chi nhánh
INSERT INTO ChiNhanh (TenChiNhanh, DiaChi, DienThoai, Email, NguoiQuanLy)
VALUES
(N'Chi nhánh Quận 1', N'123 Nguyễn Huệ', '0901111111', 'q1@san.com', N'Nguyễn Văn A'),
(N'Chi nhánh Quận 7', N'456 Nguyễn Thị Thập', '0902222222', 'q7@san.com', N'Trần Văn B'),
(N'Chi nhánh Thủ Đức', N'789 Võ Văn Ngân', '0903333333', 'td@san.com', N'Lê Văn C');
--dữ liệu bảng loại sân
INSERT INTO LoaiSan (TenLoaiSan, SoNguoiToiDa, MoTa)
VALUES
(N'Sân 5', 10, N'Sân mini'),
(N'Sân 7', 14, N'Sân trung'),
(N'Sân 11', 22, N'Sân tiêu chuẩn');
---dữ liệu bảng tính trạng sân
INSERT INTO TinhTrangSan (TenTinhTrang, MoTa)
VALUES
(N'Trống', N'Có thể đặt'),
(N'Đang sử dụng', N'Có người đá'),
(N'Bảo trì', N'Không sử dụng');
---dữ liệu bảng sân
---INSERT INTO San (TenSan,  MaLoaiSan, MaChiNhanh, MaTinhTrang, GiaMacDinh)------------chưa chạy cái này 
--VALUES
--(N'Sân A1', 1, 1, 1, 300000),
--(N'Sân B1',  2, 1, 2, 500000),
--(N'Sân C1',  3, 2,2, 800000);
---dữ liệu bảng giá sân
INSERT INTO GiaSan (MaSan, GioBatDau, GioKetThuc, GiaTien)
VALUES
(1, '06:00', '10:00', 300000),
(1, '16:00', '18:00', 350000),
(2, '18:00', '22:00', 600000);
--dữ liệu bảng khách hàng
INSERT INTO KhachHang (HoTen, DienThoai, Email, GioiTinh)
VALUES
(N'Nguyễn Quốc Đạt', '0911111111', 'dat@gmail.com', N'Nam'),
(N'Trần Thị Lan', '0922222222', 'lan@gmail.com', N'Nữ'),
(N'Lê Quốc Bảo', '0933333333', 'bao@gmail.com', N'Nam');
---dữ liệu bảng loại tài khoản
INSERT INTO LoaiTaiKhoan (TenLoaiTK)
VALUES
(N'Admin'),
(N'Nhân viên'),
(N'Khách hàng');
---Tài khoản
INSERT INTO TaiKhoan (TenDangNhap, MatKhau, MaKH, MaLoaiTK, MaChiNhanh)
VALUES
('admin', '123456', NULL, 1, NULL),
('dat12', '123', NULL, 2, 1),
('kh01', '123456', 1, 3, NULL);
---lịch đặt sân
INSERT INTO LichDatSan (MaKH, MaSan, NgayDat, GioBatDau, GioKetThuc, SoGio, TongTienSan)
VALUES
(1, 1, '2025-01-10', '16:00', '18:00', 2, 600000),
(2, 2, '2025-01-11', '18:00', '20:00', 2, 1200000),
(3, 3, '2025-01-12', '17:00', '19:00', 2, 1600000);
--hóa đơn
INSERT INTO HoaDon (MaDatSan, MaKH, MaChiNhanh, TongTienSan, ThanhTien, NguoiLap)
VALUES
(1, 1, 1, 600000, 600000, 2),
(2, 2, 1, 1200000, 1200000, 2),
(3, 3, 2, 1600000, 1600000, 2);
---chi tiết hóa đơn
INSERT INTO ChiTietHoaDonSan (MaHoaDon, MaDatSan, DonGia, SoGio, ThanhTien)
VALUES
(1, 1, 300000, 2, 600000),
(2, 2, 600000, 2, 1200000),
(3, 3, 800000, 2, 1600000);
---nhà cung cấp
INSERT INTO NhaCungCap (TenNCC, DienThoai)
VALUES
(N'Coca Cola', '0909000001'),
(N'Pepsi', '0909000002'),
(N'Nhà cung cấp địa phương', '0909000003');
---nhóm hàng
INSERT INTO NhomHang (TenNhom)
VALUES
(N'Nước uống'),
(N'Đồ ăn'),
(N'Khác');
---danh mục hàng
INSERT INTO DanhMucHang (TenHang, DonViTinh, GiaNhap, GiaBan, MaNhomHang)
VALUES
(N'Nước suối', N'Chai', 5000, 10000, 1),
(N'Bò húc', N'Lon', 10000, 20000, 1),
(N'Mì ly', N'Phần', 7000, 15000, 2);
---kho hàng
INSERT INTO KhoHang (MaHang, MaChiNhanh, SoLuongTon)
VALUES
(1, 1, 100),
(2, 1, 80),
(3, 2, 50);
---hóa đơn đồ ăn
INSERT INTO HoaDonDoAn (MaKH, MaChiNhanh, TongTien, NguoiLap)
VALUES
(1, 1, 30000, 2),
(2, 1, 40000, 2),
(3, 2, 15000, 2);
---chi tiết hóa đơn đồ ăn
INSERT INTO ChiTietHoaDonDoAn (MaHoaDonDoAn, MaHang, SoLuong, DonGia, ThanhTien)
VALUES
(1, 1, 2, 10000, 20000),
(1, 2, 1, 20000, 20000),
(2, 3, 1, 15000, 15000);
---thanh toán
INSERT INTO ThanhToan (MaHoaDon, MaKH, SoTien, PhuongThuc)
VALUES
(1, 1, 600000, N'TIỀN MẶT'),
(2, 2, 1200000, N'CHUYỂN KHOẢN'),
(3, 3, 1600000, N'TIỀN MẶT');
---dánh giá
INSERT INTO DanhGia (MaKH, MaSan, Diem, NoiDung)
VALUES
(1, 1, 5, N'Sân rất tốt'),
(2, 2, 4, N'Giá hợp lý'),
(3, 3, 3, N'Sân hơi trơn');
---
USE QuanLychuoiDatSan;
GO

-- Thêm 5 hóa đơn đồ ăn đã thanh toán
INSERT INTO HoaDonDoAn (MaChiNhanh, NgayLap, TongTien, TrangThai, NguoiLap)
VALUES 
(1, '2026-01-10', 500000, 'DA_THANH_TOAN', 1), -- Q1 bán nước
(1, '2026-01-12', 300000, 'DA_THANH_TOAN', 1),
(2, '2026-01-11', 450000, 'DA_THANH_TOAN', 1), -- Q7 bán nước
(3, '2026-01-13', 200000, 'DA_THANH_TOAN', 1), -- Thủ Đức bán nước
(1, '2026-01-14', 600000, 'DA_THANH_TOAN', 1);



-- Thêm hóa đơn cho Tháng 3/2026
INSERT INTO HoaDon (MaDatSan, MaKH, MaChiNhanh, NgayLap, TongTienSan, ThanhTien, TrangThaiThanhToan)
VALUES 
(3, 3, 1, '2026-03-10', 5000000, 5000000, 'DA_THANH_TOAN');



--------------------- nhập liệu cho bảng sân theo chi nhánh sân cho dễ quản lí--------------
USE QuanLychuoiDatSan;
GO

-- =======================================================
-- 1. NHẬP LIỆU BỔ SUNG BẢNG SÂN (SAN) CHO 3 CHI NHÁNH
-- =======================================================
-- Lưu ý: Giả định MaChiNhanh: 1=Q1, 2=Q7, 3=Thủ Đức
--        Giả định MaLoaiSan: 1=Sân 5, 2=Sân 7, 3=Sân 11
--        Giả định MaTinhTrang: 1=Trống, 2=Đang dùng, 3=Bảo trì

-- --- CHI NHÁNH QUẬN 1 (Thêm 6 sân) ---
INSERT INTO San (TenSan,  MaLoaiSan, MaChiNhanh, MaTinhTrang, GiaMacDinh, ThuTuHienThi)
VALUES
(N'Sân Q1-A1 (Sân 5)',  1, 1, 1, 300000, 1), -- Trống
(N'Sân Q1-A2 (Sân 5)',  1, 1, 3, 300000, 2), -- BẢO TRÌ
(N'Sân Q1-A3(Sân 5)',   1, 1, 1, 300000, 3), -- Trống
(N'Sân Q1-A4 (Sân 5)',  1, 1, 1, 300000, 4), -- Trống
(N'Sân Q1-A5 (Sân 5)',  1, 1, 1, 300000, 4), -- Trống
(N'Sân Q1-A6(Sân 7)',   2, 1, 1, 500000, 5), -- Trống
(N'Sân Q1-A7(Sân 7)',   2, 1, 3, 500000, 6), -- BẢO TRÌ (Sẽ hiện màu xám)
(N'Sân Đại Q1-A8(Sân 11)', 3, 1, 1, 1200000, 7); -- Trống


-- --- CHI NHÁNH QUẬN 7 (Thêm 5 sân - Rộng rãi) ---
INSERT INTO San (TenSan, MaLoaiSan, MaChiNhanh, MaTinhTrang, GiaMacDinh, ThuTuHienThi)
VALUES
(N'Sân Q7-B1(Sân 5)',   1, 2, 1, 250000, 1),
(N'Sân Q7-B2(Sân 5)',   1, 2, 1, 250000, 2),
(N'Sân Q7-B3(Sân 5)',   1, 2, 1, 250000, 3),
(N'Sân Q7-B4(Sân 5)',   1, 2, 1, 250000, 4),
(N'Sân Q7-B5(Sân 5)',   1, 2, 3, 250000, 4),-- BẢO TRÌ
(N'Sân Q7-B6(Sân 7)',   2, 2, 1, 600000, 5),
(N'Sân Q7-B7(Sân 7)',   2, 2, 3, 600000, 6), -- BẢO TRÌ
(N'Sân Đại Q7-B8(Sân 11)', 3, 2, 1, 1500000, 7);

-- --- CHI NHÁNH THỦ ĐỨC (Thêm 5 sân - Giá sinh viên) ---
INSERT INTO San (TenSan, MaLoaiSan, MaChiNhanh, MaTinhTrang, GiaMacDinh, ThuTuHienThi)--chuus ý muốn thêm GhiChu
VALUES
(N'Sân TĐ-C1(Sân 5)',  1, 3, 1, 200000, 1),
(N'Sân TĐ-C2(Sân 5)',  1, 3, 1, 200000, 2),
(N'Sân TĐ-C3(Sân 5)',  1, 3, 1, 200000, 3),
(N'Sân TĐ-C4(Sân 5)',  1, 3, 1, 200000, 4),
(N'Sân TĐ-C5(Sân 7)',  2, 3, 1, 450000, 5),
(N'Sân TĐ-C6(Sân 7)',  2, 3, 3, 450000, 6),-- BẢO TRÌ
(N'Sân TĐ-C7(Sân 7)',  2, 3, 1, 450000, 7), 
(N'Sân TĐ-C8(Sân 11)', 3, 3, 3, 1300000, 8); -- BẢO TRÌ
--(N'Sân TĐ-C1(Sân 5)',  1, 3, 1, 200000, 1,N'sân mới');

-- =======================================================
-- 2. CẬP NHẬT BẢNG GIÁ SÂN (GIASAN)
-- =======================================================
-- Thêm giá cho các sân vừa tạo (Khung giờ vàng 17h-21h đắt hơn)
INSERT INTO GiaSan (MaSan, GioBatDau, GioKetThuc, GiaTien, TrangThai)
SELECT MaSan, '17:00', '21:00', GiaMacDinh + 100000, 1
FROM San
WHERE MaSan > 3; -- Áp dụng cho các sân mới thêm


