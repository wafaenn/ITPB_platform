namespace ITBS_Platform.Api.DTOs
{
    public class UserListDto
    {
        public int Id { get; set; }
        public string Nom { get; set; } = "";
        public string Prenom { get; set; } = "";
        public string Email { get; set; } = "";
        public int RoleId { get; set; }
        public string RoleName { get; set; } = "";
    }
}
