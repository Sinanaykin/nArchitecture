using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Brand:Entity //Bu sefer interfaceden değil class dan aldık amaç cıplak class kalmasın
    {
        public string Name { get; set; }

        public Brand()
        {

        }

        public Brand(int id,string name):this()//Bu classın parametresiz ctorunuda kullan diyorus this ile
        {
            Id = id;
            Name = name;
        }

    }
}
