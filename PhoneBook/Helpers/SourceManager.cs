using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using PhoneBook.Helpers;

namespace PhoneBook.Models
{
	public class SourceManager
	{
		public static List<PersonModel> Get(int start, int take)
		{
			var personList = new List<PersonModel>();

			using (var connection = SqlHelper.GetConnection())
			{
				var sqlCommand = new SqlCommand();
				sqlCommand.Connection = connection;
				sqlCommand.CommandText = "SELECT * FROM People ORDER BY ID OFFSET @Start ROWS FETCH NEXT @Take ROWS ONLY;";

				var sqlStartParam = new SqlParameter
				{
					DbType = System.Data.DbType.Int32,
					Value = (start - 1) * take,
					ParameterName = "@Start"
				};

				var sqlTakeParam = new SqlParameter
				{
					DbType = System.Data.DbType.Int32,
					Value = take,
					ParameterName = "@Take"
				};
				sqlCommand.Parameters.Add(sqlStartParam);
				sqlCommand.Parameters.Add(sqlTakeParam);

				var data = sqlCommand.ExecuteReader();

				while (data.HasRows && data.Read())
				{
					personList.Add(new PersonModel((int)data["ID"],
					data["FirstName"].ToString(),
					data["LastName"].ToString(),
					data["Phone"].ToString(),
					data["Email"].ToString(),
					(DateTime)data["Created"],
					(DateTime?)data["Updated"]
					));
				}
			}
			return personList;
		}

		public static PersonModel GetByID(int id)
		{
			PersonModel personmodel = null ;
			using (var connection = SqlHelper.GetConnection())
			{
				var sqlCommand = new SqlCommand();
				sqlCommand.Connection = connection;
				sqlCommand.CommandText = "SELECT * FROM People where ID = @id";
				

				var sqlIDParam = new SqlParameter
				{
					DbType = System.Data.DbType.Int32,
					Value = id,
					ParameterName = "@id"
				};
				
				sqlCommand.Parameters.Add(sqlIDParam);
				
				var data = sqlCommand.ExecuteReader();

				while (data.HasRows && data.Read())
				{
					personmodel = new PersonModel(data.GetInt32(0), data.GetString(1), data.GetString(2), data.GetString(3), data.GetString(4), data.GetDateTime(5), data.GetDateTime(6));
				}
				 
			}
			return personmodel;


		}

		public static List<PersonModel> GetByName(string lastname, int start, int take)
		{
			List<PersonModel> listnamessearch = new List<PersonModel>();
			using (var connection = SqlHelper.GetConnection())
			{
				var sqlCommand = new SqlCommand();
				sqlCommand.Connection = connection;
				sqlCommand.CommandText = "SELECT * FROM People where LastName like @LastName Order by ID Offset @Start rows fetch next @Take Rows Only;";
				

				var sqlLastNameParam = new SqlParameter
				{
					DbType = System.Data.DbType.AnsiString,
					Value = lastname +="%",
					ParameterName = "@LastName"
				};

				var sqlStartParam = new SqlParameter
				{
					DbType = System.Data.DbType.Int32,
					Value = (start - 1) * take,
					ParameterName = "@Start"
				};

				var sqlTakeParam = new SqlParameter
				{
					DbType = System.Data.DbType.Int32,
					Value = take,
					ParameterName = "@Take"
				};
				
				sqlCommand.Parameters.Add(sqlLastNameParam);
				sqlCommand.Parameters.Add(sqlStartParam);
				sqlCommand.Parameters.Add(sqlTakeParam);

				var data = sqlCommand.ExecuteReader();

				while (data.HasRows && data.Read())
				{
					listnamessearch.Add(new PersonModel(data.GetInt32(0), data.GetString(1), data.GetString(2), data.GetString(3), data.GetString(4), data.GetDateTime(5), data.GetDateTime(6)));
					
				}
				return listnamessearch;
			}

		}
		 
		public static int Add(PersonModel personModel)
		{
			using (var connection = SqlHelper.GetConnection())
			{
				var sqlCommand = new SqlCommand();
				sqlCommand.Connection = connection;
				sqlCommand.CommandText = @"Insert INTO People (FirstName, LastName, Phone, Email, Created, Updated)
				VALUES (@FirstName,@LastName,@Phone, @Email, @Created, @Updated); SELECT CAST(scope_identity() AS int)";

				var sqlFirstNameParam = new SqlParameter
				{
					DbType = System.Data.DbType.AnsiString,
					Value = personModel.FirstName,
					ParameterName = "@FirstName"
				};

				var sqlLastNameParam = new SqlParameter
				{
					DbType = System.Data.DbType.AnsiString,
					Value = personModel.LastName,
					ParameterName = "@LastName"
				};

				var sqlPhoneParam = new SqlParameter
				{
					DbType = System.Data.DbType.AnsiString,
					Value = personModel.Phone,
					ParameterName = "@Phone"
				};
				var sqlEmailParam = new SqlParameter
				{
					DbType = System.Data.DbType.AnsiString,
					Value = personModel.Email,
					ParameterName = "@Email"
				};
				var sqlCreatedDateParam = new SqlParameter
				{
					DbType = System.Data.DbType.DateTime,
					Value = personModel.Created,
					ParameterName = "@Created"
				};

				var sqlUpdatedDateParam = new SqlParameter
				{
					DbType = System.Data.DbType.DateTime,
					Value = personModel.Updated,
					ParameterName = "@Updated"
				};

				sqlCommand.Parameters.Add(sqlFirstNameParam);
				sqlCommand.Parameters.Add(sqlLastNameParam);
				sqlCommand.Parameters.Add(sqlPhoneParam);
				sqlCommand.Parameters.Add(sqlEmailParam);
				sqlCommand.Parameters.Add(sqlCreatedDateParam);
				sqlCommand.Parameters.Add(sqlUpdatedDateParam);


				return (int)sqlCommand.ExecuteScalar();

			}
		}

		public static int Update(PersonModel personModel)
		{
			using (var connection = SqlHelper.GetConnection())
			{
				var sqlCommand = new SqlCommand();
				sqlCommand.Connection = connection;
				sqlCommand.CommandText = @"Update People set
										FirstName = @FirstName,
										LastName = @LastName,
										Phone = @Phone,
										Email = @Email,
										Updated = @Updated
										WHERE ID = @id;";
				 
				var sqlIDParam = new SqlParameter
				{
					DbType = System.Data.DbType.Int32,
					Value = personModel.ID,
					ParameterName = "@id"
				};

				var sqlFirstNameParam = new SqlParameter
				{
					DbType = System.Data.DbType.AnsiString,
					Value = personModel.FirstName,
					ParameterName = "@FirstName"
				};

				var sqlLastNameParam = new SqlParameter
				{
					DbType = System.Data.DbType.AnsiString,
					Value = personModel.LastName,
					ParameterName = "@LastName"
				};

				var sqlPhoneParam = new SqlParameter
				{
					DbType = System.Data.DbType.AnsiString,
					Value = personModel.Phone,
					ParameterName = "@Phone"
				};
				var sqlEmailParam = new SqlParameter
				{
					DbType = System.Data.DbType.AnsiString,
					Value = personModel.Email,
					ParameterName = "@Email"
				};

				var sqlUpdatedDateParam = new SqlParameter
				{
					DbType = System.Data.DbType.DateTime,
					Value = DateTime.Now,
					ParameterName = "@Updated"
				};

				sqlCommand.Parameters.Add(sqlIDParam);
				sqlCommand.Parameters.Add(sqlFirstNameParam);
				sqlCommand.Parameters.Add(sqlLastNameParam);
				sqlCommand.Parameters.Add(sqlPhoneParam);
				sqlCommand.Parameters.Add(sqlEmailParam);
				sqlCommand.Parameters.Add(sqlUpdatedDateParam);


				int id = (int)sqlCommand.ExecuteNonQuery();
				return id;
			}
		}
		public static void Remove(PersonModel personModel)
		{
			using (var connection = SqlHelper.GetConnection())
			{
				var sqlCommand = new SqlCommand();
				sqlCommand.Connection = connection;
				sqlCommand.CommandText = @"Delete from People where ID = @id";

				var sqlIDParam = new SqlParameter
				{
					DbType = System.Data.DbType.Int32,
					Value = personModel.ID,
					ParameterName = "@id"
				};
				
				sqlCommand.Parameters.Add(sqlIDParam);
				int x = sqlCommand.ExecuteNonQuery();
				
			}
		}
	}

		
	


}

