using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SamuraiApp
{
    class Program
    {
        private static SamuraiDbContext _context = new SamuraiDbContext();
        static void Main(string[] args)
        {
            //InsertSamurai();
            //InsertMultipleSamurai();
            //QuerySamurai();
            //InsertDiffObj();

            //InsertMultipleSamuraisViaBatch();

            //UpdateSamurai();
            //UpdateMultipleSamurai();

            //UpdateDisconectedObj();
            DeleteSamurai();

            Console.ReadKey();
        }

        public static void InsertSamurai()
        {
            var samurai = new Samurai();
            samurai.Name = "Kire";

            var sam2 = new Samurai();
            sam2.Name = "Petar";

            using (var db = new SamuraiDbContext())
            {
                db.Samurais.Add(samurai);
                db.Add(sam2);

                db.SaveChanges();
            }
        }

        public static void InsertMultipleSamurai()
        {
            var list = new List<Samurai>();

            var samurai = new Samurai();
            samurai.Name = "Kire";

            var sam2 = new Samurai();
            sam2.Name = "Petar";

            list.Add(samurai);
            list.Add(sam2);
            using (var db = new SamuraiDbContext())
            {
                db.Samurais.AddRange(samurai, sam2);
                db.Samurais.AddRange(list);
                db.AddRange(samurai, sam2);

                db.SaveChanges();
            }
        }

        public static void QuerySamurai()
        {
            using (var db = new SamuraiDbContext())
            {
                var temp = "Kire";
                var samurais = db.Samurais.Where(s => s.Name == temp).ToList();
                foreach (var samurai in samurais)
                {
                    Console.WriteLine($"{samurai.Id} {samurai.Name}");
                }

                //var samurai = samurais.Where(s => s.Id == 1).Single();
                var sam = samurais.SingleOrDefault(s => s.Id == 1);
                Console.WriteLine($"Samurai: {sam.Id} {sam.Name}");
            }
        }

        public static void InsertDiffObj()
        {
            var sam = new Samurai
            {
                Name = "Name1"
            };
            var bat = new Battle
            {
                Name = "Belasica",
                StartDate = DateTime.Today.AddDays(-30),
                EndDate = DateTime.Today
            };
            using (var db = new SamuraiDbContext())
            {
                db.AddRange(sam, bat);
                db.SaveChanges();
            }
        }

        private static void SimpleSamuraiQuery()
        {
            using (var context = new SamuraiDbContext())
            {
                var samurais = context.Samurais.ToList();
                //var query = context.Samurais;
                //var samuraisAgain = query.ToList();
                foreach (var samurai in context.Samurais)
                {
                    Console.WriteLine(samurai.Name);
                }
            }
        }

        private static void InsertSamurai1()
        {
            var samurai = new Samurai { Name = "Kire" };
            using (var context = new SamuraiDbContext())
            {
                context.Samurais.Add(samurai);
                context.SaveChanges();
            }
        }


        private static void InsertMultipleSamurais()
        {
            var samurai = new Samurai { Name = "Kire" };
            var samuraiSammy = new Samurai { Name = "Sampson" };
            using (var context = new SamuraiDbContext())
            {
                context.Samurais.AddRange(samurai, samuraiSammy);
                context.SaveChanges();
            }
        }

        private static void InsertMultipleSamuraisViaBatch()
        {
            var samurai1 = new Samurai { Name = "Samurai1" };
            var samurai2 = new Samurai { Name = "Samurai2" };
            var samurai3 = new Samurai { Name = "Samurai3" };
            var samurai4 = new Samurai { Name = "Samurai4" };
            using (var context = new SamuraiDbContext())
            {
                context.Samurais.AddRange(samurai1, samurai2, samurai3, samurai4);
                context.SaveChanges();
            }
        }        

        private static void MoreQueries()
        {
            using (var context = new SamuraiDbContext())
            {
                var samurais_NonParameterizedQuery = context.Samurais.Where(s => s.Name == "Sampson").ToList();
                var name = "Sampson";
                var samurais_ParameterizedQuery = context.Samurais.Where(s => s.Name == name).ToList();
                var samurai_Object = context.Samurais.FirstOrDefault(s => s.Name == name);
                var samurais_ObjectFindByKeyValue = context.Samurais.Find(2);
                var samuraisJ = context.Samurais.Where(s => EF.Functions.Like(s.Name, "K%")).ToList();
                var search = "K%";
                var samuraisJParameter = context.Samurais.Where(s => EF.Functions.Like(s.Name, search)).ToList();
            }

        }

        private static void UpdateSamurai()
        {
            using(var db = new SamuraiDbContext())
            {
                db.Samurais.Add(new Samurai { Name = "Temp" });
                var s1 = db.Samurais.SingleOrDefault(s => s.Id == 1);
                if(s1 != null)
                {
                    s1.Name = "sen" + s1.Name;                    
                }
                db.SaveChanges();
            }
        }

        private static void UpdateMultipleSamurai()
        {
            using (var db = new SamuraiDbContext())
            {
                var samurais = db.Samurais.Where(s => EF.Functions.Like(s.Name, "S%")).ToList();
                foreach (var item in samurais)
                {
                    item.Name = "sen" + item.Name;
                }
                db.SaveChanges();
            }
        }

        private static void UpdateDisconectedObj()
        {
            Samurai s;
            using (var db = new SamuraiDbContext())
            {
                s = db.Samurais.Single(s => s.Id == 4);
            }

            s.Name = "Dis" + s.Name;
            using (var dbNew = new SamuraiDbContext())
            {
                dbNew.Samurais.Update(s);
                dbNew.SaveChanges();
            }
        }

        private static void LinqQuery()
        {
            using (var dbNew = new SamuraiDbContext())
            {
                var query = from s in dbNew.Samurais
                            where EF.Functions.Like(s.Name, "S%")
                            select s;

                var temp = query.ToList();
                                
            }
        }

        private static void DeleteSamurai()
        {
            Samurai s;
            using(var db = new SamuraiDbContext())
            {
                s = db.Samurais.First(s => EF.Functions.Like(s.Name, "S%"));                
            }

            using (var dbNew = new SamuraiDbContext())
            {
                dbNew.Samurais.Remove(s);
                dbNew.SaveChanges();
            }
        }

        private static void RetrieveAndUpdateSamurai()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name += "San";
            _context.SaveChanges();
        }

        private static void RetrieveAndUpdateMultipleSamurais()
        {
            var samurais = _context.Samurais.ToList();
            samurais.ForEach(s => s.Name += "San");
            _context.SaveChanges();
        }


        private static void QueryAndUpdateSamurai_Disconnected()
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name == "Kikuchiyo");
            samurai.Name += "San";
            using (var newContextInstance = new SamuraiDbContext())
            {
                newContextInstance.Samurais.Update(samurai);
                newContextInstance.SaveChanges();
            }
        }

        private static void InsertBattle()
        {
            _context.Battles.Add(new Battle
            {
                Name = "Battle of Okehazama",
                StartDate = new DateTime(1560, 05, 01),
                EndDate = new DateTime(1560, 06, 15)
            });
            _context.SaveChanges();
        }

        private static void QueryAndUpdateBattle_Disconnected()
        {
            var battle = _context.Battles.FirstOrDefault();
            battle.EndDate = new DateTime(1560, 06, 30);
            using (var newContextInstance = new SamuraiDbContext())
            {
                newContextInstance.Battles.Update(battle);
                newContextInstance.SaveChanges();
            }
        }



        private static void AddSomeMoreSamurais()
        {
            _context.AddRange(
               new Samurai { Name = "Kambei Shimada" },
               new Samurai { Name = "Shichirōji " },
               new Samurai { Name = "Katsushirō Okamoto" },
               new Samurai { Name = "Heihachi Hayashida" },
               new Samurai { Name = "Kyūzō" },
               new Samurai { Name = "Gorōbei Katayama" }
             );
            _context.SaveChanges();
        }

        private static void DeleteWhileTracked()
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name == "Kambei Shimada");
            _context.Samurais.Remove(samurai);
            //some alternates:
            // _context.Remove(samurai);
            // _context.Samurais.Remove(_context.Samurais.Find(1));
            _context.SaveChanges();
        }

        private static void DeleteWhileNotTracked()
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name == "Heihachi Hayashida");
            using (var contextNewAppInstance = new SamuraiDbContext())
            {
                contextNewAppInstance.Samurais.Remove(samurai);
                //contextNewAppInstance.Entry(samurai).State=EntityState.Deleted;
                contextNewAppInstance.SaveChanges();
            }
        }

        private static void DeleteMany()
        {
            var samurais = _context.Samurais.Where(s => s.Name.Contains("ō"));
            _context.Samurais.RemoveRange(samurais);
            //alternate: _context.RemoveRange(samurais);
            _context.SaveChanges();
        }

        private static void DeleteUsingId(int samuraiId)
        {
            var samurai = _context.Samurais.Find(samuraiId);
            _context.Remove(samurai);
            _context.SaveChanges();
            //alternate: call a stored procedure!
            //_context.Database.ExecuteSqlCommand("exec DeleteById {0}", samuraiId);
        }

    }
}
