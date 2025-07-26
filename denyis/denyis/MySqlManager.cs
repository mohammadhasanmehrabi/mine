using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
namespace denyis
{
    public class MySqlManager
    {
        private string connectionString = "server=localhost;database=dentist_db;uid=root;pwd=134713811355;";

        // CRUD برای جدول بیماران
        public int AddPatient(Patient patient)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("INSERT INTO patients (first_name, last_name, phone) VALUES (@FirstName, @LastName, @Phone); SELECT LAST_INSERT_ID();", conn);
                cmd.Parameters.AddWithValue("@FirstName", patient.FirstName);
                cmd.Parameters.AddWithValue("@LastName", patient.LastName);
                cmd.Parameters.AddWithValue("@Phone", patient.Phone);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public List<Patient> GetAllPatients()
        {
            var list = new List<Patient>();
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM patients", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Patient
                        {
                            Id = reader.GetInt32("id"),
                            FirstName = reader.GetString("first_name"),
                            LastName = reader.GetString("last_name"),
                            Phone = reader.GetString("phone"),
                            CreatedAt = reader.GetDateTime("created_at")
                        });
                    }
                }
            }
            return list;
        }

        public Patient GetPatientById(int id)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM patients WHERE id=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Patient
                        {
                            Id = reader.GetInt32("id"),
                            FirstName = reader.GetString("first_name"),
                            LastName = reader.GetString("last_name"),
                            Phone = reader.GetString("phone"),
                            CreatedAt = reader.GetDateTime("created_at")
                        };
                    }
                }
            }
            return null;
        }

        public void UpdatePatient(Patient patient)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("UPDATE patients SET first_name=@FirstName, last_name=@LastName, phone=@Phone WHERE id=@Id", conn);
                cmd.Parameters.AddWithValue("@FirstName", patient.FirstName);
                cmd.Parameters.AddWithValue("@LastName", patient.LastName);
                cmd.Parameters.AddWithValue("@Phone", patient.Phone);
                cmd.Parameters.AddWithValue("@Id", patient.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeletePatient(int id)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("DELETE FROM patients WHERE id=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }

        // CRUD برای visits
        public int AddVisit(Visit visit)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("INSERT INTO visits (patient_id, date_visit, date_record, date_test_teeth, date_test_general, date_delivery, notes) VALUES (@PatientId, @DateVisit, @DateRecord, @DateTestTeeth, @DateTestGeneral, @DateDelivery, @Notes); SELECT LAST_INSERT_ID();", conn);
                cmd.Parameters.AddWithValue("@PatientId", visit.PatientId);
                cmd.Parameters.AddWithValue("@DateVisit", visit.DateVisit);
                cmd.Parameters.AddWithValue("@DateRecord", visit.DateRecord);
                cmd.Parameters.AddWithValue("@DateTestTeeth", visit.DateTestTeeth);
                cmd.Parameters.AddWithValue("@DateTestGeneral", visit.DateTestGeneral);
                cmd.Parameters.AddWithValue("@DateDelivery", visit.DateDelivery);
                cmd.Parameters.AddWithValue("@Notes", visit.Notes);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public List<Visit> GetVisitsByPatientId(int patientId)
        {
            var list = new List<Visit>();
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM visits WHERE patient_id=@PatientId", conn);
                cmd.Parameters.AddWithValue("@PatientId", patientId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Visit
                        {
                            Id = reader.GetInt32("id"),
                            PatientId = reader.GetInt32("patient_id"),
                            DateVisit = reader.GetDateTime("date_visit"),
                            DateRecord = reader.GetDateTime("date_record"),
                            DateTestTeeth = reader.GetDateTime("date_test_teeth"),
                            DateTestGeneral = reader.GetDateTime("date_test_general"),
                            DateDelivery = reader.GetDateTime("date_delivery"),
                            UpdatedAt = reader.GetDateTime("updated_at"),
                            Notes = reader.GetString("notes")
                        });
                    }
                }
            }
            return list;
        }

        public void UpdateVisit(Visit visit)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("UPDATE visits SET date_visit=@DateVisit, date_record=@DateRecord, date_test_teeth=@DateTestTeeth, date_test_general=@DateTestGeneral, date_delivery=@DateDelivery, notes=@Notes WHERE id=@Id", conn);
                cmd.Parameters.AddWithValue("@DateVisit", visit.DateVisit);
                cmd.Parameters.AddWithValue("@DateRecord", visit.DateRecord);
                cmd.Parameters.AddWithValue("@DateTestTeeth", visit.DateTestTeeth);
                cmd.Parameters.AddWithValue("@DateTestGeneral", visit.DateTestGeneral);
                cmd.Parameters.AddWithValue("@DateDelivery", visit.DateDelivery);
                cmd.Parameters.AddWithValue("@Notes", visit.Notes);
                cmd.Parameters.AddWithValue("@Id", visit.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteVisit(int id)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("DELETE FROM visits WHERE id=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }

        // CRUD برای teeth
        public int AddTooth(Tooth tooth)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("INSERT INTO teeth (patient_id, tooth_name, tooth_type, unit_price, total_price, tooth_size, tooth_color) VALUES (@PatientId, @ToothName, @ToothType, @UnitPrice, @TotalPrice, @ToothSize, @ToothColor); SELECT LAST_INSERT_ID();", conn);
                cmd.Parameters.AddWithValue("@PatientId", tooth.PatientId);
                cmd.Parameters.AddWithValue("@ToothName", tooth.ToothName);
                cmd.Parameters.AddWithValue("@ToothType", tooth.ToothType);
                cmd.Parameters.AddWithValue("@UnitPrice", tooth.UnitPrice);
                cmd.Parameters.AddWithValue("@TotalPrice", tooth.TotalPrice);
                cmd.Parameters.AddWithValue("@ToothSize", tooth.ToothSize ?? "متوسط");
                cmd.Parameters.AddWithValue("@ToothColor", tooth.ToothColor ?? "A1");
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public List<Tooth> GetTeethByPatientId(int patientId)
        {
            var list = new List<Tooth>();
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM teeth WHERE patient_id=@PatientId", conn);
                cmd.Parameters.AddWithValue("@PatientId", patientId);
                using (var reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        int colToothSize = reader.GetOrdinal("tooth_size");
                        int colToothColor = reader.GetOrdinal("tooth_color");
                        list.Add(new Tooth
                        {
                            Id = reader.GetInt32("id"),
                            PatientId = reader.GetInt32("patient_id"),
                            ToothName = reader.GetString("tooth_name"),
                            ToothType = reader.GetString("tooth_type"),
                            UnitPrice = reader.GetDecimal("unit_price"),
                            TotalPrice = reader.GetDecimal("total_price"),
                            ToothSize = reader.IsDBNull(colToothSize) ? "متوسط" : reader.GetString(colToothSize),
                            ToothColor = reader.IsDBNull(colToothColor) ? "A1" : reader.GetString(colToothColor),
                            CreatedAt = reader.GetDateTime("created_at")
                        });
                    }
                }
            }
            return list;
        }

        public void UpdateTooth(Tooth tooth)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("UPDATE teeth SET tooth_name=@ToothName, tooth_type=@ToothType, unit_price=@UnitPrice, total_price=@TotalPrice, tooth_size=@ToothSize, tooth_color=@ToothColor WHERE id=@Id", conn);
                cmd.Parameters.AddWithValue("@ToothName", tooth.ToothName);
                cmd.Parameters.AddWithValue("@ToothType", tooth.ToothType);
                cmd.Parameters.AddWithValue("@UnitPrice", tooth.UnitPrice);
                cmd.Parameters.AddWithValue("@TotalPrice", tooth.TotalPrice);
                cmd.Parameters.AddWithValue("@ToothSize", tooth.ToothSize ?? "متوسط");
                cmd.Parameters.AddWithValue("@ToothColor", tooth.ToothColor ?? "A1");
                cmd.Parameters.AddWithValue("@Id", tooth.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteTooth(int id)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("DELETE FROM teeth WHERE id=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }

        // CRUD برای payments
        public int AddPayment(Payment payment)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("INSERT INTO payments (patient_id, payment_type, amount, paid_at, cheque_count, cheque_number, cheque_date, notes) VALUES (@PatientId, @PaymentType, @Amount, @PaidAt, @ChequeCount, @ChequeNumber, @ChequeDate, @Notes); SELECT LAST_INSERT_ID();", conn);
                cmd.Parameters.AddWithValue("@PatientId", payment.PatientId);
                cmd.Parameters.AddWithValue("@PaymentType", payment.PaymentType);
                cmd.Parameters.AddWithValue("@Amount", payment.Amount);
                cmd.Parameters.AddWithValue("@PaidAt", payment.PaidAt);
                cmd.Parameters.AddWithValue("@ChequeCount", payment.ChequeCount);
                cmd.Parameters.AddWithValue("@ChequeNumber", payment.ChequeNumber);
                cmd.Parameters.AddWithValue("@ChequeDate", payment.ChequeDate);
                cmd.Parameters.AddWithValue("@Notes", payment.Notes);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public List<Payment> GetPaymentsByPatientId(int patientId)
        {
            var list = new List<Payment>();
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM payments WHERE patient_id=@PatientId", conn);
                cmd.Parameters.AddWithValue("@PatientId", patientId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Payment
                        {
                            Id = reader.GetInt32("id"),
                            PatientId = reader.GetInt32("patient_id"),
                            PaymentType = reader.GetString("payment_type"),
                            Amount = reader.GetDecimal("amount"),
                            PaidAt = reader.GetDateTime("paid_at"),
                            ChequeCount = reader.GetInt32("cheque_count"),
                            ChequeNumber = reader.GetString("cheque_number"),
                            ChequeDate = reader.GetDateTime("cheque_date"),
                            Notes = reader.GetString("notes")
                        });
                    }
                }
            }
            return list;
        }

        public void UpdatePayment(Payment payment)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("UPDATE payments SET payment_type=@PaymentType, amount=@Amount, paid_at=@PaidAt, cheque_count=@ChequeCount, cheque_number=@ChequeNumber, cheque_date=@ChequeDate, notes=@Notes WHERE id=@Id", conn);
                cmd.Parameters.AddWithValue("@PaymentType", payment.PaymentType);
                cmd.Parameters.AddWithValue("@Amount", payment.Amount);
                cmd.Parameters.AddWithValue("@PaidAt", payment.PaidAt);
                cmd.Parameters.AddWithValue("@ChequeCount", payment.ChequeCount);
                cmd.Parameters.AddWithValue("@ChequeNumber", payment.ChequeNumber);
                cmd.Parameters.AddWithValue("@ChequeDate", payment.ChequeDate);
                cmd.Parameters.AddWithValue("@Notes", payment.Notes);
                cmd.Parameters.AddWithValue("@Id", payment.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeletePayment(int id)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("DELETE FROM payments WHERE id=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }

        // CRUD برای cases
        public int AddCase(Case c)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("INSERT INTO cases (patient_id, status, description, visit_reason, last_update) VALUES (@PatientId, @Status, @Description, @VisitReason, @LastUpdate); SELECT LAST_INSERT_ID();", conn);
                cmd.Parameters.AddWithValue("@PatientId", c.PatientId);
                cmd.Parameters.AddWithValue("@Status", c.Status);
                cmd.Parameters.AddWithValue("@Description", c.Description);
                cmd.Parameters.AddWithValue("@VisitReason", c.VisitReason);
                cmd.Parameters.AddWithValue("@LastUpdate", c.LastUpdate);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public List<Case> GetCasesByPatientId(int patientId)
        {
            var list = new List<Case>();
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM cases WHERE patient_id=@PatientId", conn);
                cmd.Parameters.AddWithValue("@PatientId", patientId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Case
                        {
                            Id = reader.GetInt32("id"),
                            PatientId = reader.GetInt32("patient_id"),
                            Status = reader.GetString("status"),
                            Description = reader.GetString("description"),
                            //VisitReason = reader.IsDBNull("visit_reason") ? "" : reader.GetString("visit_reason"),
                            LastUpdate = reader.GetDateTime("last_update")
                        });
                    }
                }
            }
            return list;
        }

        public void UpdateCase(Case c)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("UPDATE cases SET status=@Status, description=@Description, visit_reason=@VisitReason, last_update=@LastUpdate WHERE id=@Id", conn);
                cmd.Parameters.AddWithValue("@Status", c.Status);
                cmd.Parameters.AddWithValue("@Description", c.Description);
                cmd.Parameters.AddWithValue("@VisitReason", c.VisitReason);
                cmd.Parameters.AddWithValue("@LastUpdate", c.LastUpdate);
                cmd.Parameters.AddWithValue("@Id", c.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteCase(int id)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("DELETE FROM cases WHERE id=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }

        // CRUD برای images
        public int AddImage(Image img)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("INSERT INTO images (patient_id, description, image_data) VALUES (@PatientId, @Description, @ImageData); SELECT LAST_INSERT_ID();", conn);
                cmd.Parameters.AddWithValue("@PatientId", img.PatientId);
                cmd.Parameters.AddWithValue("@Description", img.Description);
                cmd.Parameters.AddWithValue("@ImageData", img.ImageData);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public List<Image> GetImagesByPatientId(int patientId)
        {
            var list = new List<Image>();
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM images WHERE patient_id=@PatientId", conn);
                cmd.Parameters.AddWithValue("@PatientId", patientId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Image
                        {
                            Id = reader.GetInt32("id"),
                            PatientId = reader.GetInt32("patient_id"),
                            Description = reader.GetString("description"),
                            ImageData = (byte[])reader["image_data"],
                            CreatedAt = reader.GetDateTime("created_at")
                        });
                    }
                }
            }
            return list;
        }

        public void DeleteImage(int id)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("DELETE FROM images WHERE id=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }

        // متد جستجو با join همه جداول بر اساس patient_id
        public Dictionary<string, object> GetFullPatientInfo(int patientId)
        {
            var result = new Dictionary<string, object>();
            result["patient"] = GetPatientById(patientId);
            result["visits"] = GetVisitsByPatientId(patientId);
            result["teeth"] = GetTeethByPatientId(patientId);
            result["payments"] = GetPaymentsByPatientId(patientId);
            result["cases"] = GetCasesByPatientId(patientId);
            result["images"] = GetImagesByPatientId(patientId);
            return result;
        }

        // جستجو بر اساس اسم یا فامیل (و برگرداندن همه اطلاعات)
        public List<Dictionary<string, object>> SearchPatients(string keyword)
        {
            var patients = new List<Patient>();
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM patients WHERE first_name LIKE @kw OR last_name LIKE @kw", conn);
                cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        patients.Add(new Patient
                        {
                            Id = reader.GetInt32("id"),
                            FirstName = reader.GetString("first_name"),
                            LastName = reader.GetString("last_name"),
                            Phone = reader.GetString("phone"),
                            CreatedAt = reader.GetDateTime("created_at")
                        });
                    }
                }
            }
            var result = new List<Dictionary<string, object>>();
            foreach (var p in patients)
            {
                result.Add(GetFullPatientInfo(p.Id));
            }
            return result;
        }
    }
}
