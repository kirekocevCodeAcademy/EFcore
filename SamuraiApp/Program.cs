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
        static void Main(string[] args)
        {
            //InsertSamurai();
            //InsertMultipleSamurai();
           QuerySamurai();
            //InsertDiffObj();



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
                var samuraisJ = context.Samurais.Where(s => EF.Functions.Like(s.Name, "J%")).ToList();
                var search = "J%";
                var samuraisJParameter = context.Samurais.Where(s => EF.Functions.Like(s.Name, search)).ToList();
            }

        }
    }
}
