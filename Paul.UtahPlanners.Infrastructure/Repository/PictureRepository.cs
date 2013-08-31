using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtahPlanners.Domain.Contract.Repository;
using UtahPlanners.Infrastructure.DAO;
using UtahPlanners.Domain.Entity;

namespace UtahPlanners.Infrastructure.Repository
{
    public class PictureRepository : IPictureRepository
    {
        private PropertiesDB _context;

        public PictureRepository(PropertiesDB context)
        {
            _context = context;
        }

        public void Add(Picture picture)
        {
            _context.Pictures.Add(picture);
        }

        public Picture Get(int id)
        {
            return _context.Pictures.FirstOrDefault(p => p.id == id);
        }

        public void Remove(Picture picture)
        {
            _context.Pictures.Remove(picture);
        }

    }
}
