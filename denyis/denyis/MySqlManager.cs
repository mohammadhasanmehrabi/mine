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
                var cmd = new MySqlCommand(@"INSERT INTO teeth (patient_id, tooth_name, tooth_type, unit_price, total_price, tooth_size, tooth_color,
                                   base_fracture_top, base_fracture_bottom, soft_layer_top, soft_layer_bottom, hard_red_layer_top, hard_red_layer_bottom,
                                   hard_clear_layer_top, hard_clear_layer_bottom, saksion, price_base_fracture, price_soft_layer, price_hard_red_layer,
                                   price_hard_clear_layer, price_saksion) 
                                   VALUES (@PatientId, @ToothName, @ToothType, @UnitPrice, @TotalPrice, @ToothSize, @ToothColor,
                                   @BaseFractureTop, @BaseFractureBottom, @SoftLayerTop, @SoftLayerBottom, @HardRedLayerTop, @HardRedLayerBottom,
                                   @HardClearLayerTop, @HardClearLayerBottom, @Saksion, @PriceBaseFracture, @PriceSoftLayer, @PriceHardRedLayer,
                                   @PriceHardClearLayer, @PriceSaksion); SELECT LAST_INSERT_ID();", conn);
                cmd.Parameters.AddWithValue("@PatientId", tooth.PatientId);
                cmd.Parameters.AddWithValue("@ToothName", tooth.ToothName);
                cmd.Parameters.AddWithValue("@ToothType", tooth.ToothType);
                cmd.Parameters.AddWithValue("@UnitPrice", tooth.UnitPrice);
                cmd.Parameters.AddWithValue("@TotalPrice", tooth.TotalPrice);
                cmd.Parameters.AddWithValue("@ToothSize", tooth.ToothSize ?? "متوسط");
                cmd.Parameters.AddWithValue("@ToothColor", tooth.ToothColor ?? "A1");
                cmd.Parameters.AddWithValue("@BaseFractureTop", tooth.BaseFractureTop ?? "");
                cmd.Parameters.AddWithValue("@BaseFractureBottom", tooth.BaseFractureBottom ?? "");
                cmd.Parameters.AddWithValue("@SoftLayerTop", tooth.SoftLayerTop);
                cmd.Parameters.AddWithValue("@SoftLayerBottom", tooth.SoftLayerBottom);
                cmd.Parameters.AddWithValue("@HardRedLayerTop", tooth.HardRedLayerTop);
                cmd.Parameters.AddWithValue("@HardRedLayerBottom", tooth.HardRedLayerBottom);
                cmd.Parameters.AddWithValue("@HardClearLayerTop", tooth.HardClearLayerTop);
                cmd.Parameters.AddWithValue("@HardClearLayerBottom", tooth.HardClearLayerBottom);
                cmd.Parameters.AddWithValue("@Saksion", tooth.Saksion);
                cmd.Parameters.AddWithValue("@PriceBaseFracture", tooth.PriceBaseFracture);
                cmd.Parameters.AddWithValue("@PriceSoftLayer", tooth.PriceSoftLayer);
                cmd.Parameters.AddWithValue("@PriceHardRedLayer", tooth.PriceHardRedLayer);
                cmd.Parameters.AddWithValue("@PriceHardClearLayer", tooth.PriceHardClearLayer);
                cmd.Parameters.AddWithValue("@PriceSaksion", tooth.PriceSaksion);
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
                        int colSoftLayerTop = reader.GetOrdinal("soft_layer_top");
                        int colSoftLayerBottom = reader.GetOrdinal("soft_layer_bottom");
                        int colHardRedLayerTop = reader.GetOrdinal("hard_red_layer_top");
                        int colHardRedLayerBottom = reader.GetOrdinal("hard_red_layer_bottom");
                        int colHardClearLayerTop = reader.GetOrdinal("hard_clear_layer_top");
                        int colHardClearLayerBottom = reader.GetOrdinal("hard_clear_layer_bottom");
                        int colSaksion = reader.GetOrdinal("saksion");
                        int colPriceBaseFracture = reader.GetOrdinal("price_base_fracture");
                        int colPriceSoftLayer = reader.GetOrdinal("price_soft_layer");
                        int colPriceHardRedLayer = reader.GetOrdinal("price_hard_red_layer");
                        int colPriceHardClearLayer = reader.GetOrdinal("price_hard_clear_layer");
                        int colPriceSaksion = reader.GetOrdinal("price_saksion");
                        
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
                            CreatedAt = reader.GetDateTime("created_at"),
                            BaseFractureTop = reader.IsDBNull(reader.GetOrdinal("base_fracture_top")) ? "" : reader.GetString("base_fracture_top"),
                            BaseFractureBottom = reader.IsDBNull(reader.GetOrdinal("base_fracture_bottom")) ? "" : reader.GetString("base_fracture_bottom"),
                            SoftLayerTop = reader.IsDBNull(colSoftLayerTop) ? false : reader.GetBoolean(colSoftLayerTop),
                            SoftLayerBottom = reader.IsDBNull(colSoftLayerBottom) ? false : reader.GetBoolean(colSoftLayerBottom),
                            HardRedLayerTop = reader.IsDBNull(colHardRedLayerTop) ? false : reader.GetBoolean(colHardRedLayerTop),
                            HardRedLayerBottom = reader.IsDBNull(colHardRedLayerBottom) ? false : reader.GetBoolean(colHardRedLayerBottom),
                            HardClearLayerTop = reader.IsDBNull(colHardClearLayerTop) ? false : reader.GetBoolean(colHardClearLayerTop),
                            HardClearLayerBottom = reader.IsDBNull(colHardClearLayerBottom) ? false : reader.GetBoolean(colHardClearLayerBottom),
                            Saksion = reader.IsDBNull(colSaksion) ? false : reader.GetBoolean(colSaksion),
                            PriceBaseFracture = reader.IsDBNull(colPriceBaseFracture) ? 0 : reader.GetDecimal(colPriceBaseFracture),
                            PriceSoftLayer = reader.IsDBNull(colPriceSoftLayer) ? 0 : reader.GetDecimal(colPriceSoftLayer),
                            PriceHardRedLayer = reader.IsDBNull(colPriceHardRedLayer) ? 0 : reader.GetDecimal(colPriceHardRedLayer),
                            PriceHardClearLayer = reader.IsDBNull(colPriceHardClearLayer) ? 0 : reader.GetDecimal(colPriceHardClearLayer),
                            PriceSaksion = reader.IsDBNull(colPriceSaksion) ? 0 : reader.GetDecimal(colPriceSaksion)
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
                var cmd = new MySqlCommand(@"UPDATE teeth SET tooth_name=@ToothName, tooth_type=@ToothType, unit_price=@UnitPrice, total_price=@TotalPrice, 
                                   tooth_size=@ToothSize, tooth_color=@ToothColor, base_fracture_top=@BaseFractureTop, base_fracture_bottom=@BaseFractureBottom,
                                   soft_layer_top=@SoftLayerTop, soft_layer_bottom=@SoftLayerBottom, hard_red_layer_top=@HardRedLayerTop, 
                                   hard_red_layer_bottom=@HardRedLayerBottom, hard_clear_layer_top=@HardClearLayerTop, hard_clear_layer_bottom=@HardClearLayerBottom,
                                   saksion=@Saksion, price_base_fracture=@PriceBaseFracture, price_soft_layer=@PriceSoftLayer, price_hard_red_layer=@PriceHardRedLayer,
                                   price_hard_clear_layer=@PriceHardClearLayer, price_saksion=@PriceSaksion WHERE id=@Id", conn);
                cmd.Parameters.AddWithValue("@ToothName", tooth.ToothName);
                cmd.Parameters.AddWithValue("@ToothType", tooth.ToothType);
                cmd.Parameters.AddWithValue("@UnitPrice", tooth.UnitPrice);
                cmd.Parameters.AddWithValue("@TotalPrice", tooth.TotalPrice);
                cmd.Parameters.AddWithValue("@ToothSize", tooth.ToothSize ?? "متوسط");
                cmd.Parameters.AddWithValue("@ToothColor", tooth.ToothColor ?? "A1");
                cmd.Parameters.AddWithValue("@BaseFractureTop", tooth.BaseFractureTop ?? "");
                cmd.Parameters.AddWithValue("@BaseFractureBottom", tooth.BaseFractureBottom ?? "");
                cmd.Parameters.AddWithValue("@SoftLayerTop", tooth.SoftLayerTop);
                cmd.Parameters.AddWithValue("@SoftLayerBottom", tooth.SoftLayerBottom);
                cmd.Parameters.AddWithValue("@HardRedLayerTop", tooth.HardRedLayerTop);
                cmd.Parameters.AddWithValue("@HardRedLayerBottom", tooth.HardRedLayerBottom);
                cmd.Parameters.AddWithValue("@HardClearLayerTop", tooth.HardClearLayerTop);
                cmd.Parameters.AddWithValue("@HardClearLayerBottom", tooth.HardClearLayerBottom);
                cmd.Parameters.AddWithValue("@Saksion", tooth.Saksion);
                cmd.Parameters.AddWithValue("@PriceBaseFracture", tooth.PriceBaseFracture);
                cmd.Parameters.AddWithValue("@PriceSoftLayer", tooth.PriceSoftLayer);
                cmd.Parameters.AddWithValue("@PriceHardRedLayer", tooth.PriceHardRedLayer);
                cmd.Parameters.AddWithValue("@PriceHardClearLayer", tooth.PriceHardClearLayer);
                cmd.Parameters.AddWithValue("@PriceSaksion", tooth.PriceSaksion);
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
                var cmd = new MySqlCommand("INSERT INTO payments (patient_id, total_amount, payment_method, notes, cheque_count, created_at) VALUES (@PatientId, @TotalAmount, @PaymentMethod, @Notes, @ChequeCount, @CreatedAt); SELECT LAST_INSERT_ID();", conn);
                cmd.Parameters.AddWithValue("@PatientId", payment.PatientId);
                cmd.Parameters.AddWithValue("@TotalAmount", payment.TotalAmount);
                cmd.Parameters.AddWithValue("@PaymentMethod", payment.PaymentMethod);
                cmd.Parameters.AddWithValue("@Notes", payment.Notes);
                cmd.Parameters.AddWithValue("@ChequeCount", payment.ChequeCount);
                cmd.Parameters.AddWithValue("@CreatedAt", payment.CreatedAt);
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
                            PaymentId = reader.GetInt32("payment_id"),
                            PatientId = reader.GetInt32("patient_id"),
                            TotalAmount = reader.GetDecimal("total_amount"),
                            PaymentMethod = reader.GetString("payment_method"),
                            Notes = reader.GetString("notes"),
                            ChequeCount = reader.GetInt32("cheque_count"),
                            CreatedAt = reader.GetDateTime("created_at")
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
                var cmd = new MySqlCommand("UPDATE payments SET total_amount=@TotalAmount, payment_method=@PaymentMethod, notes=@Notes, cheque_count=@ChequeCount WHERE payment_id=@PaymentId", conn);
                cmd.Parameters.AddWithValue("@TotalAmount", payment.TotalAmount);
                cmd.Parameters.AddWithValue("@PaymentMethod", payment.PaymentMethod);
                cmd.Parameters.AddWithValue("@Notes", payment.Notes);
                cmd.Parameters.AddWithValue("@ChequeCount", payment.ChequeCount);
                cmd.Parameters.AddWithValue("@PaymentId", payment.PaymentId);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeletePayment(int paymentId)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("DELETE FROM payments WHERE payment_id=@PaymentId", conn);
                cmd.Parameters.AddWithValue("@PaymentId", paymentId);
                cmd.ExecuteNonQuery();
            }
        }

        // CRUD برای جدول چک‌ها
        public int AddCheque(Cheque cheque)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("INSERT INTO cheques (patient_id, payment_id, cheque_number, cheque_amount, cheque_date, is_default, created_at) VALUES (@PatientId, @PaymentId, @ChequeNumber, @ChequeAmount, @ChequeDate, @IsDefault, @CreatedAt); SELECT LAST_INSERT_ID();", conn);
                cmd.Parameters.AddWithValue("@PatientId", cheque.PatientId);
                cmd.Parameters.AddWithValue("@PaymentId", cheque.PaymentId);
                cmd.Parameters.AddWithValue("@ChequeNumber", cheque.ChequeNumber);
                cmd.Parameters.AddWithValue("@ChequeAmount", cheque.ChequeAmount);
                cmd.Parameters.AddWithValue("@ChequeDate", cheque.ChequeDate);
                cmd.Parameters.AddWithValue("@IsDefault", cheque.IsDefault);
                cmd.Parameters.AddWithValue("@CreatedAt", cheque.CreatedAt);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public List<Cheque> GetChequesByPatientId(int patientId)
        {
            var list = new List<Cheque>();
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM cheques WHERE patient_id=@PatientId", conn);
                cmd.Parameters.AddWithValue("@PatientId", patientId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Cheque
                        {
                            ChequeId = reader.GetInt32("cheque_id"),
                            PatientId = reader.GetInt32("patient_id"),
                            PaymentId = reader.GetInt32("payment_id"),
                            ChequeNumber = reader.GetString("cheque_number"),
                            ChequeAmount = reader.GetDecimal("cheque_amount"),
                            ChequeDate = reader.GetDateTime("cheque_date"),
                            IsDefault = reader.GetBoolean("is_default"),
                            CreatedAt = reader.GetDateTime("created_at")
                        });
                    }
                }
            }
            return list;
        }

        public List<Cheque> GetChequesByPaymentId(int paymentId)
        {
            var list = new List<Cheque>();
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM cheques WHERE payment_id=@PaymentId", conn);
                cmd.Parameters.AddWithValue("@PaymentId", paymentId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Cheque
                        {
                            ChequeId = reader.GetInt32("cheque_id"),
                            PatientId = reader.GetInt32("patient_id"),
                            PaymentId = reader.GetInt32("payment_id"),
                            ChequeNumber = reader.GetString("cheque_number"),
                            ChequeAmount = reader.GetDecimal("cheque_amount"),
                            ChequeDate = reader.GetDateTime("cheque_date"),
                            IsDefault = reader.GetBoolean("is_default"),
                            CreatedAt = reader.GetDateTime("created_at")
                        });
                    }
                }
            }
            return list;
        }

        public void UpdateCheque(Cheque cheque)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("UPDATE cheques SET cheque_number=@ChequeNumber, cheque_amount=@ChequeAmount, cheque_date=@ChequeDate, is_default=@IsDefault WHERE cheque_id=@ChequeId", conn);
                cmd.Parameters.AddWithValue("@ChequeNumber", cheque.ChequeNumber);
                cmd.Parameters.AddWithValue("@ChequeAmount", cheque.ChequeAmount);
                cmd.Parameters.AddWithValue("@ChequeDate", cheque.ChequeDate);
                cmd.Parameters.AddWithValue("@IsDefault", cheque.IsDefault);
                cmd.Parameters.AddWithValue("@ChequeId", cheque.ChequeId);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteCheque(int chequeId)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("DELETE FROM cheques WHERE cheque_id=@ChequeId", conn);
                cmd.Parameters.AddWithValue("@ChequeId", chequeId);
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
                            VisitReason = reader.IsDBNull(reader.GetOrdinal("visit_reason")) ? "" : reader.GetString("visit_reason"),
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

        // متد برای اضافه کردن رکورد تست به جدول cases
        public void AddTestCase(int patientId)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("INSERT INTO cases (patient_id, status, description, visit_reason, last_update) VALUES (@PatientId, 'active', 'تست', 'درد دندان', @LastUpdate)", conn);
                cmd.Parameters.AddWithValue("@PatientId", patientId);
                cmd.Parameters.AddWithValue("@LastUpdate", DateTime.Now);
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

        // CRUD برای انبار
        public int AddInventoryItem(InventoryItem item)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand(@"INSERT INTO inventory 
                    (product_name, sku, category, quantity, min_stock, unit_price, total_price, 
                     supplier, supplier_contact, purchase_date, stock_status, tooth_color, notes) 
                    VALUES (@ProductName, @Sku, @Category, @Quantity, @MinStock, @UnitPrice, @TotalPrice,
                            @Supplier, @SupplierContact, @PurchaseDate, @StockStatus, @ToothColor, @Notes); 
                    SELECT LAST_INSERT_ID();", conn);
                
                cmd.Parameters.AddWithValue("@ProductName", item.ProductName);
                cmd.Parameters.AddWithValue("@Sku", item.Sku);
                cmd.Parameters.AddWithValue("@Category", item.Category);
                cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                cmd.Parameters.AddWithValue("@MinStock", item.MinStock);
                cmd.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                cmd.Parameters.AddWithValue("@TotalPrice", item.TotalPrice);
                cmd.Parameters.AddWithValue("@Supplier", item.Supplier);
                cmd.Parameters.AddWithValue("@SupplierContact", item.SupplierContact);
                cmd.Parameters.AddWithValue("@PurchaseDate", item.PurchaseDate);
                cmd.Parameters.AddWithValue("@StockStatus", "موجود"); // همیشه مقدار ثابت
                cmd.Parameters.AddWithValue("@ToothColor", item.ToothColor ?? "A1");
                cmd.Parameters.AddWithValue("@Notes", item.Notes ?? "");
                
                int id = Convert.ToInt32(cmd.ExecuteScalar());
                
                // به‌روزرسانی StockStatus بر اساس موجودی
                UpdateStockStatus(id, item.Quantity, item.MinStock);
                
                return id;
            }
        }

        public List<InventoryItem> GetAllInventoryItems()
        {
            var list = new List<InventoryItem>();
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM inventory ORDER BY id DESC", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new InventoryItem
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            ProductName = reader.IsDBNull(reader.GetOrdinal("product_name")) ? "" : reader.GetString(reader.GetOrdinal("product_name")),
                            Sku = reader.IsDBNull(reader.GetOrdinal("sku")) ? "" : reader.GetString(reader.GetOrdinal("sku")),
                            Category = reader.IsDBNull(reader.GetOrdinal("category")) ? "" : reader.GetString(reader.GetOrdinal("category")),
                            Quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                            MinStock = reader.GetInt32(reader.GetOrdinal("min_stock")),
                            UnitPrice = reader.GetDecimal(reader.GetOrdinal("unit_price")),
                            TotalPrice = reader.GetDecimal(reader.GetOrdinal("total_price")),
                            Supplier = reader.IsDBNull(reader.GetOrdinal("supplier")) ? "" : reader.GetString(reader.GetOrdinal("supplier")),
                            SupplierContact = reader.IsDBNull(reader.GetOrdinal("supplier_contact")) ? "" : reader.GetString(reader.GetOrdinal("supplier_contact")),
                            PurchaseDate = reader.GetDateTime(reader.GetOrdinal("purchase_date")),
                            StockStatus = reader.IsDBNull(reader.GetOrdinal("stock_status")) ? "" : reader.GetString(reader.GetOrdinal("stock_status")),
                            ToothColor = reader.IsDBNull(reader.GetOrdinal("tooth_color")) ? "A1" : reader.GetString(reader.GetOrdinal("tooth_color")),
                            Notes = reader.IsDBNull(reader.GetOrdinal("notes")) ? "" : reader.GetString(reader.GetOrdinal("notes"))
                        });
                    }
                }
            }
            return list;
        }

        public InventoryItem GetInventoryItemById(int id)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM inventory WHERE id=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new InventoryItem
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            ProductName = reader.IsDBNull(reader.GetOrdinal("product_name")) ? "" : reader.GetString(reader.GetOrdinal("product_name")),
                            Sku = reader.IsDBNull(reader.GetOrdinal("sku")) ? "" : reader.GetString(reader.GetOrdinal("sku")),
                            Category = reader.IsDBNull(reader.GetOrdinal("category")) ? "" : reader.GetString(reader.GetOrdinal("category")),
                            Quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                            MinStock = reader.GetInt32(reader.GetOrdinal("min_stock")),
                            UnitPrice = reader.GetDecimal(reader.GetOrdinal("unit_price")),
                            TotalPrice = reader.GetDecimal(reader.GetOrdinal("total_price")),
                            Supplier = reader.IsDBNull(reader.GetOrdinal("supplier")) ? "" : reader.GetString(reader.GetOrdinal("supplier")),
                            SupplierContact = reader.IsDBNull(reader.GetOrdinal("supplier_contact")) ? "" : reader.GetString(reader.GetOrdinal("supplier_contact")),
                            PurchaseDate = reader.GetDateTime(reader.GetOrdinal("purchase_date")),
                            StockStatus = reader.IsDBNull(reader.GetOrdinal("stock_status")) ? "" : reader.GetString(reader.GetOrdinal("stock_status")),
                            Notes = reader.IsDBNull(reader.GetOrdinal("notes")) ? "" : reader.GetString(reader.GetOrdinal("notes"))
                        };
                    }
                }
            }
            return null;
        }

        public void UpdateInventoryItem(InventoryItem item)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand(@"UPDATE inventory SET 
                    product_name=@ProductName, sku=@Sku, category=@Category, quantity=@Quantity,
                    min_stock=@MinStock, unit_price=@UnitPrice, total_price=@TotalPrice,
                    supplier=@Supplier, supplier_contact=@SupplierContact, purchase_date=@PurchaseDate,
                    stock_status=@StockStatus, tooth_color=@ToothColor, notes=@Notes WHERE id=@Id", conn);
                
                cmd.Parameters.AddWithValue("@ProductName", item.ProductName);
                cmd.Parameters.AddWithValue("@Sku", item.Sku);
                cmd.Parameters.AddWithValue("@Category", item.Category);
                cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                cmd.Parameters.AddWithValue("@MinStock", item.MinStock);
                cmd.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                cmd.Parameters.AddWithValue("@TotalPrice", item.TotalPrice);
                cmd.Parameters.AddWithValue("@Supplier", item.Supplier);
                cmd.Parameters.AddWithValue("@SupplierContact", item.SupplierContact);
                cmd.Parameters.AddWithValue("@PurchaseDate", item.PurchaseDate);
                cmd.Parameters.AddWithValue("@StockStatus", "موجود"); // همیشه مقدار ثابت
                cmd.Parameters.AddWithValue("@ToothColor", item.ToothColor ?? "A1");
                cmd.Parameters.AddWithValue("@Notes", item.Notes ?? "");
                cmd.Parameters.AddWithValue("@Id", item.Id);
                
                cmd.ExecuteNonQuery();
                
                // به‌روزرسانی StockStatus بر اساس موجودی
                UpdateStockStatus(item.Id, item.Quantity, item.MinStock);
            }
        }

        private void UpdateStockStatus(int id, int quantity, int minStock)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string stockStatus;
                if (quantity <= 0)
                    stockStatus = "ناموجود";
                else if (quantity <= minStock)
                    stockStatus = "کم موجود";
                else if (quantity <= (minStock + 2))
                    stockStatus = "کم موجود"; // هشدار از 2 عدد بیشتر
                else
                    stockStatus = "موجود";

                var cmd = new MySqlCommand("UPDATE inventory SET stock_status=@StockStatus WHERE id=@Id", conn);
                cmd.Parameters.AddWithValue("@StockStatus", stockStatus);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteInventoryItem(int id)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("DELETE FROM inventory WHERE id=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public List<InventoryItem> SearchInventoryItems(string keyword)
        {
            var list = new List<InventoryItem>();
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand(@"SELECT * FROM inventory 
                    WHERE product_name LIKE @kw OR sku LIKE @kw OR category LIKE @kw OR supplier LIKE @kw
                    ORDER BY id DESC", conn);
                cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new InventoryItem
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            ProductName = reader.IsDBNull(reader.GetOrdinal("product_name")) ? "" : reader.GetString(reader.GetOrdinal("product_name")),
                            Sku = reader.IsDBNull(reader.GetOrdinal("sku")) ? "" : reader.GetString(reader.GetOrdinal("sku")),
                            Category = reader.IsDBNull(reader.GetOrdinal("category")) ? "" : reader.GetString(reader.GetOrdinal("category")),
                            Quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                            MinStock = reader.GetInt32(reader.GetOrdinal("min_stock")),
                            UnitPrice = reader.GetDecimal(reader.GetOrdinal("unit_price")),
                            TotalPrice = reader.GetDecimal(reader.GetOrdinal("total_price")),
                            Supplier = reader.IsDBNull(reader.GetOrdinal("supplier")) ? "" : reader.GetString(reader.GetOrdinal("supplier")),
                            SupplierContact = reader.IsDBNull(reader.GetOrdinal("supplier_contact")) ? "" : reader.GetString(reader.GetOrdinal("supplier_contact")),
                            PurchaseDate = reader.GetDateTime(reader.GetOrdinal("purchase_date")),
                            StockStatus = reader.IsDBNull(reader.GetOrdinal("stock_status")) ? "" : reader.GetString(reader.GetOrdinal("stock_status")),
                            Notes = reader.IsDBNull(reader.GetOrdinal("notes")) ? "" : reader.GetString(reader.GetOrdinal("notes"))
                        });
                    }
                }
            }
            return list;
        }

        public List<InventoryItem> GetLowStockItems()
        {
            var list = new List<InventoryItem>();
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                // تغییر کوئری برای نمایش محصولات که موجودی آن‌ها <= (حداقل + 2) است
                var cmd = new MySqlCommand("SELECT * FROM inventory WHERE quantity <= (min_stock + 2) AND quantity > 0 ORDER BY quantity ASC", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new InventoryItem
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            ProductName = reader.IsDBNull(reader.GetOrdinal("product_name")) ? "" : reader.GetString(reader.GetOrdinal("product_name")),
                            Sku = reader.IsDBNull(reader.GetOrdinal("sku")) ? "" : reader.GetString(reader.GetOrdinal("sku")),
                            Category = reader.IsDBNull(reader.GetOrdinal("category")) ? "" : reader.GetString(reader.GetOrdinal("category")),
                            Quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                            MinStock = reader.GetInt32(reader.GetOrdinal("min_stock")),
                            UnitPrice = reader.GetDecimal(reader.GetOrdinal("unit_price")),
                            TotalPrice = reader.GetDecimal(reader.GetOrdinal("total_price")),
                            Supplier = reader.IsDBNull(reader.GetOrdinal("supplier")) ? "" : reader.GetString(reader.GetOrdinal("supplier")),
                            SupplierContact = reader.IsDBNull(reader.GetOrdinal("supplier_contact")) ? "" : reader.GetString(reader.GetOrdinal("supplier_contact")),
                            PurchaseDate = reader.GetDateTime(reader.GetOrdinal("purchase_date")),
                            StockStatus = reader.IsDBNull(reader.GetOrdinal("stock_status")) ? "" : reader.GetString(reader.GetOrdinal("stock_status")),
                            Notes = reader.IsDBNull(reader.GetOrdinal("notes")) ? "" : reader.GetString(reader.GetOrdinal("notes"))
                        });
                    }
                }
            }
            return list;
        }

        public Dictionary<string, object> GetInventoryStatistics()
        {
            var stats = new Dictionary<string, object>();
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                
                // تعداد کل محصولات
                var cmd1 = new MySqlCommand("SELECT COUNT(*) FROM inventory", conn);
                stats["TotalProducts"] = Convert.ToInt32(cmd1.ExecuteScalar());
                
                // ارزش کل انبار
                var cmd2 = new MySqlCommand("SELECT SUM(total_price) FROM inventory", conn);
                var totalValue = cmd2.ExecuteScalar();
                stats["TotalValue"] = totalValue == DBNull.Value ? 0 : Convert.ToDecimal(totalValue);
                
                // تعداد محصولات دندان‌پزشکی
                var cmd3 = new MySqlCommand("SELECT COUNT(*) FROM inventory WHERE category LIKE '%دندان%' OR category LIKE '%دندانپزشکی%'", conn);
                stats["DentalItems"] = Convert.ToInt32(cmd3.ExecuteScalar());
                
                // تعداد محصولات کم‌موجود
                var cmd4 = new MySqlCommand("SELECT COUNT(*) FROM inventory WHERE quantity <= min_stock", conn);
                stats["LowStockItems"] = Convert.ToInt32(cmd4.ExecuteScalar());
            }
            return stats;
        }

        public List<InventoryItem> GetDentalItemsFromInventory()
        {
            var list = new List<InventoryItem>();
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                // دریافت محصولات دندان از انبار
                var cmd = new MySqlCommand("SELECT * FROM inventory WHERE category = 'دندان' ORDER BY product_name ASC", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new InventoryItem
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            ProductName = reader.IsDBNull(reader.GetOrdinal("product_name")) ? "" : reader.GetString(reader.GetOrdinal("product_name")),
                            Sku = reader.IsDBNull(reader.GetOrdinal("sku")) ? "" : reader.GetString(reader.GetOrdinal("sku")),
                            Category = reader.IsDBNull(reader.GetOrdinal("category")) ? "" : reader.GetString(reader.GetOrdinal("category")),
                            Quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                            MinStock = reader.GetInt32(reader.GetOrdinal("min_stock")),
                            UnitPrice = reader.GetDecimal(reader.GetOrdinal("unit_price")),
                            TotalPrice = reader.GetDecimal(reader.GetOrdinal("total_price")),
                            Supplier = reader.IsDBNull(reader.GetOrdinal("supplier")) ? "" : reader.GetString(reader.GetOrdinal("supplier")),
                            SupplierContact = reader.IsDBNull(reader.GetOrdinal("supplier_contact")) ? "" : reader.GetString(reader.GetOrdinal("supplier_contact")),
                            PurchaseDate = reader.GetDateTime(reader.GetOrdinal("purchase_date")),
                            StockStatus = reader.IsDBNull(reader.GetOrdinal("stock_status")) ? "" : reader.GetString(reader.GetOrdinal("stock_status")),
                            ToothColor = reader.IsDBNull(reader.GetOrdinal("tooth_color")) ? "A1" : reader.GetString(reader.GetOrdinal("tooth_color")),
                            Notes = reader.IsDBNull(reader.GetOrdinal("notes")) ? "" : reader.GetString(reader.GetOrdinal("notes"))
                        });
                    }
                }
            }
            return list;
        }
    }
}
