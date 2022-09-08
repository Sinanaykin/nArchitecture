namespace Core.Persistence.Dynamic;

public class Dynamic
{
    public IEnumerable<Sort>? Sort { get; set; }//azalan artan yapabiliriz
    public Filter? Filter { get; set; }//Filtre koyabiliriz 

    public Dynamic()
    {
    }

    public Dynamic(IEnumerable<Sort>? sort, Filter? filter)
    {
        Sort = sort;
        Filter = filter;
    }
}