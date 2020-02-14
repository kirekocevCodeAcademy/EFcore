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
            //DeleteSamurai();

            //SaveComplexSamurai();
            //AddQuote();

            //AddQuoteDisc();

            //GetSamuraiWithQuotes();
            //GetQuotes();

            //GetSamuraiQuote();
            //GetSamuraiwithSpecificQuote();


            //SamuraiWithQuoteAndSI();
            //AddBattleWithSamurai();
            //AddSamuraiToBattle();
            //AddSamuraiToBattleDisc();
            //GetSamurai();
            //SamuraiWithQoutes();
            //InsertBattleSamurai11();
            //UpdateBattle1SamuraiObjTrc();
            //UpdateBattle1SamuraiDiscScen();
            //UpdateBattle1SamuraiDiscScen();

            //GetSamuraiDom();

            UpdateDisconectedEntity();
            Console.ReadKey();
        }

        #region Part1
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
            using (var db = new SamuraiDbContext())
            {
                db.Samurais.Add(new Samurai { Name = "Temp" });
                var s1 = db.Samurais.SingleOrDefault(s => s.Id == 1);
                if (s1 != null)
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
            using (var db = new SamuraiDbContext())
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
        #endregion

        #region Part 2
        public static void SaveComplexSamurai()
        {
            var s = new Samurai();
            s.Name = "Kire";
            s.Quotes.Add(new Quote
            {
                Text = "Quote 1"
            });
            s.Quotes.Add(new Quote
            {
                Text = "Quote 2"
            });

            using (var db = new SamuraiDbContext())
            {
                db.Samurais.Add(s);
                db.SaveChanges();
            }
        }

        public static void AddQuote()
        {
            using (var db = new SamuraiDbContext())
            {
                var s = db.Samurais.Single(x => x.Id == 11);
                s.Quotes.Add(new Quote { Text = "Quote 3" });
                db.SaveChanges();
            }
        }

        public static void AddQuoteDisc()
        {
            var quote = new Quote
            {
                Text = "Quote 4",
                SamuraiId = 11
            };
            using (var db = new SamuraiDbContext())
            {
                db.Quotes.Add(quote);
                db.SaveChanges();
            }
        }

        public static void GetSamuraiWithQuotes()
        {
            using (var db = new SamuraiDbContext())
            {
                var samurais = db.Samurais.Include(s => s.Quotes).Where(s => EF.Functions.Like(s.Name, "K%")).ToList();
                foreach (var samurai in samurais)
                {
                    Console.WriteLine($"{samurai.Id} {samurai.Name}");
                    Console.WriteLine("--------------------------------------");
                    foreach (var quote in samurai.Quotes)
                    {
                        Console.WriteLine($"{quote.Id} {quote.Text} {quote.SamuraiId}");
                    }
                }
            }

        }

        public static void GetQuotes()
        {
            using (var db = new SamuraiDbContext())
            {
                var quotes = db.Quotes.Include(q => q.Samurai).Where(q => EF.Functions.Like(q.Text, "Q%")).ToList();
                foreach (var quote in quotes)
                {
                    Console.WriteLine($"Samuri: {quote.Samurai.Name} - {quote.Text}");
                }
            }
        }
        #endregion

        #region part 3

        public static void GetSamuraiQuote()
        {
            using (var db = new SamuraiDbContext())
            {
                var result = db.Samurais.Select(s => new { SamName = s.Name, QuotesCount = s.Quotes.Count }).ToList();
                foreach (var item in result)
                {
                    Console.WriteLine($"{item.SamName} : Count: {item.QuotesCount}");
                }
            }
        }

        public static void GetSamuraiQuote1()
        {
            using (var db = new SamuraiDbContext())
            {
                var result = db.Samurais.Select(s => new SamuraiQuote { SamName = s.Name, QuotesCount = s.Quotes.Count }).ToList();
                foreach (var item in result)
                {
                    Console.WriteLine($"{item.SamName} : Count: {item.QuotesCount}");
                }
            }
        }

        public static void GetSamuraiwithSpecificQuote()
        {
            using (var db = new SamuraiDbContext())
            {
                var result = db.Samurais
                    .Where(s => s.Quotes.Any(q => EF.Functions.Like(q.Text, "Q%")))
                    .Where(s => s.Name == "Kire")
                    .OrderBy(s => s.Quotes.Count)
                    .Select(s => new SamuraiQuote { SamName = s.Name, QuotesCount = s.Quotes.Count })
                    .OrderBy(s => s.SamName)
                    .ToList();
                foreach (var item in result)
                {
                    Console.WriteLine($"{item.SamName} : Count: {item.QuotesCount}");
                }
            }
        }

        private static void InsertNewPkFkGraph()
        {
            var samurai = new Samurai
            {
                Name = "Kambei Shimada",
                Quotes = new List<Quote>
                               {
                                 new Quote {Text = "I've come to save you"}
                               }
            };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static void InsertNewPkFkGraphMultipleChildren()
        {
            var samurai = new Samurai
            {
                Name = "Kyūzō",
                Quotes = new List<Quote> {
                  new Quote {Text = "Watch out for my sharp sword!"},
                  new Quote {Text="I told you to watch out for the sharp sword! Oh well!" }
                }
            };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static void AddChildToExistingObjectWhileTracked()
        {
            var samurai = _context.Samurais.First();
            samurai.Quotes.Add(new Quote
            {
                Text = "I bet you're happy that I've saved you!"
            });
            _context.SaveChanges();
        }

        private static void AddChildToExistingObjectWhileNotTracked(int samuraiId)
        {
            var quote = new Quote
            {
                Text = "Now that I saved you, will you feed me dinner?",
                SamuraiId = samuraiId
            };
            using (var newContext = new SamuraiDbContext())
            {
                newContext.Quotes.Add(quote);
                newContext.SaveChanges();
            }
        }

        private static void EagerLoadSamuraiWithQuotes()
        {
            var samuraiWithQuotes = _context.Samurais.Where(s => s.Name.Contains("Kyūzō"))
                                                     .Include(s => s.Quotes)
                                                     .Include(s => s.SecretIdentity)
                                                     .FirstOrDefault();
        }


        public struct IdAndName
        {
            public IdAndName(int id, string name)
            {
                Id = id;
                Name = name;
            }
            public int Id;
            public string Name;
        }
        private static void ProjectSomeProperties()
        {
            var someProperties = _context.Samurais.Select(s => new { s.Id, s.Name }).ToList();
            var idsAndNames = _context.Samurais.Select(s => new IdAndName(s.Id, s.Name)).ToList();
        }

        private static void ProjectSamuraisWithQuotes()
        {
            var somePropertiesWithQuotes = _context.Samurais
                .Select(s => new { s.Id, s.Name, s.Quotes.Count })
                .ToList();

        }


        private static void FilteringWithRelatedData()
        {
            var samurais = _context.Samurais
                                   .Where(s => s.Quotes.Any(q => q.Text.Contains("happy")))
                                   .ToList();
        }

        #endregion

        private static void SamuraiWithQuoteAndSI()
        {
            var samurai = new Samurai
            {
                Name = "Dirty Harry",
                Quotes = new List<Quote>
                {
                   new Quote { Text = "Having a bad day?"},
                   new Quote { Text = "Not so happy."},
                   new Quote { Text = "Do you feal lucky, punk?"}
                },
                SecretIdentity = new SecretIdentity { RealName = "Clint Eastwood" }
            };

            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static void AddBattleWithSamurai()
        {
            var battle = new Battle
            {
                Name = "Kung Fu",
                StartDate = DateTime.Today.AddMonths(-20),
                EndDate = DateTime.Today,
                SamuraiBattles = new List<SamuraiBattle>
                {
                    new SamuraiBattle
                    {
                        SamuraiId = 11,
                    },
                    new SamuraiBattle
                    {
                        Samurai = new Samurai
                        {
                            Name = "Kung Fu Fighter",
                            Quotes = new List<Quote>
                            {
                                new Quote{Text = "Just Happy"}
                            },
                            SecretIdentity = new SecretIdentity
                            {
                                RealName = "No name"
                            },
                            Battles = new List<SamuraiBattle>
                            {
                                new SamuraiBattle
                                {
                                    BattleId = 1
                                }
                            }
                        }
                    }
                }
            };

            _context.Battles.Add(battle);
            _context.SaveChanges();
        }

        public static void AddSamuraiToBattle()
        {
            var battle = _context.Battles.Single(x => x.Id == 1);
            battle.SamuraiBattles.Add(new SamuraiBattle
            {
                SamuraiId = 2
            });
            battle.SamuraiBattles.Add(new SamuraiBattle
            {
                SamuraiId = 10
            });

            _context.SaveChanges();
        }

        public static void AddSamuraiToBattleDisc()
        {
            _context.SamuraiBattle.AddRange(
                new SamuraiBattle
                {
                    SamuraiId = 7,
                    BattleId = 2,
                },
                new SamuraiBattle
                {
                    SamuraiId = 8,
                    BattleId = 2
                }
            );
            _context.SaveChanges();
        }

        public static void GetSamurai()
        {
            //var result = _context.Samurais
            //    .Include(s => s.Quotes)
            //    .Include(s => s.SecretIdentity)
            //    .Where(s => s.Battles.Any(b => b.Battle.Name == "Kung Fu"))
            //    .ToList();

            //foreach (var item in result)
            //{
            //    Console.WriteLine($"Samurai: {item.Name}");
            //    foreach (var quote in item.Quotes)
            //    {
            //        Console.WriteLine($"    {quote.Text}");
            //    }
            //    Console.WriteLine($"Real name: {item.SecretIdentity?.RealName}");
            //}


            //var result = _context.Samurais
            //    .Where(s => s.Battles.Any(b => b.Battle.Name == "Kung Fu"))
            //    .Select(s => new
            //    {
            //        s.Name,
            //        Quotes = s.Quotes.Select(q => new { q.Text }),
            //        SecretIdentity = new { s.SecretIdentity.RealName },
            //        BattlesCount = s.Battles.Count
            //    })
            //    .ToList();

            //foreach (var item in result)
            //{
            //    Console.WriteLine($"Samurai: {item.Name}");
            //    foreach (var quote in item.Quotes)
            //    {
            //        Console.WriteLine($"    {quote.Text}");
            //    }
            //    Console.WriteLine($"Real name: {item.SecretIdentity?.RealName}");
            //    Console.WriteLine($"In {item.BattlesCount} battles.");
            //}

            var result = _context.Samurais
                .Where(s => s.Battles.Count == 2)
                .Select(s => new
                {
                    s.Name,
                    SecretIdentity = s.SecretIdentity.RealName
                })
                .ToList();

            foreach (var item in result)
            {
                Console.WriteLine($"Samurai: {item.Name}");
                Console.WriteLine($"Real name: {item.SecretIdentity}");
            }
        }

        private static void SamuraiWithQoutes()
        {
            var samurai = new Samurai();
            samurai.Quotes.Add(new Quote
            {
                Text = "Qoute1"
            });
            samurai.Quotes.Add(new Quote
            {
                Text = "Qoute2"
            });
            samurai.Quotes.Add(new Quote
            {
                Text = "Qoute3"
            });
            samurai.Name = "Andrej";
            samurai.SecretIdentity = new SecretIdentity { RealName = "Jacki" };
            using (var db = new SamuraiDbContext())
            {
                db.Samurais.Add(samurai);
                db.SaveChanges();
            }
        }

        //Додаете нова битка во која учествувал самурајот со Id = 11 i еден нов самурај кој има Quote и  SecretIdentity а и учествувал во битката со id = 1
        private static void InsertBattleSamurai11()
        {
            var battle = new Battle { Name = "battle1" };
            battle.SamuraiBattles = new List<SamuraiBattle>
            {
                new SamuraiBattle{SamuraiId=11}
            };

            var samurai = new Samurai
            {
                Name = "Tayfun",
                Quotes = new List<Quote>
            {
                new Quote {Text="hello"}
            },
                SecretIdentity = new SecretIdentity { RealName = "samTayfun" }
            };
            samurai.Battles = new List<SamuraiBattle>
            {
                new SamuraiBattle{BattleId=1}
            };
            battle.SamuraiBattles.Add(new SamuraiBattle { Samurai = samurai });

            using (var db = new SamuraiDbContext())
            {
                db.Battles.Add(battle);
                db.SaveChanges();
            }
        }

        //Додадете на битката со id = 1 самураите со Id = 2 и Id = 10(со object tracking и discontinued scenario)

        private static void UpdateBattle1SamuraiObjTrc()
        {
            using (var db = new SamuraiDbContext())
            {
                Battle b = db.Battles.Single(b => b.Id == 1);
                b.SamuraiBattles = new List<SamuraiBattle>
                {
                    new SamuraiBattle{SamuraiId=14},
                    new SamuraiBattle{SamuraiId=7}
                };

                db.SaveChanges();
            }
        }

        private static void UpdateBattle1SamuraiDiscScen()
        {
            Battle b;
            using (var db = new SamuraiDbContext())
            {
                b = db.Battles.Single(b => b.Id == 3);

            }

            var samuraiBattles = new List<SamuraiBattle>
                {
                    new SamuraiBattle{SamuraiId=7, BattleId=3},
                    new SamuraiBattle{SamuraiId=14, BattleId=3}
                };

            using (var db2 = new SamuraiDbContext())
            {
                db2.SamuraiBattle.AddRange(samuraiBattles);
                db2.SaveChanges();
            }

        }


        public static void GetSamuraiDom()
        {
            //4. Испринтајте на екран: Сите самураи со нивните изреки и тајни идентитеити кои биле во битката со име = [Name]
            //var result = _context.Samurais
            //     .Include(s => s.Quotes)
            //     .Include(s => s.SecretIdentity)
            //     .Where(s => s.Battles.Any(b => b.Battle.Name == "Kung Fu"))
            //     .ToList();

            //foreach (var item in result)
            //{
            //    Console.WriteLine($"Samurai: {item.Name}");
            //    foreach (var quote in item.Quotes)
            //    {
            //        Console.WriteLine($"    {quote.Text}");
            //    }
            //    Console.WriteLine($"Real name: {item.SecretIdentity?.RealName}");
            //}

            //5. Испринтајте на екран: Само име на самурај од quote само quote и RealName од SecretIdentity и бројот на битки во кој бил
            //var result = _context.Samurais
            //    .Select(s => new
            //    {
            //        SamuraiName = s.Name,
            //        Quotes = s.Quotes.Select(q => q.Text),
            //        s.SecretIdentity.RealName,
            //        Battles = s.Battles.Count
            //    })
            //    .ToList();

            //foreach (var item in result)
            //{
            //    Console.WriteLine($"Samurai: {item.SamuraiName}");
            //    foreach (var quote in item.Quotes)
            //    {
            //        Console.WriteLine($"    {quote}");
            //    }
            //    Console.WriteLine($"Real name: {item.RealName}");
            //    Console.WriteLine($"Battles: {item.Battles}.");

            //    Console.WriteLine("-------------------------------");

            //}

            //6. Испринтајте на екран: само името на самурајот и неговиот таен идентитет кои учествувале во 2 битки
            var result = _context.Samurais
                .Where(s => s.Battles.Count == 2)
                .Select(s => new
                {
                    SamName = s.Name,
                    s.SecretIdentity.RealName
                }).ToList();


            foreach (var item in result)
            {
                Console.WriteLine($"Samurai: {item.SamName}");
                Console.WriteLine($"Real name: {item.RealName}");
                Console.WriteLine("-------------------------------");

            }
        }

        private static void UpdateDisconectedEntity()
        {
            var samurai = _context.Samurais.Include(s => s.Quotes).Single(s => s.Id == 14);

            var quote = samurai.Quotes.First();
            quote.Text += " New One";

            using(var dbNew = new SamuraiDbContext())
            {
                dbNew.Entry(quote).State = EntityState.Deleted;
                dbNew.SaveChanges();
            }

        }
    }

    internal class SamuraiQuote
    {
        public string SamName { get; set; }
        public int QuotesCount { get; set; }
    }
}
