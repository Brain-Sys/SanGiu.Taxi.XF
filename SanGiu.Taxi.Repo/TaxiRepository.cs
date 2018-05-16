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

        public List<d.Taxi> GetTaxiListForHomePage()
        {
            return conn.Table<d.Taxi>().OrderBy(t => t.Km).ToList();
        }

        public TaxiRepository(string filename)
        {
            try
            {
                conn = new SQLiteConnection(filename);
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

                int count = conn.Table<d.Taxi>().Count();

                if (count == 0)
                {
                    var list = new List<d.Taxi>();

                    for (int i = 0; i < 200000; i++)
                    {
                        list.Add(d.Taxi.CreateRandom());
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
    }
}