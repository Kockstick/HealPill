using System.Collections.Generic;
using MySqlConnector;

namespace HealPill.Database;

public class DB
{
    public static string connStr = "server=10.10.1.24;user=user_01;database=pro9;port=3306;password=user01pro";
    public static Worker currentWorker;
    
    #region MedCard
    public static void New(MedCard medCard)
    {
        using (var conn = new MySqlConnection(connStr))
        { 
            conn.Open();
            string query = "INSERT INTO `MedCard` (Name, Phone, DateOfBorn) " +
                           "VALUES (@Name, @Phone, @DateOfBorn)";
            
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Name", medCard.Name);
                cmd.Parameters.AddWithValue("@Phone", medCard.Phone);
                cmd.Parameters.AddWithValue("@DateOfBorn", medCard.DateOfBorn);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }
    }
    
    public static void Update(MedCard medCard)
    {
        using (var conn = new MySqlConnection(connStr))
        {
            conn.Open();
            string query = "UPDATE `MedCard` SET Name = @Name, Phone = @Phone, " +
                           "DateOfBorn = @DateOfBorn WHERE Id = @Id";
            
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {                
                cmd.Parameters.AddWithValue("@Id", medCard.Id);
                cmd.Parameters.AddWithValue("@Name", medCard.Name);
                cmd.Parameters.AddWithValue("@Phone", medCard.Phone);
                cmd.Parameters.AddWithValue("@DateOfBorn", medCard.DateOfBorn);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }
    }
    
    public static List<MedCard> GetAllMedCards()
    {
        List<MedCard> medCards = new List<MedCard>();

        using (var conn = new MySqlConnection(connStr))
        {
            conn.Open();
            string query = "SELECT * FROM `MedCard`";

            using (var cmd = new MySqlCommand(query, conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MedCard medCard = new MedCard();
                        
                        medCard.Id = reader.GetInt32("Id");
                        medCard.Name = reader.GetString("Name");
                        medCard.Phone = reader.GetString("Phone");
                        medCard.DateOfBorn = reader.GetDateTime("DateOfBorn");

                        medCards.Add(medCard);
                    }
                }
            }

            conn.Close();
            return medCards;
        }
    }
    
    public static bool GetMedCardById(int Id, out MedCard medCard)
    {
        using (var conn = new MySqlConnection(connStr))
        {
            conn.Open();
            string query = "SELECT * FROM MedCard WHERE Id = @Id";
            
            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", Id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        medCard = new MedCard();
                        medCard.Id = reader.GetInt32("Id");
                        medCard.Name = reader.GetString("Name");
                        medCard.Phone = reader.GetString("Phone");
                        medCard.DateOfBorn = reader.GetDateTime("DateOfBorn");
                        return true;
                    }
                }
            }
        }

        medCard = null;
        return false;
    }
    
    public static MedCard GetMedCardById(int Id)
    {
        using (var conn = new MySqlConnection(connStr))
        {
            conn.Open();
            string query = "SELECT * FROM MedCard WHERE Id = @Id";
            
            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", Id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        MedCard medCard = new MedCard();
                        medCard.Id = reader.GetInt32("Id");
                        medCard.Name = reader.GetString("Name");
                        medCard.Phone = reader.GetString("Phone");
                        medCard.DateOfBorn = reader.GetDateTime("DateOfBorn");
                        return medCard;
                    }
                }
            }
        }

        return null;
    }
    #endregion

    #region Treatment
    public static void New(Treatment treatment)
    {
        if(treatment.MedCard == null)
            return;
        
        using (var conn = new MySqlConnection(connStr))
        { 
            conn.Open();
            string query = "INSERT INTO `Treatment` (Worker, Disease, DateRequest, DateVisit, DateGetRecept, DateEndHeal, MedCard) " +
                           "VALUES (@Worker, @Disease, @DateRequest, @DateVisit, @DateGetRecept, @DateEndHeal, @MedCard)";
            
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Worker", treatment.Worker.Id);
                if(treatment.Disease != null)
                    cmd.Parameters.AddWithValue("@Disease", treatment.Disease.Id);
                else
                    cmd.Parameters.AddWithValue("@Disease", 1);
                cmd.Parameters.AddWithValue("@DateRequest", treatment.DateRequest);
                cmd.Parameters.AddWithValue("@DateVisit", treatment.DateVisit);
                cmd.Parameters.AddWithValue("@DateGetRecept", treatment.DateGetRecept);
                cmd.Parameters.AddWithValue("@DateEndHeal", treatment.DateEndHeal);
                cmd.Parameters.AddWithValue("@MedCard", treatment.MedCard.Id);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }
    }
    
    public static void Update(Treatment treatment)
    {
        using (var conn = new MySqlConnection(connStr))
        {
            conn.Open();
            string query = "UPDATE `Treatment` SET Worker = @Worker, Disease = @Disease, " +
                           "DateRequest = @DateRequest, DateVisit = @DateVisit, " +
                           "DateGetRecept = @DateGetRecept, DateEndHeal = @DateEndHeal, " +
                           "MedCard = @MedCard WHERE Id = @Id";
            
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {                
                cmd.Parameters.AddWithValue("@Id", treatment.Id);
                cmd.Parameters.AddWithValue("@Worker", treatment.Worker.Id);
                if(treatment.Disease != null)
                    cmd.Parameters.AddWithValue("@Disease", treatment.Disease.Id);
                else
                    cmd.Parameters.AddWithValue("@Disease", 1);
                cmd.Parameters.AddWithValue("@DateRequest", treatment.DateRequest);
                cmd.Parameters.AddWithValue("@DateVisit", treatment.DateVisit);
                cmd.Parameters.AddWithValue("@DateGetRecept", treatment.DateGetRecept);
                cmd.Parameters.AddWithValue("@DateEndHeal", treatment.DateEndHeal);
                cmd.Parameters.AddWithValue("@MedCard", treatment.MedCard.Id);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }
    }
    
    public static void Delete(Treatment treatment)
    {
        using (var conn = new MySqlConnection(connStr))
        {
            conn.Open();
            string query = "DELETE FROM `Treatment` WHERE Id = @Id";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", treatment.Id);
                cmd.ExecuteNonQuery();
            }

            conn.Close();
        }
    }
    
    public static List<Treatment> GetAllTreatments()
    {
        List<Treatment> treatments = new List<Treatment>();

        using (var conn = new MySqlConnection(connStr))
        {
            conn.Open();
            string query = "SELECT * FROM `Treatment`";

            using (var cmd = new MySqlCommand(query, conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Treatment treatment = new Treatment();
                        
                        treatment.Id = reader.GetInt32("Id");
                        treatment.Worker = GetWorkerById(reader.GetInt32("Worker"));
                        treatment.Disease = GetDiseaseById(reader.GetInt32("Disease"));
                        treatment.DateRequest = reader.GetDateTime("DateRequest");
                        treatment.DateVisit = reader.GetDateTime("DateVisit");
                        treatment.DateGetRecept = reader.GetDateTime("DateGetRecept");
                        treatment.DateEndHeal = reader.GetDateTime("DateEndHeal");
                        treatment.MedCard = GetMedCardById(reader.GetInt32("MedCard"));

                        treatments.Add(treatment);
                    }
                }
            }

            conn.Close();
            return treatments;
        }
    }
    
    public static bool GetTreatmentById(int Id, out Treatment treatment)
    {
        using (var conn = new MySqlConnection(connStr))
        {
            conn.Open();
            string query = "SELECT * FROM Treatment WHERE Id = @Id";
            
            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", Id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        treatment = new Treatment();
                        treatment.Id = reader.GetInt32("Id");
                        treatment.Worker = GetWorkerById(reader.GetInt32("Worker"));
                        treatment.Disease = GetDiseaseById(reader.GetInt32("Disease"));
                        treatment.DateRequest = reader.GetDateTime("DateRequest");
                        treatment.DateVisit = reader.GetDateTime("DateVisit");
                        treatment.DateGetRecept = reader.GetDateTime("DateGetRecept");
                        treatment.DateEndHeal = reader.GetDateTime("DateEndHeal");
                        treatment.MedCard = GetMedCardById(reader.GetInt32("MedCard"));
                        return true;
                    }
                }
            }
        }

        treatment = null;
        return false;
    }
    
    public static List<Treatment> GetTreatmentsByMedCard(MedCard medCard)
    {
        List<Treatment> treatments = new List<Treatment>();
        using (var conn = new MySqlConnection(connStr))
        {
            conn.Open();
            string query = "SELECT * FROM Treatment WHERE MedCard = @MedCard";
            
            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MedCard", medCard.Id);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Treatment treatment = new Treatment();
                        treatment.Id = reader.GetInt32("Id");
                        treatment.Worker = GetWorkerById(reader.GetInt32("Worker"));
                        treatment.Disease = GetDiseaseById(reader.GetInt32("Disease"));
                        treatment.DateRequest = reader.GetDateTime("DateRequest");
                        treatment.DateVisit = reader.GetDateTime("DateVisit");
                        treatment.DateGetRecept = reader.GetDateTime("DateGetRecept");
                        treatment.DateEndHeal = reader.GetDateTime("DateEndHeal");
                        treatment.MedCard = GetMedCardById(reader.GetInt32("MedCard"));
                        treatments.Add(treatment);
                    }
                }
            }
        }

        return treatments;
    }
    #endregion

    #region Worker
    public static bool GetWorkerById(int Id, out Worker worker)
    {
        using (var conn = new MySqlConnection(connStr))
        {
            conn.Open();
            string query = "SELECT * FROM Worker WHERE Id = @Id";
            
            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", Id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        worker = new Worker();
                        worker.Id = reader.GetInt32("Id");
                        worker.Name = reader.GetString("Worker");
                        worker.Phone = reader.GetString("Disease");
                        worker.ServiceTime = reader.GetInt32("DateRequest");
                        worker.DateOfBorn = reader.GetDateTime("DateRequest");
                        worker.Login = reader.GetString("DateRequest");
                        worker.Password = reader.GetString("DateRequest");
                        return true;
                    }
                }
            }
        }

        worker = null;
        return false;
    }
    
    public static Worker GetWorkerById(int Id)
    {
        using (var conn = new MySqlConnection(connStr))
        {
            conn.Open();
            string query = "SELECT * FROM Worker WHERE Id = @Id";
            
            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", Id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Worker worker = new Worker();
                        worker.Id = reader.GetInt32("Id");
                        worker.Name = reader.GetString("Name");
                        worker.Phone = reader.GetString("Phone");
                        worker.ServiceTime = reader.GetInt32("ServiceTime");
                        worker.DateOfBorn = reader.GetDateTime("DateOfBorn");
                        worker.Login = reader.GetString("Login");
                        worker.Password = reader.GetString("Password");
                        return worker;
                    }
                }
            }
        }
        
        return null;
    }
    
    public static bool GetWorkerByLogin(string login, out Worker worker)
    {
        using (var conn = new MySqlConnection(connStr))
        {
            conn.Open();
            string query = "SELECT * FROM Worker WHERE Login = @Login";
            
            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Login", login);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        worker = new Worker();
                        worker.Id = reader.GetInt32("Id");
                        worker.Name = reader.GetString("Name");
                        worker.Phone = reader.GetString("Phone");
                        worker.ServiceTime = reader.GetInt32("ServiceTime");
                        worker.DateOfBorn = reader.GetDateTime("DateOfBorn");
                        worker.Login = reader.GetString("Login");
                        worker.Password = reader.GetString("Password");
                        return true;
                    }
                }
            }
        }

        worker = null;
        return false;
    }
    #endregion

    #region Disease
    public static Disease GetDiseaseById(int Id)
    {
        using (var conn = new MySqlConnection(connStr))
        {
            conn.Open();
            string query = "SELECT * FROM Disease WHERE Id = @Id";
            
            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", Id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Disease disease = new Disease();
                        disease.Id = reader.GetInt32("Id");
                        disease.Type = reader.GetString("Type");
                        disease.Name = reader.GetString("Name");
                        return disease;
                    }
                }
            }
        }
        
        return null;
    }
    
    public static List<Disease> GetAllDiseases()
    {
        List<Disease> diseases = new List<Disease>();

        using (var conn = new MySqlConnection(connStr))
        {
            conn.Open();
            string query = "SELECT * FROM `Disease`";

            using (var cmd = new MySqlCommand(query, conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Disease disease = new Disease();
                        
                        disease.Id = reader.GetInt32("Id");
                        disease.Type = reader.GetString("Type");
                        disease.Name = reader.GetString("Name");

                        diseases.Add(disease);
                    }
                }
            }

            conn.Close();
            return diseases;
        }
    }
    #endregion

    #region TimeTable
    public static List<TimeTable> GetAllTimeTable()
    {
        List<TimeTable> timeTables = new List<TimeTable>();

        using (var conn = new MySqlConnection(connStr))
        {
            conn.Open();
            string query = "SELECT * FROM `TimeTable`";

            using (var cmd = new MySqlCommand(query, conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TimeTable timeTable = new TimeTable();
                        
                        timeTable.Id = reader.GetInt32("Id");
                        timeTable.Worker = GetWorkerById(reader.GetInt32("Worker"));
                        timeTable.DayOfWeek = reader.GetString("DayOfWeek");
                        timeTable.Start = reader.GetTimeOnly("Start");
                        timeTable.End = reader.GetTimeOnly("End");

                        timeTables.Add(timeTable);
                    }
                }
            }

            conn.Close();
            return timeTables;
        }
    }
    
    public static bool GetTimeTablesByDay(string dayOfWeek, out List<TimeTable> timeTables)
    {
        timeTables = new List<TimeTable>();
        using (var conn = new MySqlConnection(connStr))
        {
            conn.Open();
            string query = "SELECT * FROM TimeTable WHERE DayOfWeek = @DayOfWeek";
            
            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@DayOfWeek", dayOfWeek);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TimeTable timeTable = new TimeTable();
                        timeTable.Id = reader.GetInt32("Id");
                        timeTable.Worker = GetWorkerById(reader.GetInt32("Worker"));
                        timeTable.DayOfWeek = reader.GetString("DayOfWeek");
                        timeTable.Start = reader.GetTimeOnly("Start");
                        timeTable.End = reader.GetTimeOnly("End");
                        timeTables.Add(timeTable);
                    }
                }
            }
        }

        return timeTables.Count != 0;
    }
    #endregion

    #region Medicines
    public static void New(Medicines medicines)
    {
        using (var conn = new MySqlConnection(connStr))
        { 
            conn.Open();
            string query = "INSERT INTO `Medicines` (Treatment, Medicine) " +
                           "VALUES (@Treatment, @Medicine)";
            
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Treatment", medicines.Treatment);
                cmd.Parameters.AddWithValue("@Medicine", medicines.Medicine.Id);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }
    }
    
    public static void Delete(Medicines medicines)
    {
        using (var conn = new MySqlConnection(connStr))
        {
            conn.Open();
            string query = "DELETE FROM `Medicines` WHERE Id = @Id";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", medicines.Id);
                cmd.ExecuteNonQuery();
            }

            conn.Close();
        }
    }
    
    public static List<Medicines> GetMedicines(Treatment treatment)
    {
        List<Medicines> medicines = new List<Medicines>();
        using (var conn = new MySqlConnection(connStr))
        {
            conn.Open();
            string query = "SELECT * FROM Medicines WHERE Treatment = @Treatment";
            
            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Treatment", treatment.Id);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Medicines med = new Medicines();
                        med.Id = reader.GetInt32("Id");
                        med.Treatment = reader.GetInt32("Treatment");
                        med.Medicine = GetMedicineById(reader.GetInt32("Medicine"));
                        medicines.Add(med);
                    }
                }
            }
        }
        
        return medicines;
    }
    
    public static Medicine GetMedicineById(int Id)
    {
        using (var conn = new MySqlConnection(connStr))
        {
            conn.Open();
            string query = "SELECT * FROM Medicine WHERE Id = @Id";
            
            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", Id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Medicine med = new Medicine();
                        med.Id = reader.GetInt32("Id");
                        med.Name = reader.GetString("Name");
                        med.Price = reader.GetFloat("Price");
                        return med;
                    }
                }
            }
        }
        
        return null;
    }
    
    public static List<Medicine> GetAllMedicine()
    {
        List<Medicine> medicines = new List<Medicine>();
        using (var conn = new MySqlConnection(connStr))
        {
            conn.Open();
            string query = "SELECT * FROM Medicine";
            
            using (var cmd = new MySqlCommand(query, conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Medicine med = new Medicine();
                        med.Id = reader.GetInt32("Id");
                        med.Name = reader.GetString("Name");
                        med.Price = reader.GetFloat("Price");
                        medicines.Add(med);
                    }
                }
            }
        }
        
        return medicines;
    }
    #endregion
}