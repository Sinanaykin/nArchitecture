using Core.Persistence.Repositories;

namespace Core.Security.Entities;

public class OperationClaim : Entity
{
    public string Name { get; set; }//Kullanıcının rolü demek bu id ve name inin alıyoruz sadece

    public OperationClaim()
    {
    }

    public OperationClaim(int id, string name) : base(id)
    {
        Name = name;
    }
}