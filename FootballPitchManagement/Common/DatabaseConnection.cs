using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FootballPitchManagement.Common
{
    /// <summary>
    /// Class quản lý kết nối database tập trung cho toàn dự án
    /// Mỗi thành viên sẽ có connection string riêng trong App.config local
    /// </summary>
    public static class DatabaseConnection
    {
        private static string _connectionString;

        /// <summary>
        /// Lấy connection string từ App.config
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    try
                    {
                        _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString;
                        
                        if (string.IsNullOrEmpty(_connectionString))
                        {
                            throw new InvalidOperationException(
                                "Connection string 'DefaultConnection' không tìm thấy trong App.config!\n\n" +
                                "Hướng dẫn:\n" +
                                "1. Copy file App.config.example thành App.config\n" +
                                "2. Sửa connection string cho phù hợp với máy của bạn"
                            );
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            $"❌ Lỗi đọc connection string:\n\n{ex.Message}",
                            "Lỗi cấu hình",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        throw;
                    }
                }
                return _connectionString;
            }
        }

        /// <summary>
        /// Tạo SqlConnection mới
        /// </summary>
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        /// <summary>
        /// Test kết nối database
        /// </summary>
        public static bool TestConnection(out string errorMessage)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    errorMessage = string.Empty;
                    return true;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Hiển thị dialog lỗi kết nối với hướng dẫn chi tiết
        /// </summary>
        public static void ShowConnectionError(string error)
        {
            string serverName = Environment.MachineName;
            
            MessageBox.Show(
                $"❌ KHÔNG THỂ KẾT NỐI DATABASE!\n\n" +
                $"Lỗi: {error}\n\n" +
                $"📋 HƯỚNG DẪN KHẮC PHỤC:\n\n" +
                $"1️⃣ Kiểm tra SQL Server đang chạy:\n" +
                $"   - Mở SQL Server Management Studio (SSMS)\n" +
                $"   - Kết nối với server: {serverName}\\SQLEXPRESS\n\n" +
                $"2️⃣ Tạo file App.config:\n" +
                $"   - Copy App.config.example → App.config\n" +
                $"   - Sửa connection string cho đúng\n\n" +
                $"3️⃣ Chạy SQL Scripts:\n" +
                $"   - SQL/01_CreateDatabase.sql\n" +
                $"   - SQL/02_CreateTables.sql\n" +
                $"   - SQL/03_SampleData.sql\n\n" +
                $"4️⃣ Kiểm tra tên Server:\n" +
                $"   - Tên máy của bạn: {serverName}\n" +
                $"   - Connection string phải đúng tên server",
                "Lỗi kết nối Database",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }
}