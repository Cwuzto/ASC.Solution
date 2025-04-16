using Microsoft.AspNetCore.Identity;


namespace ASC.Web.Areas.Accounts.Models
{
    public class ServiceEngineerViewModel
    {
        public List<IdentityUser>? ServiceEngineers { get; set; } //lưu trữ danh sách nhân viên
        public ServiceEngineerRegistrationViewModel Registration { get; set; } //lưu trữ nhân viên thêm mới hoặc cập nhật
    }
}
