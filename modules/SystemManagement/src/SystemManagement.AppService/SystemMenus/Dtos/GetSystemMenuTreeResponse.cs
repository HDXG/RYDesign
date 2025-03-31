namespace SystemManagement.AppService.SystemMenus.Dtos
{
    public class GetSystemMenuTreeResponse
    {
       public string label { get; set; }
       public string value { get; set; }
       public List<GetSystemMenuTreeResponse> children { get; set; }
    }
}
