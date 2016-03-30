namespace CM3036_CW_1504693
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DBStudent : DbContext
    {
        // Your context has been configured to use a 'DBStudent' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'CM3036_CW_1504693.DBStudent' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DBStudent' 
        // connection string in the application configuration file.
        public DBStudent()
            : base(@"Data Source=(LocalDB)\v11.0;" +
               "attachdbfilename=C:\\Users\\user\\OneDrive\\RGU\\CM3 Computing (Application Software Development)\\CM3036 Programming in C#\\Coursework\\CM3036_CW_1504693\\CM3036_CW_1504693\\dbcm3036.cw.1504693.mdf;" +
               "Integrated Security=True;" +
               "Connect Timeout=15;" +
               "Encrypt=False;" +
               "TrustServerCertificate=False")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Student> Students { get; set; }
    }


    public class Student
    {
        public Student()
        {

        }
        public Int32 id { get; set; }
        public string firstName { get; set; }
        public string lastName  {  get; set; }
        public string matricNum { get; set; }
        public char gradeOne { get; set; }
        public char gradeTwo { get; set; }
        public char gradeThree { get; set; }
        public char gradeOverall { get; set; }
    }
}