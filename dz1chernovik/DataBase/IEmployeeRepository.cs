using System;
using System.Collections.Generic;
using System.Text;

namespace dz1chernovik.DataBase
{
    public interface IEmployeeRepository
    {
        List<Sotrudnik> GetAll();
        Sotrudnik? GetById(int id);
        void AddSotrudnik(Sotrudnik item);
        void ChangeSotrudnik(int id, string name, DateOnly date);
        void DeleteSotrudnik(int id);
    }
}
