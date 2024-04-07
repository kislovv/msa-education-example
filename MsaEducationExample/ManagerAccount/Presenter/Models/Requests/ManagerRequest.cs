namespace ManagerAccount.Presenter.Models.Requests;

public abstract class ManagerRequest
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string Fullname { get; set; }
}