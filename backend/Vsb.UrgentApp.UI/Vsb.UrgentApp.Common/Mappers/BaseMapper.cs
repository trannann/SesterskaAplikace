using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsb.UrgentApp.Common.Mappers;

namespace Vsb.UrgentApp.Common.Mapper
{
    public class BaseMapper : IBaseMapper
    {
		public T BindData<T>(DataTable dt)
		{
			DataRow dr = dt.Rows[0];

			// Get all columns' name
			List<string> columns = new List<string>();
			foreach (DataColumn dc in dt.Columns)
			{
				columns.Add(dc.ColumnName);
			}

			// Create object
			var ob = Activator.CreateInstance<T>();

			// Get all fields
			var fields = typeof(T).GetFields();
			foreach (var fieldInfo in fields)
			{
				if (columns.Contains(fieldInfo.Name))
				{
					// Fill the data into the field
					fieldInfo.SetValue(ob, dr[fieldInfo.Name]);
				}
			}

			// Get all properties
			var properties = typeof(T).GetProperties();
			foreach (var propertyInfo in properties)
			{
				if (columns.Contains(propertyInfo.Name))
				{
					if (dr[propertyInfo.Name] != null)
					{
						if (string.IsNullOrEmpty(dr[propertyInfo.Name].ToString()))
						{
							propertyInfo.SetValue(ob, string.Empty);
						}
						else
						{
							propertyInfo.SetValue(ob, dr[propertyInfo.Name]);
						}
					}
				}
			}

			return ob;
		}

		public List<T> BindDataList<T>(DataTable dt)
		{
			List<string> columns = new List<string>();
			foreach (DataColumn dc in dt.Columns)
			{
				columns.Add(dc.ColumnName);
			}

			var fields = typeof(T).GetFields();
			var properties = typeof(T).GetProperties();

			List<T> lst = new List<T>();

			foreach (DataRow dr in dt.Rows)
			{
				var ob = Activator.CreateInstance<T>();

				foreach (var fieldInfo in fields)
				{
					if (columns.Contains(fieldInfo.Name))
					{
						fieldInfo.SetValue(ob, dr[fieldInfo.Name]);
					}
				}

				foreach (var propertyInfo in properties)
				{
					if (columns.Any(p => p.Equals(propertyInfo.Name, StringComparison.OrdinalIgnoreCase)))
					{
						if (dr[propertyInfo.Name] != null)
						{
							if (propertyInfo.PropertyType == typeof(string))
							{
								if (string.IsNullOrEmpty(dr[propertyInfo.Name].ToString()))
								{
									propertyInfo.SetValue(ob, string.Empty);
								}
								else
								{
									propertyInfo.SetValue(ob, dr[propertyInfo.Name]);
								}
							}
							else if (propertyInfo.PropertyType == typeof(DateTime))
							{
								if (string.IsNullOrEmpty(dr[propertyInfo.Name].ToString()))
								{
									propertyInfo.SetValue(ob, null);
								}
								else
								{
									propertyInfo.SetValue(ob, dr[propertyInfo.Name]);
								}
							}
							else if (dr[propertyInfo.Name].GetType() == typeof(System.DBNull))
							{
								propertyInfo.SetValue(ob, null);		
							}
							else
							{
								propertyInfo.SetValue(ob, dr[propertyInfo.Name]);
							}
						}
						else
						{
							propertyInfo.SetValue(ob, null);
						}
					}
				}

				lst.Add(ob);
			}

			return lst;
		}
	}
}
