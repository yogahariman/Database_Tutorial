using System.IO;
using System.Data.SqlClient;

namespace MyClass
{
    class MyDB
    {
        #region Variable
        private readonly string connectionString;
        #endregion

        #region Constructors

        // 1. Create Database Untuk Membuat Database Baru
        // 2. Drop Database untuk menghapus database
        public MyDB()
        {
            connectionString = @"Data Source =(LocalDB)\MSSQLLocalDB; Integrated Security=True;";
        }

        // untuk membuat class koneksi apabila database sudah ada
        public MyDB(string dbAddress)
        {
            string dbName = Path.GetFileNameWithoutExtension(dbAddress);
            connectionString =
                @"Data Source =(LocalDB)\MSSQLLocalDB;" +
                "Initial Catalog = " + dbName + ";" +
                "AttachDbFileName = " + dbAddress + ";" +
                "Integrated Security=True;";
        }
        public MyDB(string dbAddress, string dbName)
        {
            connectionString =
                @"Data Source =(LocalDB)\MSSQLLocalDB;" +
                "Initial Catalog = " + dbName + ";" +
                "AttachDbFileName = " + dbAddress + ";" +
                "Integrated Security=True;";
        }
        #endregion Constructors

        // Eksekusi SQL command
        public void ExecuteQuery(string queryString)
        {
            using (SqlConnection mySqlConn = new SqlConnection(this.connectionString))
            {
                mySqlConn.Open();
                using (SqlCommand myCommand = new SqlCommand(queryString, mySqlConn))
                {
                    myCommand.ExecuteNonQuery();
                }
            }
        }


        // Create Database
        // Example :
        // MyClass.MyDB myDB = new MyClass.MyDB();
        // myDB.CreateDB(System.Environment.CurrentDirectory+"//"+"test.mdf");
        public void CreateDB(string dbAdrress)
        {
            string dbName = Path.GetFileNameWithoutExtension(dbAdrress);
            //string dirProject = Path.GetDirectoryName(dbAdrress) + "\\";

            string queryString = @"CREATE DATABASE " + dbName + " ON PRIMARY " +
                            "(NAME = " + dbName + ", " +
                            "FILENAME = '" + dbAdrress + "')";
            ExecuteQuery(queryString);
        }

        // Drop/Delete Database
        // Example :
        // MyClass.MyDB myDB = new MyClass.MyDB();
        // myDB.DropDB("test");
        public void DropDB(string dbName)
        {
            string queryString = @"
                        ALTER DATABASE " + dbName + @" SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                        DROP DATABASE " + dbName;
            //MessageBox.Show(queryString, "", MessageBoxButtons.OK);
            ExecuteQuery(queryString);
        }

        // Create Table
        // Example :
        // MyClass.MyDB myDB = new MyClass.MyDB(System.Environment.CurrentDirectory + "//" + "test.mdf");
        // myDB.CreateTable();
        public void CreateTable()
        {
            string queryString=File.ReadAllText(@"MyClass/MyDB.sql");
            ExecuteQuery(queryString);
        }

        public void InsertDefaultValues()
        {
            string queryString = File.ReadAllText(@"MyClass/MyDBInsertDefaultValues.sql");
            ExecuteQuery(queryString);
        }

        /*public void InsertLogType()
        {
            string queryString =
                 @"INSERT INTO dbo.log_type(name) 
                   VALUES
                    ('Acoustic Impedance'),
                    ('Bit size'),
                    ('Bulk Modulus'),
                    ('Caliper'),
                    ('Clay Volume'),
                    ('Coal Volume'),
                    ('Density'),
                    ('Dolomite Volume'),
                    ('Effective Porosity'),
                    ('Gamma Ray'),
                    ('Limestone Volume'),
                    ('Lithology'),
                    ('Mu-Rho'),
                    ('Neutron Porosity'),
                    ('Permeability'),
                    ('P-Impedance'),
                    ('Poisson Ratio'),
                    ('Porosity'),
                    ('Pressure'),
                    ('P-wave'),
                    ('Resistivity'),
                    ('Sand Volume'),
                    ('Shale Volume'),
                    ('Shear Modulus'),
                    ('S-Impedance'),
                    ('Sonic'),
                    ('SP'),
                    ('S-wave'),
                    ('Temperature'),
                    ('Total Porosity'),
                    ('Uranium'),
                    ('Volumetric'),
                    ('Water Saturation'),
                    ('Young's Modulus')
                    ";
            ExecuteQuery(queryString);
        }*/

        /*public void InsertLithology()
        {
            string queryString = @"
                INSERT INTO dbo.lithology(name,code) 
                VALUES
                    ('None',0),
                    ('Conglomerate',10),
                    ('Grain supported conglomerate',11),
                    ('Muddy congl.',12),
                    ('Muddy sandy congl.',13),
                    ('Sandy congl.',14),
                    ('Conglomeratic sandstone',15),
                    ('Conglomeratic muddy sandstone',16),
                    ('Sedimentary breccia',20),
                    ('Sandstone',30),
                    ('Clayey sandstone',31),
                    ('Muddy sandstone',32),
                    ('Silty sandstone',33),
                    ('Siltstone',40),
                    ('Sandy siltstone',41),
                    ('Fossil siltstone',45),
                    ('Mudstone',50),
                    ('Sandy mudstone',51),
                    ('Conglomeratic mudstone',52),
                    ('Fissile mudstone',55),
                    ('Claystone',60),
                    ('Sandy claystone',61),
                    ('Silty claystone',62),
                    ('Shale',65),
                    ('Silty shale',66),
                    ('Limestone',70),
                    ('Dolomitic limestone',72),
                    ('Dolostone',74),
                    ('Calcareous dolostone',76),
                    ('Chalk',78),
                    ('Marl',80),
                    ('Gypsum',85),
                    ('Anhydrite',86),
                    ('Gypsum / Anhydrite unspecified',87),
                    ('Halite',88),
                    ('Salt general',89),
                    ('Coal',90),
                    ('Brown coal',91),
                    ('Volcanic rock gen.',92),
                    ('Intrusive rock gen.',93),
                    ('Silicic plutonic rocks',94),
                    ('Mafic plutonic rocks',95),
                    ('Dykes and sills gen.',96),
                    ('Metamorphic rocks gen.',97)
            ";

            ExecuteQuery(queryString);
        }*/

        // insert
        /*public void InsertWell(string wellName, string wellPath)
        {
            string queryString = @"INSERT INTO dbo.well(name,path) VALUES ('" + wellName + "," + wellPath + "')";
            ExecuteQuery(queryString);
        }*/

        /*public void InsertWellAttribute(int wellId, string curve, string unit, string name, int column)
        {
            string queryString = @"INSERT INTO dbo.well(well_id,curve,unit,name,column) VALUES ('" + wellId.ToString() + "," + curve + "," + unit + "," + name + "," + column.ToString() + "')";
            ExecuteQuery(queryString);
        }*/

        // untuk query select
        /*public List<List<string>> SelectQuery(string queryString)
        {
            this.queryString = queryString;
            SqlDataReader sReader = null;
            List<List<string>> data = new List<List<string>>();

            try
            {
                using (mySqlConn = new SqlConnection(connectionString))
                {

                    SqlCommand myCommand = new SqlCommand(this.queryString, mySqlConn);

                    mySqlConn.Open();
                    sReader = myCommand.ExecuteReader();

                    //// baris pertama
                    //sReader.Read();
                    //for (int i = 0; i < sReader.FieldCount; i++)
                    //{
                    //    data.Add(new List<string>());
                    //    data[i].Add(Convert.ToString(sReader[i]));
                    //}

                    // baris selanjutnya
                    int ind = 0;
                    while (sReader.Read())
                    {
                        if (ind == 0)
                        {
                            for (int i = 0; i < sReader.FieldCount; i++)
                            {
                                data.Add(new List<string>());
                            }
                        }
                        for (int i = 0; i < sReader.FieldCount; i++)
                        {
                            data[i].Add(Convert.ToString(sReader[i]));
                        }

                        ind++;
                    }
                }
            }
            finally
            {
                if (sReader != null) sReader.Close();
                if (mySqlConn.State == ConnectionState.Open) mySqlConn.Close();
            }

            return data;
        }*/

    }
}
