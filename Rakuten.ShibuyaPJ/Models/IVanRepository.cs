using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rakuten.ShibuyaPJ.Models
{
    public interface IVanRepository
    {
        string GetFormatedAddress(string lat, string lng);
        
        //IEnumerable GetAll();
        //Van GetByID(object id);
        //void Insert(Van obj);
        //void Update(Van obj);
        //void Delete(object id);
        //void Delete(Van obj);
        //void Save();
    }
}
