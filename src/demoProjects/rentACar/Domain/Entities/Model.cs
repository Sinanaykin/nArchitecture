using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Model:Entity //Id zaten Entity nin içinde yani base class da oldugu için buraya prop olarak yazmıyoruz
    {
        public string  Name { get; set; }
        public decimal  DailyPrice{ get; set; }
        public string  ImageUrl{ get; set; }
        public virtual Brand? Brand { get; set; }//Virtual vermesek de olur burda
        public int  BrandId{ get; set; }


        public Model()
        {

        }

        public Model(int id, string name,decimal dailPrice,string imageUrl,int brandId) : this()//Bu classın yani base classın(Entity) parametresiz ctorunuda kullan diyorus this ile
        {
            Id = id;
            Name = name;
            DailyPrice= dailPrice;
            ImageUrl = imageUrl;
            BrandId = brandId;
            
        }
    }
}
