using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEDU.Core.Validators
{
	static class CustomValidator
	{
		public static string Validate(dynamic obj)
		{
			var results = new List<ValidationResult>();
			var context = new ValidationContext(obj);
			if (!Validator.TryValidateObject(obj, context, results, true))
			{
				foreach (var error in results)
				{
						return error.ErrorMessage;
				}
			}
			
			return null;
		}
	}
}
