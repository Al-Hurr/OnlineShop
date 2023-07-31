
namespace OnlineShop.Library.UserManagmentService.Responses
{
    public class UserManagmentServiceResponse<T>
    {
        public T Payload { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
