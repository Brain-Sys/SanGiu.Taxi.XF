using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using d = SanGiu.Taxi.DomainModel;

namespace SanGiu.Taxi.Repo
{
    public class TaxiRepository
    {
        SQLiteConnection conn;

        public IQueryable<d.Taxi> Taxies
        {
            get
            {
                if (conn != null)
                {
                    return conn.Table<d.Taxi>().AsQueryable();
                }
                else
                {
                    return null;
                }
            }
        }

        public List<d.Taxi> GetTaxiListForHomePage(int minimumKm = 5000, int count = 500)
        {
            return conn.Table<d.Taxi>()
                .Where(t => t.Km >= minimumKm)
                //.OrderBy(t => t.Km)
                .Take(count)
                .ToList();
        }

        public TaxiRepository(string filename)
        {
            try
            {
                conn = new SQLiteConnection(filename);
                conn.Tracer = logSql;
                conn.Trace = true;

                var tm = conn.GetMapping<d.Taxi>(CreateFlags.AllImplicit);
                // tm.Columns.FirstOrDefault(c => c.Name == "Id").
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public void CheckDB()
        {
            try
            {
                conn.CreateTable<d.Taxi>();
                //conn.CreateTable<d.Taxi>(CreateFlags.AutoIncPK);

                int count = conn.Table<d.Taxi>().Count();

                if (count == 0)
                {
                    var list = new List<d.Taxi>();

                    for (int i = 0; i < 1000; i++)
                    {
                        var t = d.Taxi.CreateRandom();
                        t.Id = i + 1;
                        list.Add(t);
                    }

                    conn.BeginTransaction();
                    conn.InsertAll(list);
                    conn.Commit();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public bool Update(d.Taxi taxi)
        {
            conn.Update(taxi);

            return true;
        }

        private void logSql(string sql)
        {
            Debug.WriteLine(sql);
        }
    }
}