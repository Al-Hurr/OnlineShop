
namespace OnlineShop.Library.UserManagmentService
{
    public class UserManagmentServiceResponse<T>
    {
        public T Payload { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
