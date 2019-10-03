using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transation_RollBack.Context;
using Transation_RollBack.Models;

namespace Transation_RollBack
{
    class Program 
    {
        static void Main(string[] args)
        {
            using(var ctx = new DataContext())
            {
                using (var transation = ctx.Database.BeginTransaction())
                {
                    
                    ctx.People.Add(new People { Name = "Carlos" });
                    ctx.SaveChanges();

                    ctx.Employee.Add(new Employee { Name = "Develop Systems 1" });
                    ctx.SaveChanges();

                    if (ctx.People.ToList().Count() > 0) 
                    {
                        transation.Rollback();
                        throw new Exception("Transação cancelada!");
                    }

                    transation.Commit();                    
                }
            }
        }
    }
}
